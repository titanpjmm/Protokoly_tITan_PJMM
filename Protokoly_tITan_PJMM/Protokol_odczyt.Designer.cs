namespace Protokoly_tITan_PJMM
{
    partial class Protokol_odczyt
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_Filtr = new System.Windows.Forms.ComboBox();
            this.radioButton_rosnaco = new System.Windows.Forms.RadioButton();
            this.radioButton_malejaco = new System.Windows.Forms.RadioButton();
            this.groupBox_filtr = new System.Windows.Forms.GroupBox();
            this.textBox_data_uplyw = new System.Windows.Forms.TextBox();
            this.textBox_data_oddania = new System.Windows.Forms.TextBox();
            this.textBox_numer_zlecenia = new System.Windows.Forms.TextBox();
            this.textBox_nazwa_klienta = new System.Windows.Forms.TextBox();
            this.textBox_numer_nip = new System.Windows.Forms.TextBox();
            this.groupBox_status_zgl = new System.Windows.Forms.GroupBox();
            this.checkBox_zaplacono = new System.Windows.Forms.CheckBox();
            this.checkBox_wykonano = new System.Windows.Forms.CheckBox();
            this.checkBox_przyjeto = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_nr_zlecenia = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_nazwa_klienta = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_I_linia = new System.Windows.Forms.Label();
            this.label_kod_miasto = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_termin_zlecenia = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_data_godzina_przyjecia = new System.Windows.Forms.Label();
            this.groupBox_opis = new System.Windows.Forms.GroupBox();
            this.richTextBox_opis = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label_nip = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label_tel_sluzbowy = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label_tel_prywatny = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label_mail = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label_typ_urzadzenia = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label_szacowany_koszt = new System.Windows.Forms.Label();
            this.groupBox_filtr.SuspendLayout();
            this.groupBox_status_zgl.SuspendLayout();
            this.groupBox_opis.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Filtr
            // 
            this.comboBox_Filtr.FormattingEnabled = true;
            this.comboBox_Filtr.Items.AddRange(new object[] {
            "Data upływu terminu",
            "Data wpływu zlecenia",
            "Numer zlecenia",
            "Numer NIP",
            "Nazwa klienta",
            "Status zlecenia"});
            this.comboBox_Filtr.Location = new System.Drawing.Point(6, 43);
            this.comboBox_Filtr.Name = "comboBox_Filtr";
            this.comboBox_Filtr.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Filtr.TabIndex = 0;
            this.comboBox_Filtr.Text = "Wybierz filtr";
            this.comboBox_Filtr.SelectedIndexChanged += new System.EventHandler(this.Filtr_SelectedIndexChanged);
            // 
            // radioButton_rosnaco
            // 
            this.radioButton_rosnaco.AutoSize = true;
            this.radioButton_rosnaco.Checked = true;
            this.radioButton_rosnaco.Location = new System.Drawing.Point(137, 33);
            this.radioButton_rosnaco.Name = "radioButton_rosnaco";
            this.radioButton_rosnaco.Size = new System.Drawing.Size(63, 17);
            this.radioButton_rosnaco.TabIndex = 2;
            this.radioButton_rosnaco.TabStop = true;
            this.radioButton_rosnaco.Text = "rosnąco";
            this.radioButton_rosnaco.UseVisualStyleBackColor = true;
            // 
            // radioButton_malejaco
            // 
            this.radioButton_malejaco.AutoSize = true;
            this.radioButton_malejaco.Location = new System.Drawing.Point(137, 56);
            this.radioButton_malejaco.Name = "radioButton_malejaco";
            this.radioButton_malejaco.Size = new System.Drawing.Size(67, 17);
            this.radioButton_malejaco.TabIndex = 3;
            this.radioButton_malejaco.Text = "malejąco";
            this.radioButton_malejaco.UseVisualStyleBackColor = true;
            // 
            // groupBox_filtr
            // 
            this.groupBox_filtr.Controls.Add(this.comboBox_Filtr);
            this.groupBox_filtr.Controls.Add(this.radioButton_malejaco);
            this.groupBox_filtr.Controls.Add(this.radioButton_rosnaco);
            this.groupBox_filtr.Location = new System.Drawing.Point(20, 36);
            this.groupBox_filtr.Name = "groupBox_filtr";
            this.groupBox_filtr.Size = new System.Drawing.Size(210, 88);
            this.groupBox_filtr.TabIndex = 4;
            this.groupBox_filtr.TabStop = false;
            this.groupBox_filtr.Text = "Filtr wyświetlania";
            // 
            // textBox_data_uplyw
            // 
            this.textBox_data_uplyw.Location = new System.Drawing.Point(236, 36);
            this.textBox_data_uplyw.Name = "textBox_data_uplyw";
            this.textBox_data_uplyw.Size = new System.Drawing.Size(151, 20);
            this.textBox_data_uplyw.TabIndex = 5;
            // 
            // textBox_data_oddania
            // 
            this.textBox_data_oddania.Location = new System.Drawing.Point(236, 68);
            this.textBox_data_oddania.Name = "textBox_data_oddania";
            this.textBox_data_oddania.Size = new System.Drawing.Size(151, 20);
            this.textBox_data_oddania.TabIndex = 6;
            // 
            // textBox_numer_zlecenia
            // 
            this.textBox_numer_zlecenia.Location = new System.Drawing.Point(236, 104);
            this.textBox_numer_zlecenia.Name = "textBox_numer_zlecenia";
            this.textBox_numer_zlecenia.Size = new System.Drawing.Size(151, 20);
            this.textBox_numer_zlecenia.TabIndex = 7;
            // 
            // textBox_nazwa_klienta
            // 
            this.textBox_nazwa_klienta.Location = new System.Drawing.Point(393, 36);
            this.textBox_nazwa_klienta.Name = "textBox_nazwa_klienta";
            this.textBox_nazwa_klienta.Size = new System.Drawing.Size(151, 20);
            this.textBox_nazwa_klienta.TabIndex = 8;
            // 
            // textBox_numer_nip
            // 
            this.textBox_numer_nip.Location = new System.Drawing.Point(393, 104);
            this.textBox_numer_nip.Name = "textBox_numer_nip";
            this.textBox_numer_nip.Size = new System.Drawing.Size(151, 20);
            this.textBox_numer_nip.TabIndex = 9;
            // 
            // groupBox_status_zgl
            // 
            this.groupBox_status_zgl.Controls.Add(this.checkBox_zaplacono);
            this.groupBox_status_zgl.Controls.Add(this.checkBox_wykonano);
            this.groupBox_status_zgl.Controls.Add(this.checkBox_przyjeto);
            this.groupBox_status_zgl.Location = new System.Drawing.Point(550, 36);
            this.groupBox_status_zgl.Name = "groupBox_status_zgl";
            this.groupBox_status_zgl.Size = new System.Drawing.Size(122, 88);
            this.groupBox_status_zgl.TabIndex = 10;
            this.groupBox_status_zgl.TabStop = false;
            this.groupBox_status_zgl.Text = "Status zgłoszenia";
            // 
            // checkBox_zaplacono
            // 
            this.checkBox_zaplacono.AutoSize = true;
            this.checkBox_zaplacono.Location = new System.Drawing.Point(6, 65);
            this.checkBox_zaplacono.Name = "checkBox_zaplacono";
            this.checkBox_zaplacono.Size = new System.Drawing.Size(79, 17);
            this.checkBox_zaplacono.TabIndex = 2;
            this.checkBox_zaplacono.Text = "Zapłacono";
            this.checkBox_zaplacono.UseVisualStyleBackColor = true;
            // 
            // checkBox_wykonano
            // 
            this.checkBox_wykonano.AutoSize = true;
            this.checkBox_wykonano.Location = new System.Drawing.Point(6, 43);
            this.checkBox_wykonano.Name = "checkBox_wykonano";
            this.checkBox_wykonano.Size = new System.Drawing.Size(78, 17);
            this.checkBox_wykonano.TabIndex = 1;
            this.checkBox_wykonano.Text = "Wykonano";
            this.checkBox_wykonano.UseVisualStyleBackColor = true;
            // 
            // checkBox_przyjeto
            // 
            this.checkBox_przyjeto.AutoSize = true;
            this.checkBox_przyjeto.Location = new System.Drawing.Point(6, 21);
            this.checkBox_przyjeto.Name = "checkBox_przyjeto";
            this.checkBox_przyjeto.Size = new System.Drawing.Size(63, 17);
            this.checkBox_przyjeto.TabIndex = 0;
            this.checkBox_przyjeto.Text = "Przyjęto";
            this.checkBox_przyjeto.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(678, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(210, 88);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Numer zlecenia:";
            // 
            // label_nr_zlecenia
            // 
            this.label_nr_zlecenia.AutoSize = true;
            this.label_nr_zlecenia.Location = new System.Drawing.Point(121, 149);
            this.label_nr_zlecenia.Name = "label_nr_zlecenia";
            this.label_nr_zlecenia.Size = new System.Drawing.Size(62, 13);
            this.label_nr_zlecenia.TabIndex = 14;
            this.label_nr_zlecenia.Text = "00000/yyyy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Nazwa klienta:";
            // 
            // label_nazwa_klienta
            // 
            this.label_nazwa_klienta.AutoSize = true;
            this.label_nazwa_klienta.Location = new System.Drawing.Point(121, 183);
            this.label_nazwa_klienta.Name = "label_nazwa_klienta";
            this.label_nazwa_klienta.Size = new System.Drawing.Size(80, 13);
            this.label_nazwa_klienta.TabIndex = 16;
            this.label_nazwa_klienta.Text = "Imię i Nazwisko";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Adres:";
            // 
            // label_I_linia
            // 
            this.label_I_linia.AutoSize = true;
            this.label_I_linia.Location = new System.Drawing.Point(23, 276);
            this.label_I_linia.Name = "label_I_linia";
            this.label_I_linia.Size = new System.Drawing.Size(31, 13);
            this.label_I_linia.TabIndex = 18;
            this.label_I_linia.Text = "I linia";
            // 
            // label_kod_miasto
            // 
            this.label_kod_miasto.AutoSize = true;
            this.label_kod_miasto.Location = new System.Drawing.Point(23, 304);
            this.label_kod_miasto.Name = "label_kod_miasto";
            this.label_kod_miasto.Size = new System.Drawing.Size(111, 13);
            this.label_kod_miasto.TabIndex = 19;
            this.label_kod_miasto.Text = "Kod pocztowy, Miasto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(547, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Termin zlecenia:";
            // 
            // label_termin_zlecenia
            // 
            this.label_termin_zlecenia.AutoSize = true;
            this.label_termin_zlecenia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_termin_zlecenia.ForeColor = System.Drawing.Color.Red;
            this.label_termin_zlecenia.Location = new System.Drawing.Point(690, 149);
            this.label_termin_zlecenia.Name = "label_termin_zlecenia";
            this.label_termin_zlecenia.Size = new System.Drawing.Size(79, 15);
            this.label_termin_zlecenia.TabIndex = 21;
            this.label_termin_zlecenia.Text = "dd/MM/yyyy";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(547, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Data i godzina przyjęcia:";
            // 
            // label_data_godzina_przyjecia
            // 
            this.label_data_godzina_przyjecia.AutoSize = true;
            this.label_data_godzina_przyjecia.Location = new System.Drawing.Point(690, 183);
            this.label_data_godzina_przyjecia.Name = "label_data_godzina_przyjecia";
            this.label_data_godzina_przyjecia.Size = new System.Drawing.Size(128, 13);
            this.label_data_godzina_przyjecia.TabIndex = 23;
            this.label_data_godzina_przyjecia.Text = "yyyy - MM - dd hh: mm: ss";
            // 
            // groupBox_opis
            // 
            this.groupBox_opis.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox_opis.Controls.Add(this.richTextBox_opis);
            this.groupBox_opis.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox_opis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox_opis.Location = new System.Drawing.Point(14, 324);
            this.groupBox_opis.Name = "groupBox_opis";
            this.groupBox_opis.Size = new System.Drawing.Size(874, 127);
            this.groupBox_opis.TabIndex = 24;
            this.groupBox_opis.TabStop = false;
            this.groupBox_opis.Text = "Opis uszkodzeń sprzętu";
            // 
            // richTextBox_opis
            // 
            this.richTextBox_opis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.richTextBox_opis.BackColor = System.Drawing.Color.White;
            this.richTextBox_opis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox_opis.Location = new System.Drawing.Point(7, 21);
            this.richTextBox_opis.MaximumSize = new System.Drawing.Size(915, 87);
            this.richTextBox_opis.MaxLength = 530;
            this.richTextBox_opis.Name = "richTextBox_opis";
            this.richTextBox_opis.Size = new System.Drawing.Size(861, 87);
            this.richTextBox_opis.TabIndex = 0;
            this.richTextBox_opis.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "NIP:";
            // 
            // label_nip
            // 
            this.label_nip.AutoSize = true;
            this.label_nip.Location = new System.Drawing.Point(121, 217);
            this.label_nip.Name = "label_nip";
            this.label_nip.Size = new System.Drawing.Size(66, 13);
            this.label_nip.TabIndex = 26;
            this.label_nip.Text = "xxx-xxx-xx-xx";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(260, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "tel. służbowy:";
            // 
            // label_tel_sluzbowy
            // 
            this.label_tel_sluzbowy.AutoSize = true;
            this.label_tel_sluzbowy.Location = new System.Drawing.Point(355, 149);
            this.label_tel_sluzbowy.Name = "label_tel_sluzbowy";
            this.label_tel_sluzbowy.Size = new System.Drawing.Size(58, 13);
            this.label_tel_sluzbowy.TabIndex = 28;
            this.label_tel_sluzbowy.Text = "xxx-xxx-xxx";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(260, 183);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "tel. prywartny:";
            // 
            // label_tel_prywatny
            // 
            this.label_tel_prywatny.AutoSize = true;
            this.label_tel_prywatny.Location = new System.Drawing.Point(355, 183);
            this.label_tel_prywatny.Name = "label_tel_prywatny";
            this.label_tel_prywatny.Size = new System.Drawing.Size(58, 13);
            this.label_tel_prywatny.TabIndex = 30;
            this.label_tel_prywatny.Text = "xxx-xxx-xxx";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(260, 217);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 31;
            this.label18.Text = "e-mail:";
            // 
            // label_mail
            // 
            this.label_mail.AutoSize = true;
            this.label_mail.Location = new System.Drawing.Point(355, 217);
            this.label_mail.Name = "label_mail";
            this.label_mail.Size = new System.Drawing.Size(104, 13);
            this.label_mail.TabIndex = 32;
            this.label_mail.Text = "example@gmail.com";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(547, 248);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 33;
            this.label20.Text = "Typ urządzenia:";
            // 
            // label_typ_urzadzenia
            // 
            this.label_typ_urzadzenia.AutoSize = true;
            this.label_typ_urzadzenia.Location = new System.Drawing.Point(690, 248);
            this.label_typ_urzadzenia.Name = "label_typ_urzadzenia";
            this.label_typ_urzadzenia.Size = new System.Drawing.Size(72, 13);
            this.label_typ_urzadzenia.TabIndex = 34;
            this.label_typ_urzadzenia.Text = "rodzaj sprzętu";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(547, 276);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(93, 13);
            this.label22.TabIndex = 35;
            this.label22.Text = "Szacowany koszt:";
            // 
            // label_szacowany_koszt
            // 
            this.label_szacowany_koszt.AutoSize = true;
            this.label_szacowany_koszt.Location = new System.Drawing.Point(690, 276);
            this.label_szacowany_koszt.Name = "label_szacowany_koszt";
            this.label_szacowany_koszt.Size = new System.Drawing.Size(54, 13);
            this.label_szacowany_koszt.TabIndex = 36;
            this.label_szacowany_koszt.Text = "cena w zł";
            // 
            // Protokol_odczyt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label_szacowany_koszt);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label_typ_urzadzenia);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label_mail);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label_tel_prywatny);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label_tel_sluzbowy);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label_nip);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox_opis);
            this.Controls.Add(this.label_data_godzina_przyjecia);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label_termin_zlecenia);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label_kod_miasto);
            this.Controls.Add(this.label_I_linia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_nazwa_klienta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_nr_zlecenia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox_status_zgl);
            this.Controls.Add(this.textBox_numer_nip);
            this.Controls.Add(this.textBox_nazwa_klienta);
            this.Controls.Add(this.textBox_numer_zlecenia);
            this.Controls.Add(this.textBox_data_oddania);
            this.Controls.Add(this.textBox_data_uplyw);
            this.Controls.Add(this.groupBox_filtr);
            this.Name = "Protokol_odczyt";
            this.Size = new System.Drawing.Size(907, 654);
            this.groupBox_filtr.ResumeLayout(false);
            this.groupBox_filtr.PerformLayout();
            this.groupBox_status_zgl.ResumeLayout(false);
            this.groupBox_status_zgl.PerformLayout();
            this.groupBox_opis.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Filtr;
        private System.Windows.Forms.RadioButton radioButton_rosnaco;
        private System.Windows.Forms.RadioButton radioButton_malejaco;
        private System.Windows.Forms.GroupBox groupBox_filtr;
        private System.Windows.Forms.TextBox textBox_data_uplyw;
        private System.Windows.Forms.TextBox textBox_data_oddania;
        private System.Windows.Forms.TextBox textBox_numer_zlecenia;
        private System.Windows.Forms.TextBox textBox_nazwa_klienta;
        private System.Windows.Forms.TextBox textBox_numer_nip;
        private System.Windows.Forms.GroupBox groupBox_status_zgl;
        private System.Windows.Forms.CheckBox checkBox_zaplacono;
        private System.Windows.Forms.CheckBox checkBox_wykonano;
        private System.Windows.Forms.CheckBox checkBox_przyjeto;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_nr_zlecenia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_nazwa_klienta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_I_linia;
        private System.Windows.Forms.Label label_kod_miasto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_termin_zlecenia;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_data_godzina_przyjecia;
        private System.Windows.Forms.GroupBox groupBox_opis;
        private System.Windows.Forms.RichTextBox richTextBox_opis;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_nip;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_tel_sluzbowy;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_tel_prywatny;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label_mail;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label_typ_urzadzenia;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label_szacowany_koszt;
    }
}
