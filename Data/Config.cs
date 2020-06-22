using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Net.Http;
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

			//	Check for updates if it is time to do so.
			if( ( DateTimeOffset.UtcNow - ProgramSettings.LastUpdateCheck > TimeSpan.FromDays( ProgramSettings.UpdateCheckFrequency_Days ) ) ||
				( DateTimeOffset.UtcNow - ProgramSettings.LastConfigUpdateCheck > TimeSpan.FromDays( ProgramSettings.UpdateCheckFrequency_Days ) && VersionInfoHelper.Parse( FileVersionInfo.GetVersionInfo( System.Reflection.Assembly.GetExecutingAssembly().Location ) ) == ProgramSettings.LastProgramUpdateVersionSeen ) )
			{
				CheckForUpdates();
				GameDataSettings.Reload();
				ZoneInfoSettings.Reload();
			}

			//	The alias file lives one directory up since we may want to share it with other programs.
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
			//	Set up a simple splash screen so the user knows what's going on.
			Form waitForm = new Form();
			waitForm.ControlBox = false;
			waitForm.MaximizeBox = false;
			waitForm.MinimizeBox = false;
			waitForm.ShowIcon = false;
			waitForm.StartPosition = FormStartPosition.CenterScreen;
			waitForm.MinimumSize = waitForm.Size;
			waitForm.MaximumSize = waitForm.Size;
			Label statusLabel = new Label();
			statusLabel.Size = waitForm.Size;
			statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			statusLabel.Text = "Checking for updates.";
			waitForm.Controls.Add( statusLabel );
			waitForm.Show();

			//	Make a backup directory to hold previous config in the case of bad updates.
			string backupFolderPath = ConfigFolderPath + "\\ConfigUpdatesBackup";
			if( !Directory.Exists( backupFolderPath ) ) Directory.CreateDirectory( backupFolderPath );

			//	Do the actual online checks.
			using( HttpClient httpClient = new HttpClient() )
			{
				//	Set the request timeout.  The default is unreasonable for how little data we are dealing with (under 10 KiB total).
				httpClient.Timeout = TimeSpan.FromSeconds( ProgramSettings.UpdateRequestTimeout_Sec );

				//	Empty version strings until we can fill them.
				string programVer = "";
				string gameDataCfgVer = "";
				string zoneDictionaryDatVer = "";
				bool versionInfoValid = false;

				//	Try to grab the current versions of things.
				try
				{
					//	Perform the http request.
					string currentVersionsRawData = httpClient.GetStringAsync( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/CurrentVersions.dat" ).Result;

					//	Process what we got back.
					string[] currentVersionsLines = currentVersionsRawData.Split( new char[] { '\r', '\n' } );
					foreach( string line in currentVersionsLines )
					{
						if( line.Split( '=' ).First().Trim().Equals( "Program" ) ) programVer = line.Split( '=' ).Last().Trim();
						if( line.Split( '=' ).First().Trim().Equals( "GameData.cfg" ) ) gameDataCfgVer = line.Split( '=' ).Last().Trim();
						if( line.Split( '=' ).First().Trim().Equals( "ZoneDictionary.dat" ) ) zoneDictionaryDatVer = line.Split( '=' ).Last().Trim();
					}

					//	Verify that we got the bits that we need.  This isn't the best validity check, but it's probably good enough.
					versionInfoValid = programVer.Length > 0 && gameDataCfgVer.Length > 0 && zoneDictionaryDatVer.Length > 0;

					//	Mark that we checked for *program* updates.
					ProgramSettings.LastUpdateCheck = DateTimeOffset.UtcNow;
				}
				catch( Exception exception )
				{
					//	If we couldn't get valid version info, mark that we tried.
					MessageBox.Show( "Unable to retrieve current version information.  This is likely due to lack of internet access or DNS lookup failure.  Full error is as follows:\r\n\r\n\r\n" + exception.ToString(), "Failure!" );
					ProgramSettings.LastUpdateCheck = DateTimeOffset.UtcNow;
					ProgramSettings.LastConfigUpdateCheck = DateTimeOffset.UtcNow;
				}

				//	If we have valid version info, proceed with the rest of it.
				if( versionInfoValid )
				{
					//	See if a newer version of the program is available than what we've seen.
					if( VersionInfoHelper.Parse( programVer ) > ProgramSettings.LastProgramUpdateVersionSeen )
					{
						MessageBox.Show( "A new version of this program is available.  Click on the update link in the main window or go to https://github.com/PunishedPineapple/WaymarkLibrarian/releases to download the latest version.", "New Version" );
						ProgramSettings.LastProgramUpdateVersionSeen = VersionInfoHelper.Parse( programVer );
					}

					//	Only handle configuration updates if the program itself is up to date (since file formats could conceivably change).
					if( VersionInfoHelper.Parse( FileVersionInfo.GetVersionInfo( System.Reflection.Assembly.GetExecutingAssembly().Location ) ) == ProgramSettings.LastProgramUpdateVersionSeen )
					{
						//	Update game data config.
						if( gameDataCfgVer != GameDataSettings.GameVersion &&
							MessageBox.Show( "A new version of the game data configuration file was found.  Update now?", "Update?", MessageBoxButtons.YesNo ) == DialogResult.Yes )
						{
							try
							{
								if( File.Exists( GameDataSettings.ConfigFilePath ) && Directory.Exists( backupFolderPath ) ) File.Copy( GameDataSettings.ConfigFilePath, backupFolderPath + "\\GameData.cfg." + DateTime.Now.ToString( "yyyy-MM-dd-HH-mm-ss" ) + ".bak", true );
								byte[] rawData = httpClient.GetByteArrayAsync( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/GameData.cfg" ).Result;
								File.WriteAllBytes( GameDataSettings.ConfigFilePath, rawData );
							}
							catch( Exception exception )
							{
								MessageBox.Show( "Offsets update failed: " + exception.ToString(), "Failure!" );
							}
						}

						//	Update zone dictionary.
						if( zoneDictionaryDatVer != ZoneInfoSettings.GameVersion &&
							MessageBox.Show( "A new version of the zone dictionary file was found.  Update now?", "Update?", MessageBoxButtons.YesNo ) == DialogResult.Yes )
						{
							try
							{
								if( File.Exists( ZoneInfoSettings.ConfigFilePath ) && Directory.Exists( backupFolderPath ) ) File.Copy( ZoneInfoSettings.ConfigFilePath, backupFolderPath + "\\ZoneDictionary.dat." + DateTime.Now.ToString( "yyyy-MM-dd-HH-mm-ss" ) + ".bak", true );
								byte[] rawData = httpClient.GetByteArrayAsync( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/ZoneDictionary.dat" ).Result;
								File.WriteAllBytes( ZoneInfoSettings.ConfigFilePath, rawData );
							}
							catch( Exception exception )
							{
								MessageBox.Show( "Zone info update failed: " + exception.ToString(), "Failure!" );
							}
						}

						//	Mark that we checked for configuration updates.
						ProgramSettings.LastConfigUpdateCheck = DateTimeOffset.UtcNow;
					}
				}
			}

			//	Close the splash screen now that we're done.
			waitForm.Close();
		}

		public ProgramConfig ProgramSettings { get; protected set; }
		public GameDataConfig GameDataSettings { get; protected set; }
		public ZoneInfo ZoneInfoSettings { get; protected set; }
		public CharacterAliasConfig CharacterAliasSettings { get; protected set; }
		public string ConfigFolderPath { get; protected set; }
	}
}
