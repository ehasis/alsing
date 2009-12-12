using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using GenArt.Classes;

namespace GenArt
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            SetMutationRatePolygonTabPageDataBindings();
        }

        private MainForm mainForm;
        private Settings copy = new Settings();

        public void Init()
        {
            Settings.CopyTo(copy);
        }

        private Settings Settings
        {
            get { return mainForm.Project.Settings; }
            set { mainForm.Project.Settings = value; }
        }

        private void ApplySettings()
        {
            copy.CopyTo(Settings);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = Serializer.DeserializeSettings(FileUtil.GetOpenFileName(FileUtil.XmlExtension));
            if (settings != null)
            {
                Settings = settings;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Serializer.Serialize(Settings, FileUtil.GetSaveFileName(FileUtil.XmlExtension));
        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void resetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.Reset();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ApplySettings();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetDataBindings()
        {
            numericUpDownAddPolygonMutationRate.DataBindings.Clear();
            trackBarAddPolygonMutationRate.DataBindings.Clear();
            numericUpDownRemovePolygonMutationRate.DataBindings.Clear();
            trackBarRemovePolygonMutationRate.DataBindings.Clear();
            numericUpDownMovePolygonMutationRate.DataBindings.Clear();
            trackBarMovePolygonMutationRate.DataBindings.Clear();

            numericUpDownAddPointMutationRate.DataBindings.Clear();
            trackBarAddPointMutationRate.DataBindings.Clear();
            numericUpDownRemovePointMutationRate.DataBindings.Clear();
            trackBarRemovePointMutationRate.DataBindings.Clear();
            numericUpDownMovePointMinMutationRate.DataBindings.Clear();
            trackBarMovePointMinMutationRate.DataBindings.Clear();
            numericUpDownMovePointMidMutationRate.DataBindings.Clear();
            trackBarMovePointMidMutationRate.DataBindings.Clear();
            numericUpDownMovePointMaxMutationRate.DataBindings.Clear();
            trackBarMovePointMaxMutationRate.DataBindings.Clear();
            
            numericUpDownRedMutationRate.DataBindings.Clear();
            trackBarRedMutationRate.DataBindings.Clear();
            numericUpDownGreenMutationRate.DataBindings.Clear();
            trackBarGreenMutationRate.DataBindings.Clear();
            numericUpDownBlueMutationRate.DataBindings.Clear();
            trackBarBlueMutationRate.DataBindings.Clear();
            numericUpDownAlphaMutationRate.DataBindings.Clear();
            trackBarAlphaMutationRate.DataBindings.Clear();
            
            numericUpDownPolygonsMin.DataBindings.Clear();
            trackBarPolygonsMin.DataBindings.Clear();
            numericUpDownPolygonsMax.DataBindings.Clear();
            trackBarPolygonsMax.DataBindings.Clear();
            
            numericUpDownPointsPerPolygonMin.DataBindings.Clear();
            trackBarPointsPerPolygonMin.DataBindings.Clear();
            numericUpDownPointsPerPolygonMax.DataBindings.Clear();
            trackBarPointsPerPolygonMax.DataBindings.Clear();
            
            numericUpDownPointsMin.DataBindings.Clear();
            trackBarPointsMin.DataBindings.Clear();
            numericUpDownPointsMax.DataBindings.Clear();
            trackBarPointsMax.DataBindings.Clear();
            
            numericUpDownMovePointRangeMin.DataBindings.Clear();
            trackBarMovePointRangeMin.DataBindings.Clear();
            numericUpDownMovePointRangeMid.DataBindings.Clear();
            trackBarMovePointRangeMid.DataBindings.Clear();
            
            numericUpDownRedRangeMin.DataBindings.Clear();
            trackBarRedRangeMin.DataBindings.Clear();
            numericUpDownRedRangeMax.DataBindings.Clear();
            trackBarRedRangeMax.DataBindings.Clear();
            
            numericUpDownGreenRangeMin.DataBindings.Clear();
            trackBarGreenRangeMin.DataBindings.Clear();
            numericUpDownGreenRangeMax.DataBindings.Clear();
            trackBarGreenRangeMax.DataBindings.Clear();
            
            numericUpDownBlueRangeMin.DataBindings.Clear();
            trackBarBlueRangeMin.DataBindings.Clear();
            numericUpDownBlueRangeMax.DataBindings.Clear();
            trackBarBlueRangeMax.DataBindings.Clear();
            
            numericUpDownAlphaRangeMin.DataBindings.Clear();
            trackBarAlphaRangeMin.DataBindings.Clear();
            numericUpDownAlphaRangeMax.DataBindings.Clear();
            trackBarAlphaRangeMax.DataBindings.Clear();            
        }

        private void SetMutationRatePolygonTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownAddPolygonMutationRate.DataBindings.Add("Value", Settings, "AddPolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAddPolygonMutationRate.DataBindings.Add("Value", Settings, "AddPolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownRemovePolygonMutationRate.DataBindings.Add("Value", Settings, "RemovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRemovePolygonMutationRate.DataBindings.Add("Value", Settings, "RemovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePolygonMutationRate.DataBindings.Add("Value", Settings, "MovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePolygonMutationRate.DataBindings.Add("Value", Settings, "MovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetMutationRatePointTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownAddPointMutationRate.DataBindings.Add("Value", Settings, "AddPointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAddPointMutationRate.DataBindings.Add("Value", Settings, "AddPointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownRemovePointMutationRate.DataBindings.Add("Value", Settings, "RemovePointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRemovePointMutationRate.DataBindings.Add("Value", Settings, "RemovePointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointMinMutationRate.DataBindings.Add("Value", Settings, "MovePointMinMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointMinMutationRate.DataBindings.Add("Value", Settings, "MovePointMinMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointMidMutationRate.DataBindings.Add("Value", Settings, "MovePointMidMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointMidMutationRate.DataBindings.Add("Value", Settings, "MovePointMidMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointMaxMutationRate.DataBindings.Add("Value", Settings, "MovePointMaxMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointMaxMutationRate.DataBindings.Add("Value", Settings, "MovePointMaxMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetMutationRateColorTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownRedMutationRate.DataBindings.Add("Value", Settings, "RedMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRedMutationRate.DataBindings.Add("Value", Settings, "RedMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownGreenMutationRate.DataBindings.Add("Value", Settings, "GreenMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarGreenMutationRate.DataBindings.Add("Value", Settings, "GreenMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownBlueMutationRate.DataBindings.Add("Value", Settings, "BlueMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarBlueMutationRate.DataBindings.Add("Value", Settings, "BlueMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownAlphaMutationRate.DataBindings.Add("Value", Settings, "AlphaMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAlphaMutationRate.DataBindings.Add("Value", Settings, "AlphaMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetRangePolygonTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownPolygonsMin.DataBindings.Add("Value", Settings, "PolygonsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPolygonsMin.DataBindings.Add("Value", Settings, "PolygonsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownPolygonsMax.DataBindings.Add("Value", Settings, "PolygonsMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPolygonsMax.DataBindings.Add("Value", Settings, "PolygonsMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownPointsPerPolygonMin.DataBindings.Add("Value", Settings, "PointsPerPolygonMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsPerPolygonMin.DataBindings.Add("Value", Settings, "PointsPerPolygonMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownPointsPerPolygonMax.DataBindings.Add("Value", Settings, "PointsPerPolygonMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsPerPolygonMax.DataBindings.Add("Value", Settings, "PointsPerPolygonMax", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetRangePointTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownPointsMin.DataBindings.Add("Value", Settings, "PointsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsMin.DataBindings.Add("Value", Settings, "PointsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownPointsMax.DataBindings.Add("Value", Settings, "PointsMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsMax.DataBindings.Add("Value", Settings, "PointsMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownMovePointRangeMin.DataBindings.Add("Value", Settings, "MovePointRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointRangeMin.DataBindings.Add("Value", Settings, "MovePointRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointRangeMid.DataBindings.Add("Value", Settings, "MovePointRangeMid", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointRangeMid.DataBindings.Add("Value", Settings, "MovePointRangeMid", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetRangeColorTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownRedRangeMin.DataBindings.Add("Value", Settings, "RedRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRedRangeMin.DataBindings.Add("Value", Settings, "RedRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownRedRangeMax.DataBindings.Add("Value", Settings, "RedRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRedRangeMax.DataBindings.Add("Value", Settings, "RedRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownGreenRangeMin.DataBindings.Add("Value", Settings, "GreenRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarGreenRangeMin.DataBindings.Add("Value", Settings, "GreenRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownGreenRangeMax.DataBindings.Add("Value", Settings, "GreenRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarGreenRangeMax.DataBindings.Add("Value", Settings, "GreenRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownBlueRangeMin.DataBindings.Add("Value", Settings, "BlueRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarBlueRangeMin.DataBindings.Add("Value", Settings, "BlueRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownBlueRangeMax.DataBindings.Add("Value", Settings, "BlueRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarBlueRangeMax.DataBindings.Add("Value", Settings, "BlueRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownAlphaRangeMin.DataBindings.Add("Value", Settings, "AlphaRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAlphaRangeMin.DataBindings.Add("Value", Settings, "AlphaRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownAlphaRangeMax.DataBindings.Add("Value", Settings, "AlphaRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAlphaRangeMax.DataBindings.Add("Value", Settings, "AlphaRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            SetMutationRatePolygonTabPageDataBindings();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            SetMutationRatePointTabPageDataBindings();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            SetMutationRateColorTabPageDataBindings();
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            SetRangePolygonTabPageDataBindings();
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            SetRangePointTabPageDataBindings();
        }

        private void tabPage8_Enter(object sender, EventArgs e)
        {
            SetRangeColorTabPageDataBindings();
        }
    }
}
