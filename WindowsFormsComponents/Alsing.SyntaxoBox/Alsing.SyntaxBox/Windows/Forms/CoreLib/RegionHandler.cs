// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Alsing.Windows.Forms.CoreLib
{
    [ToolboxItem(true)]
    public partial class RegionHandler : Component
    {
        private Color _TransparencyKey = Color.FromArgb(255, 0, 255);

        public Color TransparencyKey
        {
            get { return _TransparencyKey; }
            set { _TransparencyKey = value; }
        }

        public Control Control { get; set; }

        public Bitmap MaskImage { get; set; }

        public RegionHandler()
        {
            InitializeComponent();
        }

        public RegionHandler(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public void ApplyRegion(Control Target, Bitmap MaskImage, Color TransparencyKey)
        {
            Control = Target;
            this.MaskImage = MaskImage;
            this.TransparencyKey = TransparencyKey;
            ApplyRegion();
        }


        public void ApplyRegion()
        {
            var r = new Region(new Rectangle(0, 0, MaskImage.Width, MaskImage.Height));

            for (int y = 0; y < MaskImage.Height; y++)
                for (int x = 0; x < MaskImage.Width; x++)
                {
                    if (MaskImage.GetPixel(x, y) == TransparencyKey)
                    {
                        r.Exclude(new Rectangle(x, y, 1, 1));
                    }
                }

            Control.Region = r;
            Control.BackgroundImage = MaskImage;
        }
    }
}