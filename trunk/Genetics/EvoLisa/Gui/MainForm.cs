using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GenArt.AST;
using GenArt.Classes;
using GenArt.Core.Interfaces;
using GenArt.Core.Classes;

namespace GenArt
{
    public partial class MainForm : Form
    {
        public DnaProject Project;
        private string projectFileName = "";

        private DnaDrawing currentDrawing = null;

        private DnaDrawing guiDrawing;
        private DateTime lastRepaint = DateTime.MinValue;
        private int lastSelected;
        private TimeSpan repaintIntervall = new TimeSpan(0, 0, 0, 0, 0);
        private int repaintOnSelectedSteps = 3;
        private SettingsForm settingsForm;
        private StatsForm statsForm;

        private Thread thread;

        public MainForm()
        {
            InitializeComponent();

            Project = new DnaProject();
            Project.Init();

            Project.Settings.Scale = trackBarScale.Value;
            comboBoxAnimSaveFormat.SelectedIndex = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void StartEvolution()
        {
            var sourceImage = new SourceImage
                                  {
                                      Colors = SetupSourceColorMatrix(picPattern.Image as Bitmap),
                                      Width = picPattern.Width,
                                      Height = picPattern.Height
                                  };

            //IEvolutionJob job = new LayeredEvolutionJob(sourceImage, 4);

            //DefaultEvolutionJob job = new DefaultEvolutionJob(sourceImage, currentDrawing);
            //IEvolutionJob job = new DefaultEvolutionJob(sourceImage, currentDrawing);
            IEvolutionJob job = new ClusteredEvolutionJob(sourceImage);

            while (Project.IsRunning)
            {
                double newErrorLevel = job.GetNextErrorLevel();
              //  Project.Generations += job.Generations;

                Project.Mutations++;

                if (newErrorLevel <= Project.ErrorLevel)
                {
                    Project.Selected++;

                    if (newErrorLevel < Project.ErrorLevel)
                        Project.Positive++;
                    else
                        Project.Neutral++;

                    var newDrawing = job.GetDrawing();
                    if (currentDrawing == null) // to make always lockable...
                        currentDrawing = new DnaDrawing();

                    lock (currentDrawing)
                    {
                        currentDrawing = newDrawing;
                        Project.Drawing = currentDrawing.Clone();
                    }

                    Project.ErrorLevel = newErrorLevel;
                }
            }
        }

        //converts the source image to a Color[,] for faster lookup
        private Color[,] SetupSourceColorMatrix(Bitmap sourceBitmap)
        {
            var sourceColors = new Color[sourceBitmap.Width, sourceBitmap.Height];
            var sourceImage = picPattern.Image as Bitmap;

            if (sourceImage == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            for (int y = 0; y < sourceBitmap.Height; y++)
            {
                for (int x = 0; x < sourceBitmap.Width; x++)
                {
                    Color c = sourceImage.GetPixel(x, y);
                    sourceColors[x, y] = c;
                }
            }

            return sourceColors;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Project.IsRunning)
                Stop();
            else
                Start();
        }

        private void Start()
        {
            btnStart.Text = "Stop";
            Project.IsRunning = true;
            tmrRedraw.Enabled = true;

            if (thread != null)
                KillThread();

            thread = new Thread(StartEvolution)
            {
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal
            };

            Project.LastStartTime = DateTime.Now;
            thread.Start();
        }

        private void KillThread()
        {
            if (thread != null)
            {
                thread.Abort();
            }
            thread = null;
        }

        private void Stop()
        {
            if (Project.IsRunning)
            {
                KillThread();
                UpdateElapsed();
            }

            btnStart.Text = "Start";
            Project.IsRunning = false;
            tmrRedraw.Enabled = false;
        }

        private void UpdateElapsed()
        {
            Project.ElapsedTime += DateTime.Now - Project.LastStartTime;
        }


        private void RepaintCanvas()
        {
            if (currentDrawing == null)
                return;

            lock (currentDrawing)
            {
                guiDrawing = currentDrawing.Clone();
            }
            pnlCanvas.Invalidate();
            lastRepaint = DateTime.Now;
            lastSelected = Project.Selected;
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            if (currentDrawing == null)
                return;

            if (statsForm != null)
            {
                if (statsForm.Visible)
                    statsForm.UpdateStats();
            }

            bool shouldRepaint = false;
            if (repaintIntervall.Ticks > 0)
                if (lastRepaint < DateTime.Now - repaintIntervall)
                    shouldRepaint = true;

            if (repaintOnSelectedSteps > 0)
                if (lastSelected + repaintOnSelectedSteps < Project.Selected)
                    shouldRepaint = true;

            if (shouldRepaint)
            {
                RepaintCanvas();
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (guiDrawing == null)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }


            using (
                var backBuffer = new Bitmap(Project.Settings.Scale * picPattern.Width, Project.Settings.Scale * picPattern.Height,
                                            PixelFormat.Format24bppRgb))
            using (Graphics backGraphics = Graphics.FromImage(backBuffer))
            {
                backGraphics.SmoothingMode = SmoothingMode.HighQuality;
                Renderer.Render(guiDrawing, backGraphics, Project.Settings.Scale);

                e.Graphics.DrawImage(backBuffer, 0, 0);
            }
        }


        private void NewProject()
        {
            Project = new DnaProject();
            Project.Init();
            projectFileName = "";
            this.Text = "[New Project]";
        }

        private void OpenProject()
        {
            Stop();

            string fileName = FileUtil.GetOpenFileName(FileUtil.ProjectExtension);
            DnaProject project = Serializer.DeserializeDnaProject(fileName);

            if (project != null)
            {
                Project = project;

                if (!string.IsNullOrEmpty(Project.ImagePath))
                    OpenImage(Project.ImagePath);

                if (Project.Drawing != null)
                {
                    if (currentDrawing == null)
                        currentDrawing = new DnaDrawing();

                    lock (currentDrawing)
                    {
                        currentDrawing = Project.Drawing;
                        guiDrawing = currentDrawing.Clone();
                    }
                }
                ActivateProjectSettings();

                ResetProjectLevels();
                RepaintCanvas();

                projectFileName = fileName;
            }

            SetTitleBar();
        }

        private void ActivateProjectSettings()
        {
            trackBarScale.Value = Project.Settings.Scale;
            switch (Project.Settings.HistoryImageSaveTrigger)
            {
                case HistorySaveTrigger.None:
                    radioButtonAnimSaveNever.Checked = true;
                    break;
                case HistorySaveTrigger.Fitness:
                    radioButtonAnimSaveFitness.Checked = true;
                    break;
                case HistorySaveTrigger.Selected:
                    radioButtonAnimSaveSelected.Checked = true;
                    break;
            }
            switch (Project.Settings.HistoryImageFormatName)
            {
                case "Bmp":
                    comboBoxAnimSaveFormat.SelectedIndex = 0;
                    break;
                case "Gif":
                    comboBoxAnimSaveFormat.SelectedIndex = 1;
                    break;
                case "Jpg":
                    comboBoxAnimSaveFormat.SelectedIndex = 2;
                    break;
            }
            Project.Settings.HistoryImageSteps = numericUpDownAnimSaveSteps.Value;

            Project.Settings.Activate();
        }

        private void SaveProject()
        {
            SaveProjectAs(projectFileName);
        }

        private void SaveProjectAs()
        {
            SaveProjectAs(null);
        }

        private void SaveProjectAs(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = FileUtil.GetSaveFileName(FileUtil.ProjectExtension);

            if (string.IsNullOrEmpty(fileName))
                return;

            if (Project == null)
                return;

            if (Project.IsRunning)
                UpdateElapsed();

            Serializer.Serialize(Project, fileName);

            projectFileName = fileName;

            SetTitleBar();
        }

        private void SetTitleBar()
        {
            this.Text = projectFileName;
        }


        private void OpenImage()
        {
            OpenImage(null);
        }

        private void OpenImage(string fileName)
        {
            Stop();

            if (string.IsNullOrEmpty(fileName))
                fileName = FileUtil.GetOpenFileName(FileUtil.ImgExtension);

            if (string.IsNullOrEmpty(fileName))
                return;

            picPattern.Image = Image.FromFile(fileName);

            SetCanvasSize();

            splitContainer1.SplitterDistance = picPattern.Width + 30;

            Project.ImagePath = fileName;
            ResetProjectLevels();
        }

        private void ResetProjectLevels()
        {
            Project.ErrorLevel = double.MaxValue;
            Project.Generations = 0;
            Project.Mutations = 0;
            Project.Selected = 0;
            Project.Positive = 0;
            Project.Neutral = 0;
            Project.LastSavedFitness = 0;
            Project.LastSavedSelected = 0;
            Project.LastStartTime = DateTime.MinValue;
            Project.ElapsedTime = TimeSpan.Zero;
        }

        private void SetCanvasSize()
        {
            pnlCanvas.Height = Project.Settings.Scale * picPattern.Height;
            pnlCanvas.Width = Project.Settings.Scale * picPattern.Width;

            RepaintCanvas();
        }

        private void OpenDNA()
        {
            Stop();

            DnaDrawing drawing = Serializer.DeserializeDnaDrawing(FileUtil.GetOpenFileName(FileUtil.DnaExtension));
            if (drawing != null)
            {
                if (currentDrawing == null)
                    currentDrawing = new DnaDrawing();
                lock (currentDrawing)
                {
                    currentDrawing = drawing;
                    guiDrawing = currentDrawing.Clone();
                    Project.Drawing = currentDrawing;
                }
                ResetProjectLevels();
                RepaintCanvas();
            }
        }

        private void SaveDNA()
        {
            string fileName = FileUtil.GetSaveFileName(FileUtil.DnaExtension);
            if (string.IsNullOrEmpty(fileName) == false && currentDrawing != null)
            {
                DnaDrawing clone = null;
                lock (currentDrawing)
                {
                    clone = currentDrawing.Clone();
                }
                if (clone != null)
                    Serializer.Serialize(clone, fileName);
            }
        }

        private void SaveGeneratedImage()
        {
            if (guiDrawing == null)
                return;

            string fileName = FileUtil.GetSaveFileName(FileUtil.ImgExtension);
            ImageFormat imageFormat = ImageFormat.Jpeg;
            string fileLow = fileName.ToLower();
            if (fileLow.EndsWith("bmp"))
                imageFormat = ImageFormat.Bmp;
            if (fileLow.EndsWith("gif"))
                imageFormat = ImageFormat.Gif;

            if (string.IsNullOrEmpty(fileName) == false && currentDrawing != null)
            {
                SaveVectorImage(fileName, guiDrawing, imageFormat, Project.Settings.Scale);
            }
        }

        private void SaveVectorImage(string fileName, DnaDrawing drawing, ImageFormat imageFormat, int scale)
        {
            using (
                var img = new Bitmap(scale * picPattern.Width, scale * picPattern.Height,
                                     PixelFormat.Format24bppRgb))
            {
                using (Graphics imgGfx = Graphics.FromImage(img))
                {
                    imgGfx.SmoothingMode = SmoothingMode.HighQuality;
                    Renderer.Render(drawing, imgGfx, scale);

                    try
                    {
                        img.Save(fileName, imageFormat);
                    }
                    catch (Exception ex)
                    {
                        ;
                    }
                }
            }
        }

        private void SaveAnimationImage(DnaDrawing drawing)
        {
            if (radioButtonAnimSaveNever.Checked)
                return;

            string path = projectFileName;

            if (string.IsNullOrEmpty(path))
                return;

            if (Project.Settings.HistoryImageSaveTrigger.Equals(HistorySaveTrigger.Fitness))
                if (Project.ErrorLevel > (Project.LastSavedFitness - Convert.ToDouble(numericUpDownAnimSaveSteps.Value)))
                    return;

            if (Project.Settings.HistoryImageSaveTrigger.Equals(HistorySaveTrigger.Selected))
                if (Project.Selected < (Project.LastSavedSelected + numericUpDownAnimSaveSteps.Value))
                    return;

            if (!File.Exists(path))
                return;

            path = new FileInfo(path).DirectoryName + "\\History";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path += "\\Images";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Project.LastSavedSelected = Project.Selected;
            Project.LastSavedFitness = Project.ErrorLevel;

            string fileName = path + "\\" + Project.Selected + "." + Project.Settings.HistoryImageFormat;
            SaveVectorImage(fileName, drawing, Project.Settings.HistoryImageFormat, Project.Settings.HistoryImageScale);
        }

        private void ShowSettings()
        {
            if (settingsForm != null)
                if (settingsForm.IsDisposed)
                    settingsForm = null;

            if (settingsForm == null)
            {
                settingsForm = new SettingsForm(this);
                settingsForm.Init();
            }

            settingsForm.Show();
        }

        private void ShowStats()
        {
            if (statsForm != null)
                if (statsForm.IsDisposed)
                    statsForm = null;

            if (statsForm == null)
            {
                statsForm = new StatsForm(this);
                //statsForm.Init();
            }

            statsForm.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void sourceImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void dNAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDNA();
        }

        private void dNAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveDNA();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveGeneratedImage();
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            Project.Settings.Scale = trackBarScale.Value;
            SetCanvasSize();
        }

