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

namespace GenArt
{
    public partial class MainForm : Form
    {
        public static Settings Settings;
        private ImageFormat animFormat = ImageFormat.Jpeg;
        private DnaDrawing currentDrawing;


        private int generation;
        private DnaDrawing guiDrawing;
        private bool isRunning;
        private DateTime lastRepaint = DateTime.MinValue;
        private double lastSavedFitness = Double.MaxValue;
        private int lastSavedSelected;
        private int lastSelected;
        private TimeSpan repaintIntervall = new TimeSpan(0, 0, 0, 0, 0);
        private int repaintOnSelectedSteps = 3;
        private int scale;
        private int selected;
        private SettingsForm settingsForm;

        private Thread thread;

        public MainForm()
        {
            InitializeComponent();
            Settings = Serializer.DeserializeSettings();
            if (Settings == null)
                Settings = new Settings();
            scale = trackBarScale.Value;
            comboBoxAnimSaveFormat.SelectedIndex = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void StartEvolution()
        {
            IEvolutionJob job = new DefaultEvolutionJob(SetupSourceColorMatrix());
            while (isRunning)
            {
                currentDrawing = job.GetBestDrawing();
            }
        }

        //covnerts the source image to a Color[,] for faster lookup
        private Color[,] SetupSourceColorMatrix()
        {
            var sourceColors = new Color[Tools.MaxWidth,Tools.MaxHeight];
            var sourceImage = picPattern.Image as Bitmap;

            if (sourceImage == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            for (int y = 0; y < Tools.MaxHeight; y++)
            {
                for (int x = 0; x < Tools.MaxWidth; x++)
                {
                    Color c = sourceImage.GetPixel(x, y);
                    sourceColors[x, y] = c;
                }
            }

            return sourceColors;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning)
                Stop();
            else
                Start();
        }

        private void Start()
        {
            btnStart.Text = "Stop";
            isRunning = true;
            tmrRedraw.Enabled = true;

            if (thread != null)
                KillThread();

            thread = new Thread(StartEvolution)
                         {
                             IsBackground = true,
                             Priority = ThreadPriority.AboveNormal
                         };

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
            if (isRunning)
                KillThread();

            btnStart.Text = "Start";
            isRunning = false;
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
            lastSelected = selected;
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            if (currentDrawing == null)
                return;

            int polygons = currentDrawing.Polygons.Count;
            int points = currentDrawing.PointCount;
            double avg = 0;
            if (polygons != 0)
                avg = points/polygons;

            toolStripStatusLabelFitness.Text = currentDrawing.ErrorLevel.ToString();
            toolStripStatusLabelGeneration.Text = generation.ToString();
            toolStripStatusLabelSelected.Text = selected.ToString();
            toolStripStatusLabelPoints.Text = points.ToString();
            toolStripStatusLabelPolygons.Text = polygons.ToString();
            toolStripStatusLabelAvgPoints.Text = avg.ToString();

            bool shouldRepaint = false;
            if (repaintIntervall.Ticks > 0)
                if (lastRepaint < DateTime.Now - repaintIntervall)
                    shouldRepaint = true;

            if (repaintOnSelectedSteps > 0)
                if (lastSelected + repaintOnSelectedSteps < selected)
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
                var backBuffer = new Bitmap(scale*picPattern.Width, scale*picPattern.Height,
                                            PixelFormat.Format24bppRgb))
            using (Graphics backGraphics = Graphics.FromImage(backBuffer))
            {
                backGraphics.SmoothingMode = SmoothingMode.HighQuality;
                Renderer.Render(guiDrawing, backGraphics, scale);

                e.Graphics.DrawImage(backBuffer, 0, 0);
            }
        }

        private void OpenImage()
        {
            Stop();

            string fileName = FileUtil.GetOpenFileName(FileUtil.ImgExtension);
            if (string.IsNullOrEmpty(fileName))
                return;

            picPattern.Image = Image.FromFile(fileName);

            Tools.MaxHeight = picPattern.Height;
            Tools.MaxWidth = picPattern.Width;

            SetCanvasSize();

            splitContainer1.SplitterDistance = picPattern.Width + 30;
        }

        private void SetCanvasSize()
        {
            pnlCanvas.Height = scale*picPattern.Height;
            pnlCanvas.Width = scale*picPattern.Width;

            RepaintCanvas();
        }

        private void OpenDNA()
        {
            Stop();

            DnaDrawing drawing = Serializer.DeserializeDnaDrawing(FileUtil.GetOpenFileName(FileUtil.DnaExtension));
            if (drawing != null)
            {
                //TODO: why?
                //if (currentDrawing == null)
                //    currentDrawing = GetNewInitializedDrawing();

                lock (currentDrawing)
                {
                    currentDrawing = drawing;
                    guiDrawing = currentDrawing.Clone();
                }
                pnlCanvas.Invalidate();
                lastRepaint = DateTime.Now;
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
                SaveVectorImage(fileName, guiDrawing, imageFormat);
            }
        }

        private void SaveVectorImage(string fileName, DnaDrawing drawing, ImageFormat imageFormat)
        {
            using (
                var img = new Bitmap(scale*picPattern.Width, scale*picPattern.Height,
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

            string path = textBoxAnimSaveDir.Text;

            if (string.IsNullOrEmpty(path))
                return;

            if (radioButtonAnimSaveFitness.Checked)
                if (currentDrawing.ErrorLevel > (lastSavedFitness - Convert.ToDouble(numericUpDownAnimSaveSteps.Value)))
                    return;

            if (radioButtonAnimSaveSelected.Checked)
                if (selected < (lastSavedSelected + numericUpDownAnimSaveSteps.Value))
                    return;

            if (!Directory.Exists(path))
                return;

            lastSavedSelected = selected;
            lastSavedFitness = currentDrawing.ErrorLevel;

            string fileName = path + "\\" + selected + "." + animFormat;
            SaveVectorImage(fileName, drawing, animFormat);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settingsForm != null)
                if (settingsForm.IsDisposed)
                    settingsForm = null;

            if (settingsForm == null)
                settingsForm = new SettingsForm();

            settingsForm.Show();
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
            scale = trackBarScale.Value;
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
                    animFormat = ImageFormat.Bmp;
                    break;
                case 1:
                    animFormat = ImageFormat.Gif;
                    break;
                case 2:
                    animFormat = ImageFormat.Jpeg;
                    break;
            }
        }
    }
}