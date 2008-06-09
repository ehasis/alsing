
using System.Drawing;

using Alsing.SourceCode;
using Alsing.Windows.Forms;


namespace MDIDemo
{
	/// <summary>
	/// Summary description for SettingsForm.
	/// </summary>
	public class SettingsForm : System.Windows.Forms.Form
	{
		private SyntaxBoxControl		sb=null;
		private System.Windows.Forms.PropertyGrid pgStyles;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.ListBox lbStyles;
		private System.Windows.Forms.Label lblPreview;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.ComponentModel.IContainer components;

		public SettingsForm(SyntaxBoxControl sb)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.sb=sb;

			SyntaxDefinition l= sb.Document.Parser.SyntaxDefinition ;
			TextStyle[] tss=l.Styles ;

			

			lbStyles.Items.Clear ();
			for (int i=0;i<tss.Length;i++)
			{
				TextStyle ts=tss[i];
				var s=new Style {TextStyle = ts, Name = ts.Name};
				//s.Name=ts.Name;
			    lbStyles.Items.Add (s);
			}
			lbStyles.SelectedIndex =0;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pgStyles = new System.Windows.Forms.PropertyGrid();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblPreview = new System.Windows.Forms.Label();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.lbStyles = new System.Windows.Forms.ListBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pgStyles
			// 
			this.pgStyles.CommandsVisibleIfAvailable = true;
			this.pgStyles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgStyles.HelpVisible = false;
			this.pgStyles.LargeButtons = false;
			this.pgStyles.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgStyles.Name = "pgStyles";
			this.pgStyles.Size = new System.Drawing.Size(304, 265);
			this.pgStyles.TabIndex = 0;
			this.pgStyles.Text = "propertyGrid1";
			this.pgStyles.ToolbarVisible = false;
			this.pgStyles.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgStyles.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.pgStyles.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgStyles_PropertyValueChanged);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(189, 7);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(6, 265);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.splitter2,
																				 this.lblPreview,
																				 this.pgStyles});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(195, 7);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(304, 265);
			this.panel1.TabIndex = 3;
			// 
			// lblPreview
			// 
			this.lblPreview.BackColor = System.Drawing.SystemColors.Window;
			this.lblPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblPreview.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPreview.Location = new System.Drawing.Point(0, 169);
			this.lblPreview.Name = "lblPreview";
			this.lblPreview.Size = new System.Drawing.Size(304, 96);
			this.lblPreview.TabIndex = 3;
			this.lblPreview.Text = "The quick brown fox jumped over the cliff";
			this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 163);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(304, 6);
			this.splitter2.TabIndex = 4;
			this.splitter2.TabStop = false;
			// 
			// lbStyles
			// 
			this.lbStyles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbStyles.IntegralHeight = false;
			this.lbStyles.Location = new System.Drawing.Point(7, 7);
			this.lbStyles.Name = "lbStyles";
			this.lbStyles.Size = new System.Drawing.Size(182, 265);
			this.lbStyles.Sorted = true;
			this.lbStyles.TabIndex = 4;
			this.lbStyles.SelectedIndexChanged += new System.EventHandler(this.lbStyles_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.btnCancel,
																				 this.btnOK});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(7, 272);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(492, 40);
			this.panel2.TabIndex = 5;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(408, 9);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(320, 9);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(506, 319);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lbStyles,
																		  this.splitter1,
																		  this.panel1,
																		  this.panel2});
			this.DockPadding.All = 7;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.Text = "SettingsForm";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void SettingsForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void lbStyles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			pgStyles.SelectedObject = lbStyles.SelectedItem;
			Style st=lbStyles.SelectedItem as Style;
			PreviewStyle (st);
		}

		private void pgStyles_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			Style st=lbStyles.SelectedItem as Style;
			PreviewStyle (st);
			sb.Refresh ();
		}

		private void PreviewStyle(Style s)
		{
			lblPreview.ForeColor =s.ForeColor;
			if (s.BackColor != Color.Transparent)
				lblPreview.BackColor =s.BackColor;
			else
				lblPreview.BackColor = sb.BackColor;


			FontStyle fs=FontStyle.Regular;
			if (s.FontBold)
				fs |= FontStyle.Bold;
			if (s.FontItalic)
				fs |= FontStyle.Italic;
			if (s.FontUnderline)
				fs |= FontStyle.Underline;

			lblPreview.Font =new Font ("Courier New",11f,fs);
		}
	}
}