        private void buttonSelectAnimDir_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (!dialog.ShowDialog().Equals(DialogResult.Cancel))
            {
                textBoxAnimSaveDir.Text = dialog.SelectedPath;
            }
        }

        private void comboBoxAnimSaveFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxAnimSaveFormat.SelectedIndex)
            {
                case 0:
                    Project.Settings.HistoryImageFormat = ImageFormat.Bmp;
                    break;
                case 1:
                    Project.Settings.HistoryImageFormat = ImageFormat.Gif;
                    break;
                case 2:
                    Project.Settings.HistoryImageFormat = ImageFormat.Jpeg;
                    break;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SaveProjectAs();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ShowStats();
        }

        private void radioButtonAnimSaveNever_CheckedChanged(object sender, EventArgs e)
        {
            Project.Settings.HistoryImageSaveTrigger = HistorySaveTrigger.None;
        }

        private void radioButtonAnimSaveFitness_CheckedChanged(object sender, EventArgs e)
        {
            Project.Settings.HistoryImageSaveTrigger = HistorySaveTrigger.Fitness;
        }

        private void radioButtonAnimSaveSelected_CheckedChanged(object sender, EventArgs e)
        {
            Project.Settings.HistoryImageSaveTrigger = HistorySaveTrigger.Selected;
        }

        private void numericUpDownAnimSaveSteps_ValueChanged(object sender, EventArgs e)
        {
            Project.Settings.HistoryImageSteps = numericUpDownAnimSaveSteps.Value;
        }


    }
}