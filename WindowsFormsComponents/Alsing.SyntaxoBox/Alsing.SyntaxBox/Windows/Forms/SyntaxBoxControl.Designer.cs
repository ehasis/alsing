namespace Alsing.Windows.Forms
{
    partial class SyntaxBoxControl
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.Resources.ResourceManager(typeof(SyntaxBoxControl));
            this._GutterIcons = new System.Windows.Forms.ImageList(components);
            this._AutoListIcons = new System.Windows.Forms.ImageList(components);
            this.ParseTimer = new System.Windows.Forms.Timer(components);
            // 
            // _GutterIcons
            // 
            this._GutterIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this._GutterIcons.ImageSize = new System.Drawing.Size(17, 17);
            this._GutterIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_GutterIcons.ImageStream")));
            this._GutterIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _AutoListIcons
            // 
            this._AutoListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._AutoListIcons.ImageSize = new System.Drawing.Size(16, 16);
            this._AutoListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_AutoListIcons.ImageStream")));
            this._AutoListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ParseTimer
            // 
            this.ParseTimer.Enabled = true;
            this.ParseTimer.Interval = 1;
            this.ParseTimer.Tick += ParseTimer_Tick;
        }

        #endregion Component Designer generated code

        private System.Windows.Forms.ImageList _AutoListIcons;
        private System.Windows.Forms.ImageList _GutterIcons;
        private System.Windows.Forms.Timer ParseTimer;
    }
}
