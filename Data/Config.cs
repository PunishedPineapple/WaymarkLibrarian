using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace WaymarkLibrarian
{
	class Config
	{
		public Config()
		{
			//	Get the path to the settings folder.
			ConfigFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + "\\PunishedPineapple\\WaymarkLibrarian\\";

			//	Create the children.
			ProgramSettings = new ProgramConfig( ConfigFolderPath + "Options.cfg" );
			GameDataSettings = new GameDataConfig( ConfigFolderPath + "GameData.cfg" );
			ZoneInfoSettings = new ZoneInfo( ConfigFolderPath + "ZoneDictionary.dat" );

			//	See how long it's been since we checked for updated config.  If it's more than desired, update now.
			if( DateTimeOffset.UtcNow - ProgramSettings.LastUpdateCheck > TimeSpan.FromDays( ProgramSettings.UpdateCheckFrequency_Days ) )
			{
				CheckForUpdates();

				GameDataSettings.Reload();
				ZoneInfoSettings.Reload();
			}

			//	The alias file lives one directory up since we may want to share it with other programs in the future.
			CharacterAliasSettings = new CharacterAliasConfig( Directory.GetParent( ConfigFolderPath ).Parent.FullName + "\\CharacterAliases.cfg" );
		}

		public void SaveConfig()
		{
			//	Create the directories that hold the config files if they don't exist.
			Directory.CreateDirectory( ConfigFolderPath );

			//	Ask our children to save themselves.  The game data config should never be written, only read.  The same applies to the zone dictionary.  The children are expected to back up old files on their own if applicable.
			ProgramSettings.SaveConfig();
			CharacterAliasSettings.SaveConfig();
		}

		protected void CheckForUpdates()
		{
			//	Make a temporary directory to hold update info.
			string updateFolderPath = ConfigFolderPath + "\\updates.temp";
			Directory.CreateDirectory( updateFolderPath );

			string currentVersionsFilePath = updateFolderPath + "\\CurrentVersions.dat";
			WebClient webClient = new WebClient();
			webClient.DownloadFile( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/CurrentVersions.dat", currentVersionsFilePath );

			//	Set the last updated time to now.
			ProgramSettings.LastUpdateCheck = DateTimeOffset.UtcNow;

			//	Read the config if we have it.
			if( File.Exists( currentVersionsFilePath ) )
			{
				string programVer = "";
				string gameDataCfgVer = "";
				string zoneDictionaryDatVer = "";
				List<string> lines = File.ReadLines( currentVersionsFilePath ).ToList();
				foreach( string line in lines )
				{
					if( line.Split( '=' ).First().Trim().Equals( "Program" ) ) programVer = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "GameData.cfg" ) ) gameDataCfgVer = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "ZoneDictionary.dat" ) ) zoneDictionaryDatVer = line.Split( '=' ).Last().Trim();
				}

				//	Notify the user if a new version of the program is available.
				if( programVer.Length > 0 &&
					programVer != FileVersionInfo.GetVersionInfo( System.Reflection.Assembly.GetExecutingAssembly().Location ).FileVersion )
				{
					MessageBox.Show( "A new version of this program is available.  Go to https://github.com/PunishedPineapple/WaymarkLibrarian/releases to download the latest version.", "New Version" );
				}
				//	Only check for configuration updates if the program is up to date (since file formats could conceivably change).
				else
				{
					//	Update game data config.
					if( gameDataCfgVer.Length > 0 &&
					gameDataCfgVer != GameDataSettings.GameVersion &&
					MessageBox.Show( "A new version of the game data configuration file was found.  Update now?", "Update?", MessageBoxButtons.YesNo ) == DialogResult.Yes )
					{
						try
						{
							webClient.DownloadFile( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/GameData.cfg", updateFolderPath + "\\GameData.cfg" );
							File.Copy( updateFolderPath + "\\GameData.cfg", GameDataSettings.ConfigFilePath, true );
						}
						catch
						{
							MessageBox.Show( "Update failed!", "Failure!" );
						}
					}

					//	Update zone dictionary.
					if( zoneDictionaryDatVer.Length > 0 &&
						zoneDictionaryDatVer != ZoneInfoSettings.GameVersion &&
						MessageBox.Show( "A new version of the zone dictionary file was found.  Update now?", "Update?", MessageBoxButtons.YesNo ) == DialogResult.Yes )
					{
						try
						{
							webClient.DownloadFile( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/ZoneDictionary.dat", updateFolderPath + "\\ZoneDictionary.dat" );
							File.Copy( updateFolderPath + "\\ZoneDictionary.dat", ZoneInfoSettings.ConfigFilePath, true );
						}
						catch
						{
							MessageBox.Show( "Update failed!", "Failure!" );
						}
					}
				}
			}

			//	Clean up the update files.
			Directory.Delete( updateFolderPath, true );
		}

		public ProgramConfig ProgramSettings { get; protected set; }
		public GameDataConfig GameDataSettings { get; protected set; }
		public ZoneInfo ZoneInfoSettings { get; protected set; }
		public CharacterAliasConfig CharacterAliasSettings { get; protected set; }
		public string ConfigFolderPath { get; protected set; }
	}
}
