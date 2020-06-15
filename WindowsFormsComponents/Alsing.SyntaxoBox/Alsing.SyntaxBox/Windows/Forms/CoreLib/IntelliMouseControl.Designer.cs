namespace Alsing.Windows.Forms.CoreLib
{
    partial class IntelliMouseControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        protected void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntelliMouseControl));
            this.tmrFeedback = new System.Windows.Forms.Timer(this.components);
            this.picImage = new System.Windows.Forms.PictureBox();
            this.regionHandler1 = new Alsing.Windows.Forms.CoreLib.RegionHandler(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrFeedback
            // 
            this.tmrFeedback.Enabled = true;
            this.tmrFeedback.Interval = 10;
            this.tmrFeedback.Tick += new System.EventHandler(this.tmrFeedback_Tick);
            // 
            // picImage
            // 
            this.picImage.Image = ((System.Drawing.Image)(resources.GetObject("picImage.Image")));
            this.picImage.Location = new System.Drawing.Point(17, 17);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(100, 50);
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // regionHandler1
            // 
            this.regionHandler1.Control = null;
            this.regionHandler1.MaskImage = null;
            this.regionHandler1.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // IntelliMouseControl
            // 
            this.ParentChanged += new System.EventHandler(this.IntelliMouseControl_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.PictureBox picImage;
        protected System.Windows.Forms.Timer tmrFeedback;
        protected Alsing.Windows.Forms.CoreLib.RegionHandler regionHandler1;
    }
}
