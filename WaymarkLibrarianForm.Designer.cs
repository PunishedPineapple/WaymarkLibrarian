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
			this.components = new System.ComponentModel.Container();
			this.CharacterListDropdown = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CharacterFolderBrowseButton = new System.Windows.Forms.Button();
			this.CharacterDataFolderTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.GamePresetListBox = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.LibraryListBox = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.LibraryPresetImportButton = new System.Windows.Forms.Button();
			this.LibraryPresetRemoveButton = new System.Windows.Forms.Button();
			this.SelectedPresetInfoBox = new System.Windows.Forms.TextBox();
			this.SetCharacterAliasButton = new System.Windows.Forms.Button();
			this.ClearGameSlotButton = new System.Windows.Forms.Button();
			this.CopyToLibraryButton = new System.Windows.Forms.Button();
			this.CopyToGameButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.PresetNameTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.PresetDatePicker = new System.Windows.Forms.DateTimePicker();
			this.WriteGameFileButton = new System.Windows.Forms.Button();
			this.WaymarkACheckbox = new System.Windows.Forms.CheckBox();
			this.WaymarkBCheckbox = new System.Windows.Forms.CheckBox();
			this.WaymarkCCheckbox = new System.Windows.Forms.CheckBox();
			this.WaymarkDCheckbox = new System.Windows.Forms.CheckBox();
			this.Waymark1Checkbox = new System.Windows.Forms.CheckBox();
			this.Waymark2Checkbox = new System.Windows.Forms.CheckBox();
			this.Waymark3Checkbox = new System.Windows.Forms.CheckBox();
			this.Waymark4Checkbox = new System.Windows.Forms.CheckBox();
			this.PresetZoneDropdown = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.WaymarkATextBox_X = new System.Windows.Forms.TextBox();
			this.WaymarkATextBox_Y = new System.Windows.Forms.TextBox();
			this.WaymarkATextBox_Z = new System.Windows.Forms.TextBox();
			this.WaymarkBTextBox_X = new System.Windows.Forms.TextBox();
			this.WaymarkBTextBox_Y = new System.Windows.Forms.TextBox();
			this.WaymarkBTextBox_Z = new System.Windows.Forms.TextBox();
			this.WaymarkCTextBox_X = new System.Windows.Forms.TextBox();
			this.WaymarkCTextBox_Y = new System.Windows.Forms.TextBox();
			this.WaymarkCTextBox_Z = new System.Windows.Forms.TextBox();
			this.WaymarkDTextBox_X = new System.Windows.Forms.TextBox();
			this.WaymarkDTextBox_Y = new System.Windows.Forms.TextBox();
			this.WaymarkDTextBox_Z = new System.Windows.Forms.TextBox();
			this.Waymark1TextBox_X = new System.Windows.Forms.TextBox();
			this.Waymark1TextBox_Y = new System.Windows.Forms.TextBox();
			this.Waymark1TextBox_Z = new System.Windows.Forms.TextBox();
			this.Waymark2TextBox_X = new System.Windows.Forms.TextBox();
			this.Waymark2TextBox_Y = new System.Windows.Forms.TextBox();
			this.Waymark2TextBox_Z = new System.Windows.Forms.TextBox();
			this.Waymark3TextBox_X = new System.Windows.Forms.TextBox();
			this.Waymark3TextBox_Y = new System.Windows.Forms.TextBox();
			this.Waymark3TextBox_Z = new System.Windows.Forms.TextBox();
			this.Waymark4TextBox_X = new System.Windows.Forms.TextBox();
			this.Waymark4TextBox_Y = new System.Windows.Forms.TextBox();
			this.Waymark4TextBox_Z = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.LibraryPresetUpdateButton = new System.Windows.Forms.Button();
			this.CharacterDataFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.label8 = new System.Windows.Forms.Label();
			this.PresetTimePicker = new System.Windows.Forms.DateTimePicker();
			this.LibraryPresetExportButton = new System.Windows.Forms.Button();
			this.PresetZoneTextBox = new System.Windows.Forms.TextBox();
			this.MainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.label18 = new System.Windows.Forms.Label();
			this.LibraryPresetNewButton = new System.Windows.Forms.Button();
			this.HelpLinkLabel = new System.Windows.Forms.LinkLabel();
			this.UpdateLinkLabel = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// CharacterListDropdown
			// 
			this.CharacterListDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CharacterListDropdown.FormattingEnabled = true;
			this.CharacterListDropdown.Location = new System.Drawing.Point(8, 72);
			this.CharacterListDropdown.Name = "CharacterListDropdown";
			this.CharacterListDropdown.Size = new System.Drawing.Size(256, 21);
			this.CharacterListDropdown.TabIndex = 2;
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
			this.CharacterFolderBrowseButton.TabIndex = 1;
			this.CharacterFolderBrowseButton.Text = "...";
			this.MainFormToolTip.SetToolTip(this.CharacterFolderBrowseButton, "Select the folder that contains the FFXIV character configuration data for all ch" +
        "aracters.");
			this.CharacterFolderBrowseButton.UseVisualStyleBackColor = true;
			this.CharacterFolderBrowseButton.Click += new System.EventHandler(this.CharacterFolderBrowseButton_Click);
			// 
			// CharacterDataFolderTextBox
			// 
			this.CharacterDataFolderTextBox.Location = new System.Drawing.Point(8, 24);
			this.CharacterDataFolderTextBox.Name = "CharacterDataFolderTextBox";
			this.CharacterDataFolderTextBox.ReadOnly = true;
			this.CharacterDataFolderTextBox.Size = new System.Drawing.Size(256, 20);
			this.CharacterDataFolderTextBox.TabIndex = 0;
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
			this.GamePresetListBox.TabIndex = 4;
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
			this.LibraryListBox.Size = new System.Drawing.Size(248, 407);
			this.LibraryListBox.TabIndex = 10;
			this.LibraryListBox.SelectedIndexChanged += new System.EventHandler(this.LibraryListBox_SelectedIndexChanged);
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
			// LibraryPresetImportButton
			// 
			this.LibraryPresetImportButton.Location = new System.Drawing.Point(456, 440);
			this.LibraryPresetImportButton.Name = "LibraryPresetImportButton";
			this.LibraryPresetImportButton.Size = new System.Drawing.Size(56, 24);
			this.LibraryPresetImportButton.TabIndex = 11;
			this.LibraryPresetImportButton.Text = "Import";
			this.MainFormToolTip.SetToolTip(this.LibraryPresetImportButton, "Import a shared preset, or create a new one from scratch.");
			this.LibraryPresetImportButton.UseVisualStyleBackColor = true;
			this.LibraryPresetImportButton.Click += new System.EventHandler(this.LibraryPresetImportButton_Click);
			// 
			// LibraryPresetRemoveButton
			// 
			this.LibraryPresetRemoveButton.Location = new System.Drawing.Point(584, 440);
			this.LibraryPresetRemoveButton.Name = "LibraryPresetRemoveButton";
			this.LibraryPresetRemoveButton.Size = new System.Drawing.Size(56, 24);
			this.LibraryPresetRemoveButton.TabIndex = 13;
			this.LibraryPresetRemoveButton.Text = "Remove";
			this.MainFormToolTip.SetToolTip(this.LibraryPresetRemoveButton, "Delete the selected preset from the library.");
			this.LibraryPresetRemoveButton.UseVisualStyleBackColor = true;
			this.LibraryPresetRemoveButton.Click += new System.EventHandler(this.LibraryPresetRemoveButton_Click);
			// 
			// SelectedPresetInfoBox
			// 
			this.SelectedPresetInfoBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SelectedPresetInfoBox.Location = new System.Drawing.Point(8, 248);
			this.SelectedPresetInfoBox.Multiline = true;
			this.SelectedPresetInfoBox.Name = "SelectedPresetInfoBox";
			this.SelectedPresetInfoBox.ReadOnly = true;
			this.SelectedPresetInfoBox.Size = new System.Drawing.Size(256, 176);
			this.SelectedPresetInfoBox.TabIndex = 8;
			// 
			// SetCharacterAliasButton
			// 
			this.SetCharacterAliasButton.Location = new System.Drawing.Point(272, 72);
			this.SetCharacterAliasButton.Name = "SetCharacterAliasButton";
			this.SetCharacterAliasButton.Size = new System.Drawing.Size(75, 23);
			this.SetCharacterAliasButton.TabIndex = 3;
			this.SetCharacterAliasButton.Text = "Set Alias";
			this.MainFormToolTip.SetToolTip(this.SetCharacterAliasButton, "Set a friendly name for the currently selected character.");
			this.SetCharacterAliasButton.UseVisualStyleBackColor = true;
			this.SetCharacterAliasButton.Click += new System.EventHandler(this.SetCharacterAliasButton_Click);
			// 
			// ClearGameSlotButton
			// 
			this.ClearGameSlotButton.Location = new System.Drawing.Point(8, 192);
			this.ClearGameSlotButton.Name = "ClearGameSlotButton";
			this.ClearGameSlotButton.Size = new System.Drawing.Size(72, 23);
			this.ClearGameSlotButton.TabIndex = 7;
			this.ClearGameSlotButton.Text = "Clear Slot";
			this.MainFormToolTip.SetToolTip(this.ClearGameSlotButton, "Remove the preset from the selected game slot.");
			this.ClearGameSlotButton.UseVisualStyleBackColor = true;
			this.ClearGameSlotButton.Click += new System.EventHandler(this.ClearGameSlotButton_Click);
			// 
			// CopyToLibraryButton
			// 
			this.CopyToLibraryButton.Location = new System.Drawing.Point(272, 168);
			this.CopyToLibraryButton.Name = "CopyToLibraryButton";
			this.CopyToLibraryButton.Size = new System.Drawing.Size(107, 23);
			this.CopyToLibraryButton.TabIndex = 6;
			this.CopyToLibraryButton.Text = "Copy to Library ->";
			this.MainFormToolTip.SetToolTip(this.CopyToLibraryButton, "Copy the selected game slot into a new preset in the library.");
			this.CopyToLibraryButton.UseVisualStyleBackColor = true;
			this.CopyToLibraryButton.Click += new System.EventHandler(this.CopyToLibraryButton_Click);
			// 
			// CopyToGameButton
			// 
			this.CopyToGameButton.Location = new System.Drawing.Point(272, 120);
			this.CopyToGameButton.Name = "CopyToGameButton";
			this.CopyToGameButton.Size = new System.Drawing.Size(107, 23);
			this.CopyToGameButton.TabIndex = 5;
			this.CopyToGameButton.Text = "<- Copy to Game";
			this.MainFormToolTip.SetToolTip(this.CopyToGameButton, "Copy the selected preset from the libray to the selected game slot.");
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
			// PresetNameTextBox
			// 
			this.PresetNameTextBox.Location = new System.Drawing.Point(656, 24);
			this.PresetNameTextBox.Name = "PresetNameTextBox";
			this.PresetNameTextBox.Size = new System.Drawing.Size(360, 20);
			this.PresetNameTextBox.TabIndex = 13;
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
			// PresetDatePicker
			// 
			this.PresetDatePicker.CustomFormat = "";
			this.PresetDatePicker.Location = new System.Drawing.Point(656, 72);
			this.PresetDatePicker.Name = "PresetDatePicker";
			this.PresetDatePicker.Size = new System.Drawing.Size(256, 20);
			this.PresetDatePicker.TabIndex = 14;
			// 
			// WriteGameFileButton
			// 
			this.WriteGameFileButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
			this.WriteGameFileButton.FlatAppearance.BorderSize = 2;
			this.WriteGameFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.WriteGameFileButton.Location = new System.Drawing.Point(280, 248);
			this.WriteGameFileButton.Name = "WriteGameFileButton";
			this.WriteGameFileButton.Size = new System.Drawing.Size(96, 96);
			this.WriteGameFileButton.TabIndex = 9;
			this.WriteGameFileButton.Text = "Write Game File";
			this.MainFormToolTip.SetToolTip(this.WriteGameFileButton, "Write the current presets to the configuration for the selected character.");
			this.WriteGameFileButton.UseVisualStyleBackColor = true;
			this.WriteGameFileButton.Click += new System.EventHandler(this.WriteGameFileButton_Click);
			// 
			// WaymarkACheckbox
			// 
			this.WaymarkACheckbox.AutoSize = true;
			this.WaymarkACheckbox.Location = new System.Drawing.Point(656, 184);
			this.WaymarkACheckbox.Name = "WaymarkACheckbox";
			this.WaymarkACheckbox.Size = new System.Drawing.Size(15, 14);
			this.WaymarkACheckbox.TabIndex = 17;
			this.WaymarkACheckbox.UseVisualStyleBackColor = true;
			// 
			// WaymarkBCheckbox
			// 
			this.WaymarkBCheckbox.AutoSize = true;
			this.WaymarkBCheckbox.Location = new System.Drawing.Point(656, 216);
			this.WaymarkBCheckbox.Name = "WaymarkBCheckbox";
			this.WaymarkBCheckbox.Size = new System.Drawing.Size(15, 14);
			this.WaymarkBCheckbox.TabIndex = 21;
			this.WaymarkBCheckbox.UseVisualStyleBackColor = true;
			// 
			// WaymarkCCheckbox
			// 
			this.WaymarkCCheckbox.AutoSize = true;
			this.WaymarkCCheckbox.Location = new System.Drawing.Point(656, 248);
			this.WaymarkCCheckbox.Name = "WaymarkCCheckbox";
			this.WaymarkCCheckbox.Size = new System.Drawing.Size(15, 14);
			this.WaymarkCCheckbox.TabIndex = 25;
			this.WaymarkCCheckbox.UseVisualStyleBackColor = true;
			// 
			// WaymarkDCheckbox
			// 
			this.WaymarkDCheckbox.AutoSize = true;
			this.WaymarkDCheckbox.Location = new System.Drawing.Point(656, 280);
			this.WaymarkDCheckbox.Name = "WaymarkDCheckbox";
			this.WaymarkDCheckbox.Size = new System.Drawing.Size(15, 14);
			this.WaymarkDCheckbox.TabIndex = 29;
			this.WaymarkDCheckbox.UseVisualStyleBackColor = true;
			// 
			// Waymark1Checkbox
			// 
			this.Waymark1Checkbox.AutoSize = true;
			this.Waymark1Checkbox.Location = new System.Drawing.Point(656, 312);
			this.Waymark1Checkbox.Name = "Waymark1Checkbox";
			this.Waymark1Checkbox.Size = new System.Drawing.Size(15, 14);
			this.Waymark1Checkbox.TabIndex = 33;
			this.Waymark1Checkbox.UseVisualStyleBackColor = true;
			// 
			// Waymark2Checkbox
			// 
			this.Waymark2Checkbox.AutoSize = true;
			this.Waymark2Checkbox.Location = new System.Drawing.Point(656, 344);
			this.Waymark2Checkbox.Name = "Waymark2Checkbox";
			this.Waymark2Checkbox.Size = new System.Drawing.Size(15, 14);
			this.Waymark2Checkbox.TabIndex = 37;
			this.Waymark2Checkbox.UseVisualStyleBackColor = true;
			// 
			// Waymark3Checkbox
			// 
			this.Waymark3Checkbox.AutoSize = true;
			this.Waymark3Checkbox.Location = new System.Drawing.Point(656, 376);
			this.Waymark3Checkbox.Name = "Waymark3Checkbox";
			this.Waymark3Checkbox.Size = new System.Drawing.Size(15, 14);
			this.Waymark3Checkbox.TabIndex = 41;
			this.Waymark3Checkbox.UseVisualStyleBackColor = true;
			// 
			// Waymark4Checkbox
			// 
			this.Waymark4Checkbox.AutoSize = true;
			this.Waymark4Checkbox.Location = new System.Drawing.Point(656, 408);
			this.Waymark4Checkbox.Name = "Waymark4Checkbox";
			this.Waymark4Checkbox.Size = new System.Drawing.Size(15, 14);
			this.Waymark4Checkbox.TabIndex = 45;
			this.Waymark4Checkbox.UseVisualStyleBackColor = true;
			// 
			// PresetZoneDropdown
			// 
			this.PresetZoneDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PresetZoneDropdown.FormattingEnabled = true;
			this.PresetZoneDropdown.Location = new System.Drawing.Point(656, 120);
			this.PresetZoneDropdown.Name = "PresetZoneDropdown";
			this.PresetZoneDropdown.Size = new System.Drawing.Size(256, 21);
			this.PresetZoneDropdown.TabIndex = 16;
			this.PresetZoneDropdown.SelectedIndexChanged += new System.EventHandler(this.PresetZoneDropdown_SelectedIndexChanged);
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
			// WaymarkATextBox_X
			// 
			this.WaymarkATextBox_X.Location = new System.Drawing.Point(696, 184);
			this.WaymarkATextBox_X.Name = "WaymarkATextBox_X";
			this.WaymarkATextBox_X.Size = new System.Drawing.Size(100, 20);
			this.WaymarkATextBox_X.TabIndex = 18;
			// 
			// WaymarkATextBox_Y
			// 
			this.WaymarkATextBox_Y.Location = new System.Drawing.Point(808, 184);
			this.WaymarkATextBox_Y.Name = "WaymarkATextBox_Y";
			this.WaymarkATextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.WaymarkATextBox_Y.TabIndex = 19;
			// 
			// WaymarkATextBox_Z
			// 
			this.WaymarkATextBox_Z.Location = new System.Drawing.Point(920, 184);
			this.WaymarkATextBox_Z.Name = "WaymarkATextBox_Z";
			this.WaymarkATextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.WaymarkATextBox_Z.TabIndex = 20;
			// 
			// WaymarkBTextBox_X
			// 
			this.WaymarkBTextBox_X.Location = new System.Drawing.Point(696, 216);
			this.WaymarkBTextBox_X.Name = "WaymarkBTextBox_X";
			this.WaymarkBTextBox_X.Size = new System.Drawing.Size(100, 20);
			this.WaymarkBTextBox_X.TabIndex = 22;
			// 
			// WaymarkBTextBox_Y
			// 
			this.WaymarkBTextBox_Y.Location = new System.Drawing.Point(808, 216);
			this.WaymarkBTextBox_Y.Name = "WaymarkBTextBox_Y";
			this.WaymarkBTextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.WaymarkBTextBox_Y.TabIndex = 23;
			// 
			// WaymarkBTextBox_Z
			// 
			this.WaymarkBTextBox_Z.Location = new System.Drawing.Point(920, 216);
			this.WaymarkBTextBox_Z.Name = "WaymarkBTextBox_Z";
			this.WaymarkBTextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.WaymarkBTextBox_Z.TabIndex = 24;
			// 
			// WaymarkCTextBox_X
			// 
			this.WaymarkCTextBox_X.Location = new System.Drawing.Point(696, 248);
			this.WaymarkCTextBox_X.Name = "WaymarkCTextBox_X";
			this.WaymarkCTextBox_X.Size = new System.Drawing.Size(100, 20);
			this.WaymarkCTextBox_X.TabIndex = 26;
			// 
			// WaymarkCTextBox_Y
			// 
			this.WaymarkCTextBox_Y.Location = new System.Drawing.Point(808, 248);
			this.WaymarkCTextBox_Y.Name = "WaymarkCTextBox_Y";
			this.WaymarkCTextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.WaymarkCTextBox_Y.TabIndex = 27;
			// 
			// WaymarkCTextBox_Z
			// 
			this.WaymarkCTextBox_Z.Location = new System.Drawing.Point(920, 248);
			this.WaymarkCTextBox_Z.Name = "WaymarkCTextBox_Z";
			this.WaymarkCTextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.WaymarkCTextBox_Z.TabIndex = 28;
			// 
			// WaymarkDTextBox_X
			// 
			this.WaymarkDTextBox_X.Location = new System.Drawing.Point(696, 280);
			this.WaymarkDTextBox_X.Name = "WaymarkDTextBox_X";
			this.WaymarkDTextBox_X.Size = new System.Drawing.Size(100, 20);
			this.WaymarkDTextBox_X.TabIndex = 30;
			// 
			// WaymarkDTextBox_Y
			// 
			this.WaymarkDTextBox_Y.Location = new System.Drawing.Point(808, 280);
			this.WaymarkDTextBox_Y.Name = "WaymarkDTextBox_Y";
			this.WaymarkDTextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.WaymarkDTextBox_Y.TabIndex = 31;
			// 
			// WaymarkDTextBox_Z
			// 
			this.WaymarkDTextBox_Z.Location = new System.Drawing.Point(920, 280);
			this.WaymarkDTextBox_Z.Name = "WaymarkDTextBox_Z";
			this.WaymarkDTextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.WaymarkDTextBox_Z.TabIndex = 32;
			// 
			// Waymark1TextBox_X
			// 
			this.Waymark1TextBox_X.Location = new System.Drawing.Point(696, 312);
			this.Waymark1TextBox_X.Name = "Waymark1TextBox_X";
			this.Waymark1TextBox_X.Size = new System.Drawing.Size(100, 20);
			this.Waymark1TextBox_X.TabIndex = 34;
			// 
			// Waymark1TextBox_Y
			// 
			this.Waymark1TextBox_Y.Location = new System.Drawing.Point(808, 312);
			this.Waymark1TextBox_Y.Name = "Waymark1TextBox_Y";
			this.Waymark1TextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.Waymark1TextBox_Y.TabIndex = 35;
			// 
			// Waymark1TextBox_Z
			// 
			this.Waymark1TextBox_Z.Location = new System.Drawing.Point(920, 312);
			this.Waymark1TextBox_Z.Name = "Waymark1TextBox_Z";
			this.Waymark1TextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.Waymark1TextBox_Z.TabIndex = 36;
			// 
			// Waymark2TextBox_X
			// 
			this.Waymark2TextBox_X.Location = new System.Drawing.Point(696, 344);
			this.Waymark2TextBox_X.Name = "Waymark2TextBox_X";
			this.Waymark2TextBox_X.Size = new System.Drawing.Size(100, 20);
			this.Waymark2TextBox_X.TabIndex = 38;
			// 
			// Waymark2TextBox_Y
			// 
			this.Waymark2TextBox_Y.Location = new System.Drawing.Point(808, 344);
			this.Waymark2TextBox_Y.Name = "Waymark2TextBox_Y";
			this.Waymark2TextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.Waymark2TextBox_Y.TabIndex = 39;
			// 
			// Waymark2TextBox_Z
			// 
			this.Waymark2TextBox_Z.Location = new System.Drawing.Point(920, 344);
			this.Waymark2TextBox_Z.Name = "Waymark2TextBox_Z";
			this.Waymark2TextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.Waymark2TextBox_Z.TabIndex = 40;
			// 
			// Waymark3TextBox_X
			// 
			this.Waymark3TextBox_X.Location = new System.Drawing.Point(696, 376);
			this.Waymark3TextBox_X.Name = "Waymark3TextBox_X";
			this.Waymark3TextBox_X.Size = new System.Drawing.Size(100, 20);
			this.Waymark3TextBox_X.TabIndex = 42;
			// 
			// Waymark3TextBox_Y
			// 
			this.Waymark3TextBox_Y.Location = new System.Drawing.Point(808, 376);
			this.Waymark3TextBox_Y.Name = "Waymark3TextBox_Y";
			this.Waymark3TextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.Waymark3TextBox_Y.TabIndex = 43;
			// 
			// Waymark3TextBox_Z
			// 
			this.Waymark3TextBox_Z.Location = new System.Drawing.Point(920, 376);
			this.Waymark3TextBox_Z.Name = "Waymark3TextBox_Z";
			this.Waymark3TextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.Waymark3TextBox_Z.TabIndex = 44;
			// 
			// Waymark4TextBox_X
			// 
			this.Waymark4TextBox_X.Location = new System.Drawing.Point(696, 408);
			this.Waymark4TextBox_X.Name = "Waymark4TextBox_X";
			this.Waymark4TextBox_X.Size = new System.Drawing.Size(100, 20);
			this.Waymark4TextBox_X.TabIndex = 46;
			// 
			// Waymark4TextBox_Y
			// 
			this.Waymark4TextBox_Y.Location = new System.Drawing.Point(808, 408);
			this.Waymark4TextBox_Y.Name = "Waymark4TextBox_Y";
			this.Waymark4TextBox_Y.Size = new System.Drawing.Size(100, 20);
			this.Waymark4TextBox_Y.TabIndex = 47;
			// 
			// Waymark4TextBox_Z
			// 
			this.Waymark4TextBox_Z.Location = new System.Drawing.Point(920, 408);
			this.Waymark4TextBox_Z.Name = "Waymark4TextBox_Z";
			this.Waymark4TextBox_Z.Size = new System.Drawing.Size(96, 20);
			this.Waymark4TextBox_Z.TabIndex = 48;
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
			// LibraryPresetUpdateButton
			// 
			this.LibraryPresetUpdateButton.AccessibleDescription = "";
			this.LibraryPresetUpdateButton.AccessibleName = "";
			this.LibraryPresetUpdateButton.Location = new System.Drawing.Point(696, 440);
			this.LibraryPresetUpdateButton.Name = "LibraryPresetUpdateButton";
			this.LibraryPresetUpdateButton.Size = new System.Drawing.Size(320, 24);
			this.LibraryPresetUpdateButton.TabIndex = 49;
			this.LibraryPresetUpdateButton.Text = "Update";
			this.MainFormToolTip.SetToolTip(this.LibraryPresetUpdateButton, "Update the selected preset with the values above.");
			this.LibraryPresetUpdateButton.UseVisualStyleBackColor = true;
			this.LibraryPresetUpdateButton.Click += new System.EventHandler(this.LibraryPresetUpdateButton_Click);
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
			// PresetTimePicker
			// 
			this.PresetTimePicker.CustomFormat = "";
			this.PresetTimePicker.Location = new System.Drawing.Point(920, 72);
			this.PresetTimePicker.Name = "PresetTimePicker";
			this.PresetTimePicker.Size = new System.Drawing.Size(96, 20);
			this.PresetTimePicker.TabIndex = 15;
			// 
			// LibraryPresetExportButton
			// 
			this.LibraryPresetExportButton.Location = new System.Drawing.Point(520, 440);
			this.LibraryPresetExportButton.Name = "LibraryPresetExportButton";
			this.LibraryPresetExportButton.Size = new System.Drawing.Size(56, 24);
			this.LibraryPresetExportButton.TabIndex = 12;
			this.LibraryPresetExportButton.Text = "Export";
			this.MainFormToolTip.SetToolTip(this.LibraryPresetExportButton, "Generate text to share the selected preset.");
			this.LibraryPresetExportButton.UseVisualStyleBackColor = true;
			this.LibraryPresetExportButton.Click += new System.EventHandler(this.LibraryPresetExportButton_Click);
			// 
			// PresetZoneTextBox
			// 
			this.PresetZoneTextBox.Location = new System.Drawing.Point(920, 120);
			this.PresetZoneTextBox.Name = "PresetZoneTextBox";
			this.PresetZoneTextBox.Size = new System.Drawing.Size(96, 20);
			this.PresetZoneTextBox.TabIndex = 73;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(920, 104);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(49, 13);
			this.label18.TabIndex = 74;
			this.label18.Text = "Zone ID:";
			// 
			// LibraryPresetNewButton
			// 
			this.LibraryPresetNewButton.Location = new System.Drawing.Point(392, 440);
			this.LibraryPresetNewButton.Name = "LibraryPresetNewButton";
			this.LibraryPresetNewButton.Size = new System.Drawing.Size(56, 23);
			this.LibraryPresetNewButton.TabIndex = 75;
			this.LibraryPresetNewButton.Text = "New";
			this.LibraryPresetNewButton.UseVisualStyleBackColor = true;
			this.LibraryPresetNewButton.Click += new System.EventHandler(this.LibraryPresetNewButton_Click);
			// 
			// HelpLinkLabel
			// 
			this.HelpLinkLabel.AutoSize = true;
			this.HelpLinkLabel.Location = new System.Drawing.Point(8, 456);
			this.HelpLinkLabel.Name = "HelpLinkLabel";
			this.HelpLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.HelpLinkLabel.TabIndex = 76;
			this.HelpLinkLabel.TabStop = true;
			this.HelpLinkLabel.Text = "Info/Help";
			this.HelpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLinkLabel_LinkClicked);
			// 
			// UpdateLinkLabel
			// 
			this.UpdateLinkLabel.AutoSize = true;
			this.UpdateLinkLabel.Enabled = false;
			this.UpdateLinkLabel.LinkColor = System.Drawing.Color.Red;
			this.UpdateLinkLabel.Location = new System.Drawing.Point(153, 445);
			this.UpdateLinkLabel.Name = "UpdateLinkLabel";
			this.UpdateLinkLabel.Size = new System.Drawing.Size(130, 13);
			this.UpdateLinkLabel.TabIndex = 77;
			this.UpdateLinkLabel.TabStop = true;
			this.UpdateLinkLabel.Text = "Program Update Available";
			this.UpdateLinkLabel.VisitedLinkColor = System.Drawing.Color.Red;
			this.UpdateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateLinkLabel_LinkClicked);
			// 
			// WaymarkLibrarianForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1034, 476);
			this.Controls.Add(this.UpdateLinkLabel);
			this.Controls.Add(this.HelpLinkLabel);
			this.Controls.Add(this.LibraryPresetNewButton);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.PresetZoneTextBox);
			this.Controls.Add(this.LibraryPresetExportButton);
			this.Controls.Add(this.PresetTimePicker);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.LibraryPresetUpdateButton);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.Waymark4TextBox_Z);
			this.Controls.Add(this.Waymark4TextBox_Y);
			this.Controls.Add(this.Waymark4TextBox_X);
			this.Controls.Add(this.Waymark3TextBox_Z);
			this.Controls.Add(this.Waymark3TextBox_Y);
			this.Controls.Add(this.Waymark3TextBox_X);
			this.Controls.Add(this.Waymark2TextBox_Z);
			this.Controls.Add(this.Waymark2TextBox_Y);
			this.Controls.Add(this.Waymark2TextBox_X);
			this.Controls.Add(this.Waymark1TextBox_Z);
			this.Controls.Add(this.Waymark1TextBox_Y);
			this.Controls.Add(this.Waymark1TextBox_X);
			this.Controls.Add(this.WaymarkDTextBox_Z);
			this.Controls.Add(this.WaymarkDTextBox_Y);
			this.Controls.Add(this.WaymarkDTextBox_X);
			this.Controls.Add(this.WaymarkCTextBox_Z);
			this.Controls.Add(this.WaymarkCTextBox_Y);
			this.Controls.Add(this.WaymarkCTextBox_X);
			this.Controls.Add(this.WaymarkBTextBox_Z);
			this.Controls.Add(this.WaymarkBTextBox_Y);
			this.Controls.Add(this.WaymarkBTextBox_X);
			this.Controls.Add(this.WaymarkATextBox_Z);
			this.Controls.Add(this.WaymarkATextBox_Y);
			this.Controls.Add(this.WaymarkATextBox_X);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.PresetZoneDropdown);
			this.Controls.Add(this.Waymark4Checkbox);
			this.Controls.Add(this.Waymark3Checkbox);
			this.Controls.Add(this.Waymark2Checkbox);
			this.Controls.Add(this.Waymark1Checkbox);
			this.Controls.Add(this.WaymarkDCheckbox);
			this.Controls.Add(this.WaymarkCCheckbox);
			this.Controls.Add(this.WaymarkBCheckbox);
			this.Controls.Add(this.WaymarkACheckbox);
			this.Controls.Add(this.WriteGameFileButton);
			this.Controls.Add(this.PresetDatePicker);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.PresetNameTextBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.CopyToGameButton);
			this.Controls.Add(this.CopyToLibraryButton);
			this.Controls.Add(this.ClearGameSlotButton);
			this.Controls.Add(this.SetCharacterAliasButton);
			this.Controls.Add(this.SelectedPresetInfoBox);
			this.Controls.Add(this.LibraryPresetRemoveButton);
			this.Controls.Add(this.LibraryPresetImportButton);
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
		private System.Windows.Forms.Button LibraryPresetImportButton;
		private System.Windows.Forms.Button LibraryPresetRemoveButton;
		private System.Windows.Forms.TextBox SelectedPresetInfoBox;
		private System.Windows.Forms.Button SetCharacterAliasButton;
		private System.Windows.Forms.Button ClearGameSlotButton;
		private System.Windows.Forms.Button CopyToLibraryButton;
		private System.Windows.Forms.Button CopyToGameButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox PresetNameTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker PresetDatePicker;
		private System.Windows.Forms.Button WriteGameFileButton;
		private System.Windows.Forms.CheckBox WaymarkACheckbox;
		private System.Windows.Forms.CheckBox WaymarkBCheckbox;
		private System.Windows.Forms.CheckBox WaymarkCCheckbox;
		private System.Windows.Forms.CheckBox WaymarkDCheckbox;
		private System.Windows.Forms.CheckBox Waymark1Checkbox;
		private System.Windows.Forms.CheckBox Waymark2Checkbox;
		private System.Windows.Forms.CheckBox Waymark3Checkbox;
		private System.Windows.Forms.CheckBox Waymark4Checkbox;
		private System.Windows.Forms.ComboBox PresetZoneDropdown;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox WaymarkATextBox_X;
		private System.Windows.Forms.TextBox WaymarkATextBox_Y;
		private System.Windows.Forms.TextBox WaymarkATextBox_Z;
		private System.Windows.Forms.TextBox WaymarkBTextBox_X;
		private System.Windows.Forms.TextBox WaymarkBTextBox_Y;
		private System.Windows.Forms.TextBox WaymarkBTextBox_Z;
		private System.Windows.Forms.TextBox WaymarkCTextBox_X;
		private System.Windows.Forms.TextBox WaymarkCTextBox_Y;
		private System.Windows.Forms.TextBox WaymarkCTextBox_Z;
		private System.Windows.Forms.TextBox WaymarkDTextBox_X;
		private System.Windows.Forms.TextBox WaymarkDTextBox_Y;
		private System.Windows.Forms.TextBox WaymarkDTextBox_Z;
		private System.Windows.Forms.TextBox Waymark1TextBox_X;
		private System.Windows.Forms.TextBox Waymark1TextBox_Y;
		private System.Windows.Forms.TextBox Waymark1TextBox_Z;
		private System.Windows.Forms.TextBox Waymark2TextBox_X;
		private System.Windows.Forms.TextBox Waymark2TextBox_Y;
		private System.Windows.Forms.TextBox Waymark2TextBox_Z;
		private System.Windows.Forms.TextBox Waymark3TextBox_X;
		private System.Windows.Forms.TextBox Waymark3TextBox_Y;
		private System.Windows.Forms.TextBox Waymark3TextBox_Z;
		private System.Windows.Forms.TextBox Waymark4TextBox_X;
		private System.Windows.Forms.TextBox Waymark4TextBox_Y;
		private System.Windows.Forms.TextBox Waymark4TextBox_Z;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Button LibraryPresetUpdateButton;
		private System.Windows.Forms.FolderBrowserDialog CharacterDataFolderDialog;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.DateTimePicker PresetTimePicker;
		private System.Windows.Forms.Button LibraryPresetExportButton;
		private System.Windows.Forms.TextBox PresetZoneTextBox;
		private System.Windows.Forms.ToolTip MainFormToolTip;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button LibraryPresetNewButton;
		private System.Windows.Forms.LinkLabel HelpLinkLabel;
        private System.Windows.Forms.LinkLabel UpdateLinkLabel;
    }
}

