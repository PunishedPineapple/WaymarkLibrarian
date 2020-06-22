using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;

//	Use for InputBox rather than rolling our own trivial input dialog.
using Microsoft.VisualBasic;

namespace WaymarkLibrarian
{
	public partial class WaymarkLibrarianForm : Form
	{
		//	Construction
		public WaymarkLibrarianForm()
		{
			//	WinForms Stuff
			InitializeComponent();

			//	Set up controls.
			PresetTimePicker.Format = DateTimePickerFormat.Time;
			PresetTimePicker.ShowUpDown = true;

			//	Get config settings.
			mSettings = new Config();

			//	Set up the object that handles interfacing with the game save files.
			mGameDataHandler = new GameDataHandler( mSettings.GameDataSettings );

			//	Set up the zone data controls.
			PopulatePresetZoneDropdown();

			//	Load waymark library.
			mPresetLibrary = new PresetLibrary( mSettings.ConfigFolderPath + "\\WaymarkLibrary.xml" );
			PopulateLibraryListBox();

			//	Populate the controls for the character config folder, character list, and game preset list based on the saved settings.
			if( Directory.Exists( mSettings.ProgramSettings.CharacterDataFolderPath ) )
			{
				CharacterDataFolderTextBox.Text = mSettings.ProgramSettings.CharacterDataFolderPath;
				PopulateCharacterListDropdown();
				if( mSettings.ProgramSettings.DefaultCharacterID.Length > 0 && File.Exists( mSettings.ProgramSettings.CharacterDataFolderPath + '\\' + mSettings.ProgramSettings.DefaultCharacterID + '\\' + mSettings.GameDataSettings.WaymarkDataFileName ) )
				{
					for( int i = 0; i < CharacterListDropdown.Items.Count; ++i )
					{
						if( mCharacterFolderList[i].Split( '\\' ).Last() == mSettings.ProgramSettings.DefaultCharacterID )
						{
							CharacterListDropdown.SelectedIndex = i;
							break;
						}
					}
				}
			}

			//	Show a warning about backing up character data if this is the first time that the program has been used.
			if( mSettings.ProgramSettings.ShowInitialWarning )
			{
				MessageBox.Show( "Please back up your character configuration data before using this program.  This can be done either through the configuration options in the FFXIV launcher, or on the character selection screen in-game.\r\n\r\nIf you would like a detailed explanation of how to use this program, click on the\"Info/Help\" link in the bottom left corner of the main program window.", "Warning" );
				mSettings.ProgramSettings.ShowInitialWarning = false;
			}

			//	Show/Enable the update link as appropriate.
			if( mSettings.ProgramSettings.LastProgramUpdateVersionSeen > VersionInfoHelper.Parse( FileVersionInfo.GetVersionInfo( System.Reflection.Assembly.GetExecutingAssembly().Location ) ) )
            {
				UpdateLinkLabel.Enabled = true;
				UpdateLinkLabel.Show();
            }
            else
            {
				UpdateLinkLabel.Enabled = false;
				UpdateLinkLabel.Hide();
			}
		}

		//	Data Members
		private GamePresetContainer mGamePresetContainer;
		private GameDataHandler mGameDataHandler;
		private PresetLibrary mPresetLibrary;
		private string[] mCharacterFolderList;
		private Config mSettings;

