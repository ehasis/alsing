namespace Alsing.Irc.Cleint
{
    partial class ChannelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserList = new System.Windows.Forms.ListBox();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.UserList);
            this.pnlRight.Location = new System.Drawing.Point(615, 1);
            this.pnlRight.Size = new System.Drawing.Size(181, 473);
            this.pnlRight.Visible = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(612, 1);
            this.splitter1.Size = new System.Drawing.Size(3, 473);
            this.splitter1.Visible = true;
            // 
            // UserList
            // 
            this.UserList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.UserList.FormattingEnabled = true;
            this.UserList.IntegralHeight = false;
            this.UserList.ItemHeight = 15;
            this.UserList.Location = new System.Drawing.Point(1, 1);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(179, 471);
            this.UserList.Sorted = true;
            this.UserList.TabIndex = 4;
            // 
            // ChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 492);
            this.Name = "ChannelForm";
            this.Text = "ChannelForm";
            this.Load += new System.EventHandler(this.ChannelForm_Load);
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox UserList;


    }
}