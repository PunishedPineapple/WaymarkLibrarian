namespace WaymarkLibrarian
{
	partial class WaymarkLibrarianForm
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
			this.CharacterListDropdown = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CharacterFolderBrowseButton = new System.Windows.Forms.Button();
			this.CharacterDataFolderTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.GamePresetListBox = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.LibraryListBox = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SelectedPresetInfoBox = new System.Windows.Forms.TextBox();
			this.SetCharacterAliasButton = new System.Windows.Forms.Button();
			this.ClearGameSlotButton = new System.Windows.Forms.Button();
			this.CopyToLibraryButton = new System.Windows.Forms.Button();
			this.CopyToGameButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.WriteGameFileButton = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.textBox13 = new System.Windows.Forms.TextBox();
			this.textBox14 = new System.Windows.Forms.TextBox();
			this.textBox15 = new System.Windows.Forms.TextBox();
			this.textBox16 = new System.Windows.Forms.TextBox();
			this.textBox17 = new System.Windows.Forms.TextBox();
			this.textBox18 = new System.Windows.Forms.TextBox();
			this.textBox19 = new System.Windows.Forms.TextBox();
			this.textBox20 = new System.Windows.Forms.TextBox();
			this.textBox21 = new System.Windows.Forms.TextBox();
			this.textBox22 = new System.Windows.Forms.TextBox();
			this.textBox23 = new System.Windows.Forms.TextBox();
			this.textBox24 = new System.Windows.Forms.TextBox();
			this.textBox25 = new System.Windows.Forms.TextBox();
			this.textBox26 = new System.Windows.Forms.TextBox();
			this.textBox27 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.textBox28 = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.CharacterDataFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CharacterListDropdown
			// 
			this.CharacterListDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CharacterListDropdown.FormattingEnabled = true;
			this.CharacterListDropdown.Location = new System.Drawing.Point(8, 72);
			this.CharacterListDropdown.Name = "CharacterListDropdown";
			this.CharacterListDropdown.Size = new System.Drawing.Size(256, 21);
			this.CharacterListDropdown.TabIndex = 0;
			this.CharacterListDropdown.SelectedIndexChanged += new System.EventHandler(this.CharacterListDropdown_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Active Character:";
			// 
			// CharacterFolderBrowseButton
			// 
			this.CharacterFolderBrowseButton.Location = new System.Drawing.Point(272, 24);
			this.CharacterFolderBrowseButton.Name = "CharacterFolderBrowseButton";
			this.CharacterFolderBrowseButton.Size = new System.Drawing.Size(40, 23);
			this.CharacterFolderBrowseButton.TabIndex = 2;
			this.CharacterFolderBrowseButton.Text = "...";
			this.CharacterFolderBrowseButton.UseVisualStyleBackColor = true;
			this.CharacterFolderBrowseButton.Click += new System.EventHandler(this.CharacterFolderBrowseButton_Click);
			// 
			// CharacterDataFolderTextBox
			// 
			this.CharacterDataFolderTextBox.Location = new System.Drawing.Point(8, 24);
			this.CharacterDataFolderTextBox.Name = "CharacterDataFolderTextBox";
			this.CharacterDataFolderTextBox.ReadOnly = true;
			this.CharacterDataFolderTextBox.Size = new System.Drawing.Size(256, 20);
			this.CharacterDataFolderTextBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(146, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "FFXIV Character Data Folder:";
			// 
			// GamePresetListBox
			// 
			this.GamePresetListBox.FormattingEnabled = true;
			this.GamePresetListBox.Location = new System.Drawing.Point(8, 120);
			this.GamePresetListBox.Name = "GamePresetListBox";
			this.GamePresetListBox.Size = new System.Drawing.Size(256, 69);
			this.GamePresetListBox.TabIndex = 5;
			this.GamePresetListBox.SelectedIndexChanged += new System.EventHandler(this.GamePresetListBox_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Current Presets:";
			// 
			// LibraryListBox
			// 
			this.LibraryListBox.FormattingEnabled = true;
			this.LibraryListBox.Location = new System.Drawing.Point(392, 24);
			this.LibraryListBox.Name = "LibraryListBox";
			this.LibraryListBox.Size = new System.Drawing.Size(248, 381);
			this.LibraryListBox.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(392, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Preset Library:";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(392, 408);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 23);
			this.button2.TabIndex = 9;
			this.button2.Text = "Add";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(480, 408);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(72, 23);
			this.button3.TabIndex = 10;
			this.button3.Text = "Remove";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// SelectedPresetInfoBox
			// 
			this.SelectedPresetInfoBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SelectedPresetInfoBox.Location = new System.Drawing.Point(8, 248);
			this.SelectedPresetInfoBox.Multiline = true;
			this.SelectedPresetInfoBox.Name = "SelectedPresetInfoBox";
			this.SelectedPresetInfoBox.ReadOnly = true;
			this.SelectedPresetInfoBox.Size = new System.Drawing.Size(256, 176);
			this.SelectedPresetInfoBox.TabIndex = 12;
			// 
			// SetCharacterAliasButton
			// 
			this.SetCharacterAliasButton.Location = new System.Drawing.Point(272, 72);
			this.SetCharacterAliasButton.Name = "SetCharacterAliasButton";
			this.SetCharacterAliasButton.Size = new System.Drawing.Size(75, 23);
			this.SetCharacterAliasButton.TabIndex = 13;
			this.SetCharacterAliasButton.Text = "Set Alias";
			this.SetCharacterAliasButton.UseVisualStyleBackColor = true;
			this.SetCharacterAliasButton.Click += new System.EventHandler(this.SetCharacterAliasButton_Click);
			// 
			// ClearGameSlotButton
			// 
			this.ClearGameSlotButton.Location = new System.Drawing.Point(8, 192);
			this.ClearGameSlotButton.Name = "ClearGameSlotButton";
			this.ClearGameSlotButton.Size = new System.Drawing.Size(72, 23);
			this.ClearGameSlotButton.TabIndex = 14;
			this.ClearGameSlotButton.Text = "Clear Slot";
			this.ClearGameSlotButton.UseVisualStyleBackColor = true;
			this.ClearGameSlotButton.Click += new System.EventHandler(this.ClearGameSlotButton_Click);
			// 
			// CopyToLibraryButton
			// 
			this.CopyToLibraryButton.Location = new System.Drawing.Point(272, 168);
			this.CopyToLibraryButton.Name = "CopyToLibraryButton";
			this.CopyToLibraryButton.Size = new System.Drawing.Size(107, 23);
			this.CopyToLibraryButton.TabIndex = 15;
			this.CopyToLibraryButton.Text = "Copy to Library ->";
			this.CopyToLibraryButton.UseVisualStyleBackColor = true;
			this.CopyToLibraryButton.Click += new System.EventHandler(this.CopyToLibraryButton_Click);
			// 
			// CopyToGameButton
			// 
			this.CopyToGameButton.Location = new System.Drawing.Point(272, 120);
			this.CopyToGameButton.Name = "CopyToGameButton";
			this.CopyToGameButton.Size = new System.Drawing.Size(107, 23);
			this.CopyToGameButton.TabIndex = 16;
			this.CopyToGameButton.Text = "<- Copy to Game";
			this.CopyToGameButton.UseVisualStyleBackColor = true;
			this.CopyToGameButton.Click += new System.EventHandler(this.CopyToGameButton_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 232);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(106, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Selected Preset Info:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(656, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(135, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Preset Name (Library-Only):";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(656, 24);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(360, 20);
			this.textBox3.TabIndex = 19;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(656, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(101, 13);
			this.label7.TabIndex = 20;
			this.label7.Text = "Time/Date Created:";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(656, 72);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(240, 20);
			this.dateTimePicker1.TabIndex = 21;
			// 
			// WriteGameFileButton
			// 
			this.WriteGameFileButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
			this.WriteGameFileButton.FlatAppearance.BorderSize = 2;
			this.WriteGameFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.WriteGameFileButton.Location = new System.Drawing.Point(280, 248);
			this.WriteGameFileButton.Name = "WriteGameFileButton";
			this.WriteGameFileButton.Size = new System.Drawing.Size(96, 96);
			this.WriteGameFileButton.TabIndex = 22;
			this.WriteGameFileButton.Text = "Write Game File";
			this.WriteGameFileButton.UseVisualStyleBackColor = true;
			this.WriteGameFileButton.Click += new System.EventHandler(this.WriteGameFileButton_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(656, 184);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(15, 14);
			this.checkBox1.TabIndex = 24;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(656, 216);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(15, 14);
			this.checkBox2.TabIndex = 25;
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(656, 248);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(15, 14);
			this.checkBox3.TabIndex = 26;
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(656, 280);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(15, 14);
			this.checkBox4.TabIndex = 27;
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Location = new System.Drawing.Point(656, 312);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(15, 14);
			this.checkBox5.TabIndex = 28;
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Location = new System.Drawing.Point(656, 344);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(15, 14);
			this.checkBox6.TabIndex = 29;
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox7
			// 
			this.checkBox7.AutoSize = true;
			this.checkBox7.Location = new System.Drawing.Point(656, 376);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(15, 14);
			this.checkBox7.TabIndex = 30;
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new System.Drawing.Point(656, 408);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(15, 14);
			this.checkBox8.TabIndex = 31;
			this.checkBox8.UseVisualStyleBackColor = true;
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(656, 120);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(360, 21);
			this.comboBox2.TabIndex = 32;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(656, 104);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(35, 13);
			this.label9.TabIndex = 33;
			this.label9.Text = "Zone:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(672, 184);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(17, 13);
			this.label10.TabIndex = 34;
			this.label10.Text = "A:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(672, 216);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(17, 13);
			this.label11.TabIndex = 35;
			this.label11.Text = "B:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(672, 248);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(17, 13);
			this.label12.TabIndex = 36;
			this.label12.Text = "C:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(672, 280);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(18, 13);
			this.label13.TabIndex = 37;
			this.label13.Text = "D:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(672, 312);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(16, 13);
			this.label14.TabIndex = 38;
			this.label14.Text = "1:";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(672, 344);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(16, 13);
			this.label15.TabIndex = 39;
			this.label15.Text = "2:";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(672, 376);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(16, 13);
			this.label16.TabIndex = 40;
			this.label16.Text = "3:";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(672, 408);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(16, 13);
			this.label17.TabIndex = 41;
			this.label17.Text = "4:";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(696, 184);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(100, 20);
			this.textBox4.TabIndex = 42;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(808, 184);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(100, 20);
			this.textBox5.TabIndex = 43;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(920, 184);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(100, 20);
			this.textBox6.TabIndex = 44;
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(696, 216);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(100, 20);
			this.textBox7.TabIndex = 45;
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(808, 216);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(100, 20);
			this.textBox8.TabIndex = 46;
			// 
			// textBox9
			// 
			this.textBox9.Location = new System.Drawing.Point(920, 216);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new System.Drawing.Size(100, 20);
			this.textBox9.TabIndex = 47;
			// 
			// textBox10
			// 
			this.textBox10.Location = new System.Drawing.Point(696, 248);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(100, 20);
			this.textBox10.TabIndex = 48;
			// 
			// textBox11
			// 
			this.textBox11.Location = new System.Drawing.Point(808, 248);
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new System.Drawing.Size(100, 20);
			this.textBox11.TabIndex = 49;
			// 
			// textBox12
			// 
			this.textBox12.Location = new System.Drawing.Point(920, 248);
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new System.Drawing.Size(100, 20);
			this.textBox12.TabIndex = 50;
			// 
			// textBox13
			// 
			this.textBox13.Location = new System.Drawing.Point(696, 280);
			this.textBox13.Name = "textBox13";
			this.textBox13.Size = new System.Drawing.Size(100, 20);
			this.textBox13.TabIndex = 51;
			// 
			// textBox14
			// 
			this.textBox14.Location = new System.Drawing.Point(808, 280);
			this.textBox14.Name = "textBox14";
			this.textBox14.Size = new System.Drawing.Size(100, 20);
			this.textBox14.TabIndex = 52;
			// 
			// textBox15
			// 
			this.textBox15.Location = new System.Drawing.Point(920, 280);
			this.textBox15.Name = "textBox15";
			this.textBox15.Size = new System.Drawing.Size(100, 20);
			this.textBox15.TabIndex = 53;
			// 
			// textBox16
			// 
			this.textBox16.Location = new System.Drawing.Point(696, 312);
			this.textBox16.Name = "textBox16";
			this.textBox16.Size = new System.Drawing.Size(100, 20);
			this.textBox16.TabIndex = 54;
			// 
			// textBox17
			// 
			this.textBox17.Location = new System.Drawing.Point(808, 312);
			this.textBox17.Name = "textBox17";
			this.textBox17.Size = new System.Drawing.Size(100, 20);
			this.textBox17.TabIndex = 55;
			// 
			// textBox18
			// 
			this.textBox18.Location = new System.Drawing.Point(920, 312);
			this.textBox18.Name = "textBox18";
			this.textBox18.Size = new System.Drawing.Size(100, 20);
			this.textBox18.TabIndex = 56;
			// 
			// textBox19
			// 
			this.textBox19.Location = new System.Drawing.Point(696, 344);
			this.textBox19.Name = "textBox19";
			this.textBox19.Size = new System.Drawing.Size(100, 20);
			this.textBox19.TabIndex = 57;
			// 
			// textBox20
			// 
			this.textBox20.Location = new System.Drawing.Point(808, 344);
			this.textBox20.Name = "textBox20";
			this.textBox20.Size = new System.Drawing.Size(100, 20);
			this.textBox20.TabIndex = 58;
			// 
			// textBox21
			// 
			this.textBox21.Location = new System.Drawing.Point(920, 344);
			this.textBox21.Name = "textBox21";
			this.textBox21.Size = new System.Drawing.Size(100, 20);
			this.textBox21.TabIndex = 59;
			// 
			// textBox22
			// 
			this.textBox22.Location = new System.Drawing.Point(696, 376);
			this.textBox22.Name = "textBox22";
			this.textBox22.Size = new System.Drawing.Size(100, 20);
			this.textBox22.TabIndex = 60;
			// 
			// textBox23
			// 
			this.textBox23.Location = new System.Drawing.Point(808, 376);
			this.textBox23.Name = "textBox23";
			this.textBox23.Size = new System.Drawing.Size(100, 20);
			this.textBox23.TabIndex = 61;
			// 
			// textBox24
			// 
			this.textBox24.Location = new System.Drawing.Point(920, 376);
			this.textBox24.Name = "textBox24";
			this.textBox24.Size = new System.Drawing.Size(100, 20);
			this.textBox24.TabIndex = 62;
			// 
			// textBox25
			// 
			this.textBox25.Location = new System.Drawing.Point(696, 408);
			this.textBox25.Name = "textBox25";
			this.textBox25.Size = new System.Drawing.Size(100, 20);
			this.textBox25.TabIndex = 63;
			// 
			// textBox26
			// 
			this.textBox26.Location = new System.Drawing.Point(808, 408);
			this.textBox26.Name = "textBox26";
			this.textBox26.Size = new System.Drawing.Size(100, 20);
			this.textBox26.TabIndex = 64;
			// 
			// textBox27
			// 
			this.textBox27.Location = new System.Drawing.Point(920, 408);
			this.textBox27.Name = "textBox27";
			this.textBox27.Size = new System.Drawing.Size(100, 20);
			this.textBox27.TabIndex = 65;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(728, 160);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(38, 13);
			this.label19.TabIndex = 67;
			this.label19.Text = "X Pos:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(840, 160);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(38, 13);
			this.label20.TabIndex = 68;
			this.label20.Text = "Y Pos:";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(952, 160);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(38, 13);
			this.label21.TabIndex = 69;
			this.label21.Text = "Z Pos:";
			// 
			// textBox28
			// 
			this.textBox28.Location = new System.Drawing.Point(904, 72);
			this.textBox28.Name = "textBox28";
			this.textBox28.Size = new System.Drawing.Size(112, 20);
			this.textBox28.TabIndex = 70;
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(568, 408);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(72, 23);
			this.button9.TabIndex = 71;
			this.button9.Text = "Update";
			this.button9.UseVisualStyleBackColor = true;
			// 
			// CharacterDataFolderDialog
			// 
			this.CharacterDataFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(280, 352);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 72);
			this.label8.TabIndex = 72;
			this.label8.Text = "IMPORTANT: The game\'s preset slots are not affected until this button is pressed." +
    "";
			// 
			// WaymarkLibrarianForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1034, 444);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.textBox28);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.textBox27);
			this.Controls.Add(this.textBox26);
			this.Controls.Add(this.textBox25);
			this.Controls.Add(this.textBox24);
			this.Controls.Add(this.textBox23);
			this.Controls.Add(this.textBox22);
			this.Controls.Add(this.textBox21);
			this.Controls.Add(this.textBox20);
			this.Controls.Add(this.textBox19);
			this.Controls.Add(this.textBox18);
			this.Controls.Add(this.textBox17);
			this.Controls.Add(this.textBox16);
			this.Controls.Add(this.textBox15);
			this.Controls.Add(this.textBox14);
			this.Controls.Add(this.textBox13);
			this.Controls.Add(this.textBox12);
			this.Controls.Add(this.textBox11);
			this.Controls.Add(this.textBox10);
			this.Controls.Add(this.textBox9);
			this.Controls.Add(this.textBox8);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.checkBox8);
			this.Controls.Add(this.checkBox7);
			this.Controls.Add(this.checkBox6);
			this.Controls.Add(this.checkBox5);
			this.Controls.Add(this.checkBox4);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.WriteGameFileButton);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.CopyToGameButton);
			this.Controls.Add(this.CopyToLibraryButton);
			this.Controls.Add(this.ClearGameSlotButton);
			this.Controls.Add(this.SetCharacterAliasButton);
			this.Controls.Add(this.SelectedPresetInfoBox);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.LibraryListBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.GamePresetListBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.CharacterDataFolderTextBox);
			this.Controls.Add(this.CharacterFolderBrowseButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CharacterListDropdown);
			this.Name = "WaymarkLibrarianForm";
			this.Text = "FFXIV Waymark Librarian";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaymarkLibrarianForm_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CharacterListDropdown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button CharacterFolderBrowseButton;
		private System.Windows.Forms.TextBox CharacterDataFolderTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox GamePresetListBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox LibraryListBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox SelectedPresetInfoBox;
		private System.Windows.Forms.Button SetCharacterAliasButton;
		private System.Windows.Forms.Button ClearGameSlotButton;
		private System.Windows.Forms.Button CopyToLibraryButton;
		private System.Windows.Forms.Button CopyToGameButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Button WriteGameFileButton;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.TextBox textBox13;
		private System.Windows.Forms.TextBox textBox14;
		private System.Windows.Forms.TextBox textBox15;
		private System.Windows.Forms.TextBox textBox16;
		private System.Windows.Forms.TextBox textBox17;
		private System.Windows.Forms.TextBox textBox18;
		private System.Windows.Forms.TextBox textBox19;
		private System.Windows.Forms.TextBox textBox20;
		private System.Windows.Forms.TextBox textBox21;
		private System.Windows.Forms.TextBox textBox22;
		private System.Windows.Forms.TextBox textBox23;
		private System.Windows.Forms.TextBox textBox24;
		private System.Windows.Forms.TextBox textBox25;
		private System.Windows.Forms.TextBox textBox26;
		private System.Windows.Forms.TextBox textBox27;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox textBox28;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.FolderBrowserDialog CharacterDataFolderDialog;
		private System.Windows.Forms.Label label8;
	}
}

