using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GenArt.Core.Classes;

namespace GenArt
{
    public partial class StatsForm : Form
    {
        public StatsForm(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            Stats stats = new Stats(mainForm.Project);

            propertyGrid1.SelectedObject = stats;
        }

        private MainForm mainForm;

        internal void UpdateStats()
        {
            propertyGrid1.Refresh();
        }
    }
}
