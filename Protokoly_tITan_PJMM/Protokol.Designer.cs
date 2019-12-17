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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Protokol));
            this.menuStrip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsJPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formularzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formularzOdczytToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formularzFakturyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_protokol = new System.Windows.Forms.Panel();
            this.label_copyright = new System.Windows.Forms.Label();
            this.Border = new DevComponents.DotNetBar.Validator.Highlighter();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.menuStrip_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_main
            // 
            this.menuStrip_main.BackColor = System.Drawing.Color.Bisque;
            this.menuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.widokToolStripMenuItem});
            this.menuStrip_main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_main.Name = "menuStrip_main";
            this.menuStrip_main.Size = new System.Drawing.Size(923, 24);
            this.menuStrip_main.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsJPGToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "Plik";
            // 
            // saveAsJPGToolStripMenuItem
            // 
            this.saveAsJPGToolStripMenuItem.Name = "saveAsJPGToolStripMenuItem";
            this.saveAsJPGToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveAsJPGToolStripMenuItem.Text = "Zapisz jako PDF";
            this.saveAsJPGToolStripMenuItem.Click += new System.EventHandler(this.saveAsJPGToolStripMenuItem_Click);
            // 
            // widokToolStripMenuItem
            // 
            this.widokToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formularzToolStripMenuItem,
            this.formularzOdczytToolStripMenuItem,
            this.formularzFakturyToolStripMenuItem});
            this.widokToolStripMenuItem.Name = "widokToolStripMenuItem";
            this.widokToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.widokToolStripMenuItem.Text = "Widok";
            // 
            // formularzToolStripMenuItem
            // 
            this.formularzToolStripMenuItem.Name = "formularzToolStripMenuItem";
            this.formularzToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.formularzToolStripMenuItem.Text = "Formularz - protokół";
            this.formularzToolStripMenuItem.Click += new System.EventHandler(this.formularzToolStripMenuItem_Click);
            // 
            // formularzOdczytToolStripMenuItem
            // 
            this.formularzOdczytToolStripMenuItem.Name = "formularzOdczytToolStripMenuItem";
            this.formularzOdczytToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.formularzOdczytToolStripMenuItem.Text = "Formularz - odczyt";
            this.formularzOdczytToolStripMenuItem.Click += new System.EventHandler(this.formularzOdczytToolStripMenuItem_Click);
            // 
            // formularzFakturyToolStripMenuItem
            // 
            this.formularzFakturyToolStripMenuItem.Name = "formularzFakturyToolStripMenuItem";
            this.formularzFakturyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.formularzFakturyToolStripMenuItem.Text = "Formularz - faktury";
            this.formularzFakturyToolStripMenuItem.Click += new System.EventHandler(this.formularzFakturyToolStripMenuItem_Click);
            // 
            // panel_protokol
            // 
            this.panel_protokol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel_protokol.AutoScroll = true;
            this.panel_protokol.BackColor = System.Drawing.Color.White;
            this.panel_protokol.Location = new System.Drawing.Point(8, 28);
            this.panel_protokol.Name = "panel_protokol";
            this.panel_protokol.Size = new System.Drawing.Size(907, 654);
            this.panel_protokol.TabIndex = 0;
            // 
            // label_copyright
            // 
            this.label_copyright.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_copyright.AutoSize = true;
            this.label_copyright.Location = new System.Drawing.Point(290, 688);
            this.label_copyright.Name = "label_copyright";
            this.label_copyright.Size = new System.Drawing.Size(349, 13);
            this.label_copyright.TabIndex = 5;
            this.label_copyright.Text = "Application created by tITan PJMM Sp. z o. o. © 2019 All rights reserved";
            // 
            // Border
            // 
            this.Border.ContainerControl = this;
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Protokol
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(923, 705);
            this.Controls.Add(this.label_copyright);
            this.Controls.Add(this.panel_protokol);
            this.Controls.Add(this.menuStrip_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_main;
            this.Name = "Protokol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "titan PJMM Sp. z o. o. - aplikacja do tworzenia protokołów";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Protokol_Paint);
            this.menuStrip_main.ResumeLayout(false);
            this.menuStrip_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip_main;
        private System.Windows.Forms.Panel panel_protokol;
        private System.Windows.Forms.Label label_copyright;
        private System.Windows.Forms.ToolStripMenuItem widokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formularzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formularzOdczytToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsJPGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formularzFakturyToolStripMenuItem;
        private DevComponents.DotNetBar.Validator.Highlighter Border;
        private DevComponents.DotNetBar.StyleManager styleManager1;
    }
}

