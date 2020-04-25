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

//	Use for InputBox rather than rolling our own trivial input dialog.
using Microsoft.VisualBasic;

namespace WaymarkLibrarian
{
	public partial class WaymarkLibrarianForm : Form
	{
		public WaymarkLibrarianForm()
		{
			//	***** TODO LIST: *****
			//		Make import button bring up a context menu with both import and empty new options (work in pp format import too).
			//		Make copy to game bring up a context menu of which slot to use.
			//		Restrict date dropdown to not allow before 5.2 patch.  Have date in zone dictionary with release date for each zone?
			//		Add a button for default character setting?
			//		Add expected file size to game data settings and check that before doing anything.  Refuse to load if it doesn't match the expected size.

			//	WinForms Stuff
			InitializeComponent();

			//	Control setup.
			PresetTimePicker.Format = DateTimePickerFormat.Time;
			PresetTimePicker.ShowUpDown = true;

			//	Get config settings.
			mSettings = new Config();

			//	Set up the object that handles interfacing with the game save files.
			mGameDataHandler = new GameDataHandler( mSettings.GameDataSettings );

			//	Load zone ID dictionary data.

			//	Load waymark library.
			mPresetLibrary = new PresetLibrary( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + "\\WaymarkLibrarian\\WaymarkLibrary.xml" );
			PopulateLibraryListBox();

			//	Initialize the game settings folder, character list, and game preset list.
			if( Directory.Exists( mSettings.ProgramSettings.CharacterDataFolderPath ) )
			{
				CharacterDataFolderTextBox.Text = mSettings.ProgramSettings.CharacterDataFolderPath;
				PopulateCharacterListDropdown();
				if( ( mSettings.ProgramSettings.DefaultCharacterID.Length > 0 ) && File.Exists( mSettings.ProgramSettings.CharacterDataFolderPath + '\\' + mSettings.ProgramSettings.DefaultCharacterID + '\\' + mSettings.GameDataSettings.WaymarkDataFileName ) )
				{
					for( int i = 0; i < CharacterListDropdown.Items.Count; ++i )
					{
						if( mCharacterFolderList[i].Split( '\\' ).Last() == mSettings.ProgramSettings.DefaultCharacterID )
						{
							CharacterListDropdown.SelectedIndex = i;
							mGamePresetContainer = mGameDataHandler.ReadGameData( mCharacterFolderList[CharacterListDropdown.SelectedIndex] + '\\' + mSettings.GameDataSettings.WaymarkDataFileName );
							PopulateGamePresetListBox();
							break;
						}
					}
				}
			}
		}

		private GamePresetContainer mGamePresetContainer;
		private GameDataHandler mGameDataHandler;
		private PresetLibrary mPresetLibrary;
		private string[] mCharacterFolderList;
		private Config mSettings;

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
					GamePresetListBox.Items.Add( "Slot " + ( i + 1 ).ToString() + ": " + ( mGamePresetContainer[i].ZoneID == 0u ? "Empty" : ( "Zone " + mGamePresetContainer[i].ZoneID.ToString() ) ) );
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
				SelectedPresetInfoBox.Text = mGamePresetContainer[(uint)GamePresetListBox.SelectedIndex].GetPresetDataString();
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

			if( !clear )
			{
				foreach( WaymarkPreset preset in mPresetLibrary.Presets )
				{
					LibraryListBox.Items.Add( preset.Name + " (" + preset.ZoneID.ToString() + ") (" + preset.LastModified.ToLocalTime().ToString( "g" ) + ")" );
				}

				if( previousSelectedIndex < LibraryListBox.Items.Count )
				{
					LibraryListBox.SelectedIndex = previousSelectedIndex;
				}
			}
		}

		private void PopulatePresetEditor( bool clear = false )
		{
			if( !clear && LibraryListBox.SelectedIndex > -1 && LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count )
			{
				PresetNameTextBox.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Name;
				PresetDatePicker.Value = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified.LocalDateTime.Date;
				PresetTimePicker.Value = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified.LocalDateTime;

				//	*****TODO: Make this the actual dropdown once the zone dictionary is implemented.*****
				PresetZoneDropdown.Text = mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID.ToString();

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
				PresetNameTextBox.Text = "";
				PresetDatePicker.Value = DateTime.Now;
				PresetTimePicker.Value = DateTime.Now;

				//	*****TODO: Set dropdown index instead once zone dictionary is implemented.*****
				PresetZoneDropdown.Text = "";

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
			mGamePresetContainer = mGameDataHandler.ReadGameData( mCharacterFolderList[CharacterListDropdown.SelectedIndex] + '\\' + mSettings.GameDataSettings.WaymarkDataFileName );
			PopulateGamePresetListBox( CharacterListDropdown.SelectedIndex < 0 );
		}

		private void CopyToLibraryButton_Click( object sender, EventArgs e )
		{
			if( GamePresetListBox.SelectedIndex > -1 || GamePresetListBox.SelectedIndex < mSettings.GameDataSettings.NumberOfPresets )
			{
				mPresetLibrary.AddPreset( mGamePresetContainer.Presets[GamePresetListBox.SelectedIndex] );
				PopulateLibraryListBox();
			}
		}

		private void CopyToGameButton_Click( object sender, EventArgs e )
		{
			//	*****TODO: Maybe pop up a context menu for which preset slot to overwrite rather than just using the selected one?*****
			if( LibraryListBox.SelectedIndex > -1 && LibraryListBox.SelectedIndex < mPresetLibrary.Presets.Count && GamePresetListBox.SelectedIndex > -1 && GamePresetListBox.SelectedIndex < mSettings.GameDataSettings.NumberOfPresets )
			{
				mGamePresetContainer.ReplacePreset( (uint)GamePresetListBox.SelectedIndex, mPresetLibrary.Presets[LibraryListBox.SelectedIndex] );
				PopulateGamePresetListBox();
			}
		}

		private void GamePresetListBox_SelectedIndexChanged( object sender, EventArgs e )
		{
			PopulateGamePresetInfoBox();
		}

		private void WriteGameFileButton_Click( object sender, EventArgs e )
		{
			//	*****TODO: Prompt user to confirm, also validate file is really selected character.*****
			mGameDataHandler.WriteGameData( mCharacterFolderList[CharacterListDropdown.SelectedIndex] + '\\' + mSettings.GameDataSettings.WaymarkDataFileName, mGameDataHandler.ConstructGameData( mGamePresetContainer ) );
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
				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].Name = PresetNameTextBox.Text;
				mPresetLibrary.Presets[LibraryListBox.SelectedIndex].LastModified = ( PresetDatePicker.Value.Date + PresetTimePicker.Value.TimeOfDay ).ToUniversalTime();

				//	*****TODO: Make this use the actual dropdown once the zone dictionary is implemented.*****
				UInt16 tempShort;
				if( UInt16.TryParse( PresetZoneDropdown.Text, out tempShort ) ) mPresetLibrary.Presets[LibraryListBox.SelectedIndex].ZoneID = tempShort;

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

				PopulateLibraryListBox();
			}
		}
	}
}