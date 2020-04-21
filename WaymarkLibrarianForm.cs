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

			//	WinForms Stuff
			InitializeComponent();

			//	Read config file if it exists; otherwise make one with default settings.
			//	***** TODO *****
			mProgramConfig = new ProgramConfig();
			mGameDataConfig = new GameDataConfig( "UISAVE.DAT", 0x6C97, 5u, 8u, 12u, 8u, 0x31 );
			mGameDataHandler = new GameDataHandler( mGameDataConfig );

			//	Put it in user data.

			//SelectedPresetInfoBox.Text = gamePresetContainer.GetDataString();
			//	Load zone ID data.

			//	Load waymark library.

			//	Initialize character list (including aliases), select first character (can a default character be in settings? add button for it?), and populate current game waymark data.
		}

		private void GamePresetListBox_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( mGamePresetContainer != null )
			{
				if( GamePresetListBox.SelectedIndex < 0 )
				{
					SelectedPresetInfoBox.Text = "";
				}
				else
				{
					SelectedPresetInfoBox.Text = mGamePresetContainer[(uint)GamePresetListBox.SelectedIndex].GetPresetDataString();
				}
			}
		}

		private WaymarkPresets mGamePresetContainer;
		private ProgramConfig mProgramConfig;
		private string[] mCharacterFolderList;
		private GameDataConfig mGameDataConfig;
		private GameDataHandler mGameDataHandler;

		private void PopulateCharacterListDropdown()
		{
			mCharacterFolderList = Directory.GetDirectories( mProgramConfig.CharacterDataFolderPath, "FFXIV_CHR*", SearchOption.TopDirectoryOnly );
			foreach( string dir in mCharacterFolderList )
			{
				CharacterListDropdown.Items.Add( dir.Split('\\').Last() );
			}
		}
		private void PopulateGamePresetListBox( bool clear = false )
		{
			GamePresetListBox.SelectedIndex = -1;
			GamePresetListBox.Items.Clear();

			if( !clear && ( mGamePresetContainer != null ) )
			{
				for( uint i = 0u; i < mGamePresetContainer.Presets.Length; ++i )
				{
					GamePresetListBox.Items.Add( "Slot " + (i+1).ToString() + ": " + ( mGamePresetContainer[i].ZoneID == 0u ? "Empty" : ( "Zone " + mGamePresetContainer[i].ZoneID.ToString() ) ) );
				}
			}
		}

		private void CharacterFolderBrowseButton_Click( object sender, EventArgs e )
		{
			CharacterDataFolderDialog.ShowDialog();
			mProgramConfig.CharacterDataFolderPath = CharacterDataFolderDialog.SelectedPath;
			CharacterDataFolderTextBox.Text = mProgramConfig.CharacterDataFolderPath;
			PopulateCharacterListDropdown();
		}

		private void CharacterListDropdown_SelectionChangeCommitted( object sender, EventArgs e )
		{
			if( CharacterListDropdown.SelectedIndex < 0 )
			{
				PopulateGamePresetListBox( true );
			}
			else
			{
				mGamePresetContainer = mGameDataHandler.ReadGameData( mCharacterFolderList[CharacterListDropdown.SelectedIndex] + '\\' + mGameDataConfig.WaymarkDataFileName );
			}

			PopulateGamePresetListBox();
		}
	}
}