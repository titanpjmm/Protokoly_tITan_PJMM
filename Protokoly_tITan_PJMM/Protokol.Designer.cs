namespace Protokoly_tITan_PJMM
{
    partial class Protokol
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Protokol));
            this.menuStrip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsJPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formularzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formularzOdczytToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Border = new DevComponents.DotNetBar.Validator.Highlighter();
            this.panel_protokol = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_main
            // 
            this.menuStrip_main.BackColor = System.Drawing.Color.Silver;
            this.menuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.widokToolStripMenuItem});
            this.menuStrip_main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_main.Name = "menuStrip_main";
            this.menuStrip_main.Size = new System.Drawing.Size(984, 24);
            this.menuStrip_main.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsPDFToolStripMenuItem,
            this.saveAsJPGToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsPDFToolStripMenuItem
            // 
            this.saveAsPDFToolStripMenuItem.Name = "saveAsPDFToolStripMenuItem";
            this.saveAsPDFToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAsPDFToolStripMenuItem.Text = "Save as PDF";
            this.saveAsPDFToolStripMenuItem.Click += new System.EventHandler(this.saveAsPDFToolStripMenuItem_Click);
            // 
            // saveAsJPGToolStripMenuItem
            // 
            this.saveAsJPGToolStripMenuItem.Name = "saveAsJPGToolStripMenuItem";
            this.saveAsJPGToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAsJPGToolStripMenuItem.Text = "Save as JPG";
            this.saveAsJPGToolStripMenuItem.Click += new System.EventHandler(this.saveAsJPGToolStripMenuItem_Click);
            // 
            // widokToolStripMenuItem
            // 
            this.widokToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formularzToolStripMenuItem,
            this.formularzOdczytToolStripMenuItem});
            this.widokToolStripMenuItem.Name = "widokToolStripMenuItem";
            this.widokToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.widokToolStripMenuItem.Text = "Widok";
            // 
            // formularzToolStripMenuItem
            // 
            this.formularzToolStripMenuItem.Name = "formularzToolStripMenuItem";
            this.formularzToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.formularzToolStripMenuItem.Text = "Formularz";
            this.formularzToolStripMenuItem.Click += new System.EventHandler(this.formularzToolStripMenuItem_Click);
            // 
            // formularzOdczytToolStripMenuItem
            // 
            this.formularzOdczytToolStripMenuItem.Name = "formularzOdczytToolStripMenuItem";
            this.formularzOdczytToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.formularzOdczytToolStripMenuItem.Text = "Formularz - odczyt";
            this.formularzOdczytToolStripMenuItem.Click += new System.EventHandler(this.formularzOdczytToolStripMenuItem_Click);
            // 
            // Border
            // 
            this.Border.ContainerControl = this;
            // 
            // panel_protokol
            // 
            this.panel_protokol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel_protokol.AutoScroll = true;
            this.panel_protokol.BackColor = System.Drawing.Color.White;
            this.Border.SetHighlightColor(this.panel_protokol, DevComponents.DotNetBar.Validator.eHighlightColor.Green);
            this.panel_protokol.Location = new System.Drawing.Point(12, 27);
            this.panel_protokol.Name = "panel_protokol";
            this.panel_protokol.Size = new System.Drawing.Size(960, 736);
            this.panel_protokol.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 766);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(349, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Application created by tITan PJMM Sp. z o. o. © 2019 All rights reserved";
            // 
            // Protokol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 781);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel_protokol);
            this.Controls.Add(this.menuStrip_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_main;
            this.Name = "Protokol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "titan PJMM Sp. z o. o. - aplikacja do tworzenia protokołów";
            this.menuStrip_main.ResumeLayout(false);
            this.menuStrip_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip_main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsJPGToolStripMenuItem;
        private DevComponents.DotNetBar.Validator.Highlighter Border;
        private System.Windows.Forms.Panel panel_protokol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem widokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formularzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formularzOdczytToolStripMenuItem;
    }
}

