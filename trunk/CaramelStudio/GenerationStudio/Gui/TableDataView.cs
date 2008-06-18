using System;
using System.Data;
using System.Windows.Forms;

namespace GenerationStudio.Gui
{
    public partial class TableDataView : UserControl
    {
        public TableDataView()
        {
            InitializeComponent();
        }

        public DataTable Data { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Grid.DataSource = Data;
        }
    }
}