		//	Types
		#region Types
		//	Trivial class to aid with JSON serialiazing.  I *hate* serializing objects instead of using an actual config format, but this is probably the best thing to have presets be transferrable between this program and Paisley Park.  At least having a buffer object lets us keep the JSON stuff isolated, even if it results in more work for us.
		//	Some of these fields may be meaningless for this program in order to (maybe) allow Paisley import of our exports.
		class PresetExportObject
		{
			public class WaymarkExportObject
			{
				public double X { get; set; } = 0.0;
				public double Y { get; set; } = 0.0;
				public double Z { get; set; } = 0.0;
				public int ID { get; set; } = 0;
				public bool Active { get; set; } = false;
			}
			public string Name { get; set; } = "Unknown Name";
			public UInt16 ZoneID { get; set; } = (UInt16)0u;
			public DateTimeOffset Time { get; set; } = new DateTimeOffset( DateTimeOffset.Now.UtcDateTime );
			public WaymarkExportObject A { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject B { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject C { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject D { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject One { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject Two { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject Three { get; set; } = new WaymarkExportObject();
			public WaymarkExportObject Four { get; set; } = new WaymarkExportObject();

		}
		#endregion

		//	Class Functions
		#region General Helper Functions
		public static bool DynamicObjectPropertyExists( dynamic obj, string name )
		{
			//	Didn't write; came from https://stackoverflow.com/a/48752086;
			if( obj == null ) return false;
			if( obj is IDictionary<string, object> dict ) return dict.ContainsKey( name );
			if( obj is Newtonsoft.Json.Linq.JObject ) return ( (Newtonsoft.Json.Linq.JObject)obj ).ContainsKey( name );
			return obj.GetType().GetProperty( name ) != null;
		}
		#endregion

		//	Member Functions
		#region Event Helper Functions
		private void PopulateCharacterListDropdown()
		{
			CharacterListDropdown.Items.Clear();
			PopulateGamePresetListBox( true );
			mCharacterFolderList = Directory.GetDirectories( mSettings.ProgramSettings.CharacterDataFolderPath, "FFXIV_CHR*", SearchOption.TopDirectoryOnly );
			foreach( string dir in mCharacterFolderList )
			{
				CharacterListDropdown.Items.Add( mSettings.CharacterAliasSettings.GetAlias( dir.Split('\\').Last() ) );
			}
		}
		private void PopulateGamePresetListBox( bool clear = false )
		{
			int previousSelectedIndex = GamePresetListBox.SelectedIndex;
			GamePresetListBox.SelectedIndex = -1;
			GamePresetListBox.Items.Clear();

			if( !clear && mGamePresetContainer != null )
			{
				for( uint i = 0u; i < mGamePresetContainer.Presets.Length; ++i )
				{
					GamePresetListBox.Items.Add( "Slot " + ( i + 1 ).ToString() + ": " + ( mGamePresetContainer[i].ZoneID == 0u ? "Empty" : mSettings.ZoneInfoSettings.GetZoneName( mGamePresetContainer[i].ZoneID ) ) );
				}

				if( previousSelectedIndex < GamePresetListBox.Items.Count )
				{
					GamePresetListBox.SelectedIndex = previousSelectedIndex;
				}
			}
		}

		private void PopulateGamePresetInfoBox()
		{
			if( mGamePresetContainer != null && GamePresetListBox.SelectedIndex >= 0 && GamePresetListBox.SelectedIndex < mSettings.GameDataSettings.NumberOfPresets )
			{
				SelectedPresetInfoBox.Text = mGamePresetContainer[(uint)GamePresetListBox.SelectedIndex].GetPresetDataString( mSettings.ZoneInfoSettings );
			}
			else
			{
				SelectedPresetInfoBox.Text = "";
			}
		}

		private void PopulateLibraryListBox( bool clear = false )
		{
			int previousSelectedIndex = LibraryListBox.SelectedIndex;
			LibraryListBox.SelectedIndex = -1;
			LibraryListBox.Items.Clear();
			PopulatePresetEditor( true );


			if( !clear )
			{
				mPresetLibrary.SortPresets();
				foreach( WaymarkPreset preset in mPresetLibrary.Presets )
				{
					LibraryListBox.Items.Add( preset.Name /*+ " (" + preset.ZoneID.ToString() + ") (" + preset.LastModified.ToLocalTime().ToString( "g" ) + ")"*/ );
				}

				//	Won't work well if we're sorting.
				/*if( previousSelectedIndex < LibraryListBox.Items.Count )
				{
					LibraryListBox.SelectedIndex = previousSelectedIndex;
				}*/
			}
		}

		private void PopulatePresetZoneDropdown()
		{
			//	Remember that the indices will be off by one since we have custom as a zone ID option at the top of the list.
			PresetZoneDropdown.Items.Clear();
			PresetZoneDropdown.Items.Add( "Custom" );
			for( int i = 0; mSettings.ZoneInfoSettings.ZoneDataIndexExists( i ); ++i )
			{
				PresetZoneDropdown.Items.Add( mSettings.ZoneInfoSettings.GetValueFromIndex( i ).ToString() + " (" + mSettings.ZoneInfoSettings.GetKeyFromIndex( i ).ToString() + ")" );
			}
		}

		private void PopulatePresetEditor( bool clear = false )
		{
			if( !clear && LibraryListBox.SelectedIndex > -1 && LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count )
			{
				PresetNameTextBox.Enabled = true;
				PresetDatePicker.Enabled = true;
				PresetTimePicker.Enabled = true;

				PresetZoneDropdown.Enabled = true;
				PresetZoneTextBox.Enabled = true;

				WaymarkACheckbox.Enabled = true;
				WaymarkATextBox_X.Enabled = true;
				WaymarkATextBox_Y.Enabled = true;
				WaymarkATextBox_Z.Enabled = true;

				WaymarkBCheckbox.Enabled = true;
				WaymarkBTextBox_X.Enabled = true;
				WaymarkBTextBox_Y.Enabled = true;
				WaymarkBTextBox_Z.Enabled = true;

				WaymarkCCheckbox.Enabled = true;
				WaymarkCTextBox_X.Enabled = true;
				WaymarkCTextBox_Y.Enabled = true;
				WaymarkCTextBox_Z.Enabled = true;

				WaymarkDCheckbox.Enabled = true;
				WaymarkDTextBox_X.Enabled = true;
				WaymarkDTextBox_Y.Enabled = true;
				WaymarkDTextBox_Z.Enabled = true;

				Waymark1Checkbox.Enabled = true;
				Waymark1TextBox_X.Enabled = true;
				Waymark1TextBox_Y.Enabled = true;
				Waymark1TextBox_Z.Enabled = true;

				Waymark2Checkbox.Enabled = true;
				Waymark2TextBox_X.Enabled = true;
				Waymark2TextBox_Y.Enabled = true;
				Waymark2TextBox_Z.Enabled = true;

				Waymark3Checkbox.Enabled = true;
				Waymark3TextBox_X.Enabled = true;
				Waymark3TextBox_Y.Enabled = true;
				Waymark3TextBox_Z.Enabled = true;

				Waymark4Checkbox.Enabled = true;
				Waymark4TextBox_X.Enabled = true;
				Waymark4TextBox_Y.Enabled = true;
				Waymark4TextBox_Z.Enabled = true;

				LibraryPresetUpdateButton.Enabled = true;

				PresetNameTextBox.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Name;
				PresetDatePicker.Value = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified.LocalDateTime.Date;
				PresetTimePicker.Value = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified.LocalDateTime;

				//	Set the zone in the dropdown if recognized; otherwise set custom and enable the text box for manual entry.  Remember that the indices will be off by one since we have custom as a zone ID option at the top of the list.
				bool isKnownZone = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID != 0 && mSettings.ZoneInfoSettings.ZoneDataExists( mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID );
				PresetZoneDropdown.SelectedIndex = isKnownZone ? mSettings.ZoneInfoSettings.GetIndex( mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID ) + 1 : 0;
				PresetZoneTextBox.Enabled = !isKnownZone;
				PresetZoneTextBox.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID.ToString();

				WaymarkACheckbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].IsEnabled;
				WaymarkATextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.X.ToString();
				WaymarkATextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Y.ToString();
				WaymarkATextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Z.ToString();

				WaymarkBCheckbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].IsEnabled;
				WaymarkBTextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.X.ToString();
				WaymarkBTextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Y.ToString();
				WaymarkBTextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Z.ToString();

				WaymarkCCheckbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].IsEnabled;
				WaymarkCTextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.X.ToString();
				WaymarkCTextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Y.ToString();
				WaymarkCTextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Z.ToString();

				WaymarkDCheckbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].IsEnabled;
				WaymarkDTextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.X.ToString();
				WaymarkDTextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Y.ToString();
				WaymarkDTextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Z.ToString();

				Waymark1Checkbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].IsEnabled;
				Waymark1TextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.X.ToString();
				Waymark1TextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Y.ToString();
				Waymark1TextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Z.ToString();

				Waymark2Checkbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].IsEnabled;
				Waymark2TextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.X.ToString();
				Waymark2TextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Y.ToString();
				Waymark2TextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Z.ToString();

				Waymark3Checkbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].IsEnabled;
				Waymark3TextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.X.ToString();
				Waymark3TextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Y.ToString();
				Waymark3TextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Z.ToString();

