using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GenArt.AST;
using GenArt.Classes;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt
{
    public partial class MainForm : Form
    {
        public DnaProject Project;
        private string projectPath = "";

        private DnaDrawing currentDrawing = new DnaDrawing(); // make non null to make sure lock will always work...

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

            IEvolutionJob job = new DefaultEvolutionJob(sourceImage);
            double newErrorLevel;

            while (Project.IsRunning)
            {

                var newDrawing = job.GetBestDrawing();
                
                Project.Generations++;


                if (newDrawing.IsDirty)
                {
                    Project.Mutations++;

                    if (newDrawing.ErrorLevel <= Project.ErrorLevel)
                    {
                        Project.Selected++;

                        if (newDrawing.ErrorLevel < Project.ErrorLevel)
                            Project.Positive++;
                        else
                            Project.Neutral++;

                        lock (currentDrawing)
                        {
                            currentDrawing = newDrawing;
                            Project.Drawing = currentDrawing.Clone();
                        }

                        Project.ErrorLevel = newDrawing.ErrorLevel;
                    }
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
                Project.ElapsedTime += DateTime.Now - Project.LastStartTime;
            }

            btnStart.Text = "Start";
            Project.IsRunning = false;
            tmrRedraw.Enabled = false;
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

            if (currentDrawing.Polygons == null)
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
            projectPath = "";
        }

        private void OpenProject()
        {
            SetTitleBar();
        }

        private void SaveProject()
        {
            SetTitleBar();
        }

        private void SaveProjectAs()
        {
            SetTitleBar();
        }

        private void SetTitleBar()
        {
            this.Text = projectPath; 
        }


        private void OpenImage()
        {
            Stop();

            string fileName = FileUtil.GetOpenFileName(FileUtil.ImgExtension);
            if (string.IsNullOrEmpty(fileName))
                return;

            picPattern.Image = Image.FromFile(fileName);

            SetCanvasSize();

            splitContainer1.SplitterDistance = picPattern.Width + 30;

            Project.ImagePath = fileName;
            ResetProjectLevels();
            Project.LastSavedFitness = double.MaxValue;
            Project.LastSavedSelected = 0;
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
            Project.ElapsedTime = TimeSpan.MinValue;
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
                //TODO: why? 
                //ANSW: To be able to safely lock on currentDrawing...
                if (currentDrawing == null)
                    currentDrawing = new DnaDrawing();

                lock (currentDrawing)
                {
                    currentDrawing = drawing;
                    guiDrawing = currentDrawing.Clone();
                }
                pnlCanvas.Invalidate();
                lastRepaint = DateTime.Now;
                ResetProjectLevels();
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

            string path = Project.Settings.AnimSaveDir;

            if (string.IsNullOrEmpty(path))
                return;

            if (radioButtonAnimSaveFitness.Checked)
                if (Project.ErrorLevel > (Project.LastSavedFitness - Convert.ToDouble(numericUpDownAnimSaveSteps.Value)))
                    return;

            if (radioButtonAnimSaveSelected.Checked)
                if (Project.Selected < (Project.LastSavedSelected + numericUpDownAnimSaveSteps.Value))
                    return;

            if (!Directory.Exists(path))
                return;

            Project.LastSavedSelected = Project.Selected;
            Project.LastSavedFitness = Project.ErrorLevel;

            string fileName = path + "\\" + Project.Selected + "." + Project.Settings.AnimFormat;
            SaveVectorImage(fileName, drawing, Project.Settings.AnimFormat, Project.Settings.AnimScale);
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
                    Project.Settings.AnimFormat = ImageFormat.Bmp;
                    break;
                case 1:
                    Project.Settings.AnimFormat = ImageFormat.Gif;
                    break;
                case 2:
                    Project.Settings.AnimFormat = ImageFormat.Jpeg;
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


    }
}