				Waymark4Checkbox.Checked = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].IsEnabled;
				Waymark4TextBox_X.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.X.ToString();
				Waymark4TextBox_Y.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Y.ToString();
				Waymark4TextBox_Z.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Z.ToString();
			}
			else
			{
				PresetNameTextBox.Enabled = false;
				PresetDatePicker.Enabled = false;
				PresetTimePicker.Enabled = false;

				PresetZoneDropdown.Enabled = false;
				PresetZoneTextBox.Enabled = false;

				WaymarkACheckbox.Enabled = false;
				WaymarkATextBox_X.Enabled = false;
				WaymarkATextBox_Y.Enabled = false;
				WaymarkATextBox_Z.Enabled = false;

				WaymarkBCheckbox.Enabled = false;
				WaymarkBTextBox_X.Enabled = false;
				WaymarkBTextBox_Y.Enabled = false;
				WaymarkBTextBox_Z.Enabled = false;

				WaymarkCCheckbox.Enabled = false;
				WaymarkCTextBox_X.Enabled = false;
				WaymarkCTextBox_Y.Enabled = false;
				WaymarkCTextBox_Z.Enabled = false;

				WaymarkDCheckbox.Enabled = false;
				WaymarkDTextBox_X.Enabled = false;
				WaymarkDTextBox_Y.Enabled = false;
				WaymarkDTextBox_Z.Enabled = false;

				Waymark1Checkbox.Enabled = false;
				Waymark1TextBox_X.Enabled = false;
				Waymark1TextBox_Y.Enabled = false;
				Waymark1TextBox_Z.Enabled = false;

				Waymark2Checkbox.Enabled = false;
				Waymark2TextBox_X.Enabled = false;
				Waymark2TextBox_Y.Enabled = false;
				Waymark2TextBox_Z.Enabled = false;

				Waymark3Checkbox.Enabled = false;
				Waymark3TextBox_X.Enabled = false;
				Waymark3TextBox_Y.Enabled = false;
				Waymark3TextBox_Z.Enabled = false;

				Waymark4Checkbox.Enabled = false;
				Waymark4TextBox_X.Enabled = false;
				Waymark4TextBox_Y.Enabled = false;
				Waymark4TextBox_Z.Enabled = false;

				LibraryPresetUpdateButton.Enabled = false;

				PresetNameTextBox.Text = "";
				PresetDatePicker.Value = DateTime.Now;
				PresetTimePicker.Value = DateTime.Now;

				PresetZoneDropdown.SelectedIndex = 0;
				PresetZoneTextBox.Text = "";

				WaymarkACheckbox.Checked = false;
				WaymarkATextBox_X.Text = "";
				WaymarkATextBox_Y.Text = "";
				WaymarkATextBox_Z.Text = "";


				WaymarkBCheckbox.Checked = false;
				WaymarkBTextBox_X.Text = "";
				WaymarkBTextBox_Y.Text = "";
				WaymarkBTextBox_Z.Text = "";


				WaymarkCCheckbox.Checked = false;
				WaymarkCTextBox_X.Text = "";
				WaymarkCTextBox_Y.Text = "";
				WaymarkCTextBox_Z.Text = "";


				WaymarkDCheckbox.Checked = false;
				WaymarkDTextBox_X.Text = "";
				WaymarkDTextBox_Y.Text = "";
				WaymarkDTextBox_Z.Text = "";


				Waymark1Checkbox.Checked = false;
				Waymark1TextBox_X.Text = "";
				Waymark1TextBox_Y.Text = "";
				Waymark1TextBox_Z.Text = "";


				Waymark2Checkbox.Checked = false;
				Waymark2TextBox_X.Text = "";
				Waymark2TextBox_Y.Text = "";
				Waymark2TextBox_Z.Text = "";


				Waymark3Checkbox.Checked = false;
				Waymark3TextBox_X.Text = "";
				Waymark3TextBox_Y.Text = "";
				Waymark3TextBox_Z.Text = "";


				Waymark4Checkbox.Checked = false;
				Waymark4TextBox_X.Text = "";
				Waymark4TextBox_Y.Text = "";
				Waymark4TextBox_Z.Text = "";
			}
		}
		#endregion

		//	Event Handlers
		#region Event Handlers
		private void CharacterFolderBrowseButton_Click( object sender, EventArgs e )
		{
			CharacterDataFolderDialog.ShowDialog();
			if( ( CharacterDataFolderDialog.SelectedPath != null ) && Directory.Exists( CharacterDataFolderDialog.SelectedPath ) )
			{
				mSettings.ProgramSettings.CharacterDataFolderPath = CharacterDataFolderDialog.SelectedPath;
				CharacterDataFolderTextBox.Text = mSettings.ProgramSettings.CharacterDataFolderPath;
				PopulateCharacterListDropdown();
			}
		}

		private void CharacterListDropdown_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( CharacterListDropdown.SelectedIndex > -1 && CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length )
			{
				try
				{
					mGamePresetContainer = mGameDataHandler.ReadGameData( mCharacterFolderList[CharacterListDropdown.SelectedIndex] + '\\' + mSettings.GameDataSettings.WaymarkDataFileName );
					PopulateGamePresetListBox();
				}
				catch( Exception exception )
				{
					MessageBox.Show( "An error has occured while reading the file for character \"" + mSettings.CharacterAliasSettings.GetAlias( mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last() ) + "\": " + exception.Message, "Failure!" );
					PopulateGamePresetListBox( true );
				}
			}
			else
			{
				PopulateGamePresetListBox( true );
			}
		}

		private void CopyToLibraryButton_Click( object sender, EventArgs e )
		{
			if( GamePresetListBox.SelectedIndex > -1 && GamePresetListBox.SelectedIndex < mSettings.GameDataSettings.NumberOfPresets &&
				CharacterListDropdown.SelectedIndex > -1 && CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length )
			{
				WaymarkPreset newPreset = new WaymarkPreset( mGamePresetContainer.Presets[GamePresetListBox.SelectedIndex] );
				newPreset.Name = mSettings.ZoneInfoSettings.GetZoneName( newPreset.ZoneID ) + " (" + mSettings.CharacterAliasSettings.GetAlias( mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last() ) + " Slot " + ( GamePresetListBox.SelectedIndex + 1 ).ToString() + ")";
				mPresetLibrary.AddPreset( newPreset );
				PopulateLibraryListBox();
			}
		}

		private void CopyToGameButton_Click( object sender, EventArgs e )
		{
			if( LibraryListBox.SelectedIndex > -1 && LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count &&
				GamePresetListBox.SelectedIndex > -1 && GamePresetListBox.SelectedIndex < mSettings.GameDataSettings.NumberOfPresets )
			{
				bool actuallyCopyPreset = true;

				if( mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID == 0 )
				{
					MessageBox.Show( "This preset has a ZoneID of zero.  It cannot be copied to the game without a proper ZoneID.", "Invalid Zone ID" );
					actuallyCopyPreset = false;
				}
				else if( !mSettings.ZoneInfoSettings.ZoneDataExists( mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID ) )
				{
					if( MessageBox.Show( "This preset has an unrecognized zone ID.  This may mean that the zone dictionary is out of date, but it could also mean that the ID is invalid.  It is unknown how the game will respond to an invalid zone ID.  Are you sure that you wish to continue?", "Unrecognized Zone ID", MessageBoxButtons.OKCancel ) == DialogResult.Cancel ) actuallyCopyPreset = false;
				}

				if( actuallyCopyPreset )
				{
					mGamePresetContainer.ReplacePreset( (uint)GamePresetListBox.SelectedIndex, mPresetLibrary.Presets[LibraryListBox.SelectedIndex] );
				}

				PopulateGamePresetListBox();
			}
		}

		private void GamePresetListBox_SelectedIndexChanged( object sender, EventArgs e )
		{
			PopulateGamePresetInfoBox();
		}

		private void WriteGameFileButton_Click( object sender, EventArgs e )
		{
			//	Might be overkill, but really validate that we're writing to the character folder that we think we are, and then prompt the user to confirm as well.
			if( CharacterListDropdown.SelectedIndex > -1 &&
				CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length &&
				mSettings.CharacterAliasSettings.GetAlias( mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last() ) == CharacterListDropdown.SelectedItem.ToString() &&
				MessageBox.Show( "Are you certain that you wish to write these presets to the game file for the character \"" + mSettings.CharacterAliasSettings.GetAlias( mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last() ) + "\"?  This cannot be undone.  Ensure that you are logged out of this character before continuing.", "Confirm Game File Write", MessageBoxButtons.OKCancel ) == DialogResult.OK )
			{
				try
				{
					mGameDataHandler.WriteGameData( mCharacterFolderList[CharacterListDropdown.SelectedIndex] + '\\' + mSettings.GameDataSettings.WaymarkDataFileName, mGameDataHandler.ConstructGameData( mGamePresetContainer ) );
					MessageBox.Show( "The game file has been written.", "Success!" );
				}
				catch( Exception exception )
				{
					MessageBox.Show( "An error has occured while writing the data: " + exception.Message, "Failure!" );
				}
			}
		}

		private void WaymarkLibrarianForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			if( CharacterListDropdown.SelectedIndex > -1 && CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length )
			{
				mSettings.ProgramSettings.DefaultCharacterID = mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last();
			}
			mSettings.SaveConfig();
			mPresetLibrary.SaveConfig();
		}

		private void ClearGameSlotButton_Click( object sender, EventArgs e )
		{
			if( mGamePresetContainer != null && GamePresetListBox.SelectedIndex >= 0 && GamePresetListBox.SelectedIndex < mSettings.GameDataSettings.NumberOfPresets )
			{
				mGamePresetContainer.ReplacePreset( (uint)GamePresetListBox.SelectedIndex, new WaymarkPreset() );
				PopulateGamePresetListBox();
			}
		}

		private void SetCharacterAliasButton_Click( object sender, EventArgs e )
		{
			int currentCharacterIndex = CharacterListDropdown.SelectedIndex;
			if( currentCharacterIndex > -1 && currentCharacterIndex < mCharacterFolderList.Length )
			{
				string characterFolderName = mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last();
				string input = Interaction.InputBox( "Input the alias (friendly name) that you wish to use for the character " + characterFolderName, "Set Alias", mSettings.CharacterAliasSettings.GetAlias( characterFolderName ) );
				if( input.Length > 0 )
				{
					mSettings.CharacterAliasSettings.SetAlias( mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last(), input );
					PopulateCharacterListDropdown();
					CharacterListDropdown.SelectedIndex = currentCharacterIndex;
				}
			}
		}

		private void LibraryListBox_SelectedIndexChanged( object sender, EventArgs e )
		{
			PopulatePresetEditor();
		}

		private void LibraryPresetUpdateButton_Click( object sender, EventArgs e )
		{
			if( LibraryListBox.SelectedIndex > -1 && LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count )
			{
				//	Name is whatever's in the box.
				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Name = PresetNameTextBox.Text;

				//	Limit the date to something feasible.
				if( ( PresetDatePicker.Value.Date + PresetTimePicker.Value.TimeOfDay ).ToUniversalTime() > mSettings.ProgramSettings.EarliestPresetTimestampAllowed )
				{
					mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified = ( PresetDatePicker.Value.Date + PresetTimePicker.Value.TimeOfDay ).ToUniversalTime();
				}
				else
				{
					mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified = mSettings.ProgramSettings.EarliestPresetTimestampAllowed;
				}

				//	The Zone ID will always come from the text box because the dropdown will populate that in the event that the user cannot edit it.
				UInt16 tempShort;
				if( UInt16.TryParse( PresetZoneTextBox.Text, out tempShort ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID = tempShort;

				//	Read all of the waymark flags and positions.
				double tempDouble;
				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].IsEnabled = WaymarkACheckbox.Checked;
				if( double.TryParse( WaymarkATextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.X = tempDouble;
				if( double.TryParse( WaymarkATextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Y = tempDouble;
				if( double.TryParse( WaymarkATextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].IsEnabled = WaymarkACheckbox.Checked;
				if( double.TryParse( WaymarkBTextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.X = tempDouble;
				if( double.TryParse( WaymarkBTextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Y = tempDouble;
				if( double.TryParse( WaymarkBTextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].IsEnabled = WaymarkCCheckbox.Checked;
				if( double.TryParse( WaymarkCTextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.X = tempDouble;
				if( double.TryParse( WaymarkCTextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Y = tempDouble;
				if( double.TryParse( WaymarkCTextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].IsEnabled = WaymarkDCheckbox.Checked;
				if( double.TryParse( WaymarkDTextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.X = tempDouble;
				if( double.TryParse( WaymarkDTextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Y = tempDouble;
				if( double.TryParse( WaymarkDTextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].IsEnabled = Waymark1Checkbox.Checked;
				if( double.TryParse( Waymark1TextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.X = tempDouble;
				if( double.TryParse( Waymark1TextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Y = tempDouble;
				if( double.TryParse( Waymark1TextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].IsEnabled = Waymark2Checkbox.Checked;
				if( double.TryParse( Waymark2TextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.X = tempDouble;
				if( double.TryParse( Waymark2TextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Y = tempDouble;
				if( double.TryParse( Waymark2TextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].IsEnabled = Waymark3Checkbox.Checked;
				if( double.TryParse( Waymark3TextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.X = tempDouble;
				if( double.TryParse( Waymark3TextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Y = tempDouble;
				if( double.TryParse( Waymark3TextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Z = tempDouble;

				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].IsEnabled = Waymark4Checkbox.Checked;
				if( double.TryParse( Waymark4TextBox_X.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.X = tempDouble;
				if( double.TryParse( Waymark4TextBox_Y.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Y = tempDouble;
				if( double.TryParse( Waymark4TextBox_Z.Text, out tempDouble ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Z = tempDouble;

				//	Repopulate the library preset list.
				PopulateLibraryListBox();
			}
		}

		private void LibraryPresetNewButton_Click( object sender, EventArgs e )
		{
			WaymarkPreset newPreset = new WaymarkPreset();
			newPreset.Name = "New Preset";
			mPresetLibrary.AddPreset( newPreset );

			PopulateLibraryListBox();
		}

		private void LibraryPresetImportButton_Click( object sender, EventArgs e )
		{
			string input = Interaction.InputBox( "Paste the JSON (*barf*) for the preset that you wish to import.  Accepts Paisley Park exports as well, but you'll have to manually set the zone ID for them.", "Import Preset" );

			try
			{
				WaymarkPreset newPreset = new WaymarkPreset();

				dynamic importObj = JsonConvert.DeserializeObject( input );

				newPreset.Name = importObj.Name;
				if( DynamicObjectPropertyExists( importObj, "ZoneID" ) )
				{
					newPreset.ZoneID = importObj.ZoneID;
				}
				if( DynamicObjectPropertyExists( importObj, "Time" ) )
				{
					newPreset.LastModified = importObj.Time;
				}
				if( DynamicObjectPropertyExists( importObj, "A" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].IsEnabled = importObj.A.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.X = importObj.A.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Y = importObj.A.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Z = importObj.A.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "B" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].IsEnabled = importObj.B.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.X = importObj.B.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Y = importObj.B.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Z = importObj.B.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "C" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].IsEnabled = importObj.C.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.X = importObj.C.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Y = importObj.C.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Z = importObj.C.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "D" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].IsEnabled = importObj.D.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.X = importObj.D.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Y = importObj.D.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Z = importObj.D.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "One" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].IsEnabled = importObj.One.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.X = importObj.One.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Y = importObj.One.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Z = importObj.One.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "Two" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].IsEnabled = importObj.Two.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.X = importObj.Two.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Y = importObj.Two.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Z = importObj.Two.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "Three" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].IsEnabled = importObj.Three.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.X = importObj.Three.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Y = importObj.Three.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Z = importObj.Three.Z;
				}
				if( DynamicObjectPropertyExists( importObj, "Four" ) )
				{
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].IsEnabled = importObj.Four.Active;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.X = importObj.Four.X;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Y = importObj.Four.Y;
					newPreset.Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Z = importObj.Four.Z;
				}

				mPresetLibrary.AddPreset( newPreset );
			}
			catch
			{
				MessageBox.Show( "Unable to import the provided data.  This is probably due to improperly-formed JSON or missing fields.", "Import Failed!" );
			}

			PopulateLibraryListBox();
		}

		private void LibraryPresetRemoveButton_Click( object sender, EventArgs e )
		{
			if( LibraryListBox.SelectedIndex > -1 &&
				LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count &&
				MessageBox.Show( "Are you certain that you wish to delete the preset \"" + mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Name + "\"?  This is permanent.", "Confirm Preset Deletion", MessageBoxButtons.OKCancel ) == DialogResult.OK )
			{
				mPresetLibrary.RemovePreset( LibraryListBox.SelectedIndex );
				PopulateLibraryListBox();
			}
		}

		private void LibraryPresetExportButton_Click( object sender, EventArgs e )
		{
			if( LibraryListBox.SelectedIndex > -1 &&
				LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count )
			{
				PresetExportObject exportObj = new PresetExportObject();
				exportObj.Name = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Name;
				exportObj.ZoneID = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID;
				exportObj.Time = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified;

				exportObj.A.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.X;
				exportObj.A.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Y;
				exportObj.A.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].Pos.Z;
				exportObj.A.ID = WaymarkPreset.GetWaymarkNumber( 'A' );
				exportObj.A.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'A' )].IsEnabled;

				exportObj.B.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.X;
				exportObj.B.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Y;
				exportObj.B.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].Pos.Z;
				exportObj.B.ID = WaymarkPreset.GetWaymarkNumber( 'B' );
				exportObj.B.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'B' )].IsEnabled;

				exportObj.C.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.X;
				exportObj.C.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Y;
				exportObj.C.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].Pos.Z;
				exportObj.C.ID = WaymarkPreset.GetWaymarkNumber( 'C' );
				exportObj.C.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'C' )].IsEnabled;

				exportObj.D.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.X;
				exportObj.D.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Y;
				exportObj.D.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].Pos.Z;
				exportObj.D.ID = WaymarkPreset.GetWaymarkNumber( 'D' );
				exportObj.D.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( 'D' )].IsEnabled;

				exportObj.One.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.X;
				exportObj.One.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Y;
				exportObj.One.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].Pos.Z;
				exportObj.One.ID = WaymarkPreset.GetWaymarkNumber( '1' );
				exportObj.One.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '1' )].IsEnabled;

				exportObj.Two.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.X;
				exportObj.Two.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Y;
				exportObj.Two.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].Pos.Z;
				exportObj.Two.ID = WaymarkPreset.GetWaymarkNumber( '2' );
				exportObj.Two.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '2' )].IsEnabled;

				exportObj.Three.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.X;
				exportObj.Three.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Y;
				exportObj.Three.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].Pos.Z;
				exportObj.Three.ID = WaymarkPreset.GetWaymarkNumber( '3' );
				exportObj.Three.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '3' )].IsEnabled;

				exportObj.Four.X = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.X;
				exportObj.Four.Y = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Y;
				exportObj.Four.Z = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].Pos.Z;
				exportObj.Four.ID = WaymarkPreset.GetWaymarkNumber( '4' );
				exportObj.Four.Active = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Waymarks[WaymarkPreset.GetWaymarkNumber( '4' )].IsEnabled;

				string objString = JsonConvert.SerializeObject( exportObj );
				Interaction.InputBox( "Copy the JSON export string below:", "Preset Export", objString );
			}
		}

		private void PresetZoneDropdown_SelectedIndexChanged( object sender, EventArgs e )
		{
			//	Enable the text box if "Custom" is selected.
			PresetZoneTextBox.Enabled = PresetZoneDropdown.SelectedIndex == 0;

			//	Set the text in the text box if a valid zone is selected in the dropdown.  Remember that the indices will be off by one since we have custom as a zone ID option at the top of the list.
			if( PresetZoneDropdown.SelectedIndex > 0 )
			{
				PresetZoneTextBox.Text = mSettings.ZoneInfoSettings.GetKeyFromIndex( PresetZoneDropdown.SelectedIndex - 1 ).ToString();
			}
		}
		
		private void HelpLinkLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
		{
			System.Diagnostics.Process.Start( "https://github.com/PunishedPineapple/WaymarkLibrarian#waymarklibrarian" );
		}
		#endregion

		private void UpdateLinkLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
		{
			System.Diagnostics.Process.Start( "https://github.com/PunishedPineapple/WaymarkLibrarian/releases" );
		}
	}
}