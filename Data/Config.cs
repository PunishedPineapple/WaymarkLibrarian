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

			//	Set the last updated time to now.  Do this regardless of whether we succeeded, as there's no point in spamming the user with update prompts if we can't get the current versions for some reason.
			ProgramSettings.LastUpdateCheck = DateTimeOffset.UtcNow;

			using( HttpClient httpClient = new HttpClient() )
			{
				//	Set the request timeout.  The default is unreasonable for how little data we are dealing with (under 10 KiB total).
				httpClient.Timeout = TimeSpan.FromSeconds( ProgramSettings.UpdateRequestTimeout_Sec );

				//	Empty version strings until we can fill them.
				string programVer = "";
				string gameDataCfgVer = "";
				string zoneDictionaryDatVer = "";

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
				}
				catch( HttpRequestException exception )
				{
					MessageBox.Show( "Unable to retrieve current version information: " + exception.ToString() + "\r\n\r\nThis is likely due to lack of internet access or DNS lookup failure.", "Failure!" );
				}

				//	Notify the user if a new version of the program is available.
				if( programVer.Length > 0 &&
					VersionInfoHelper.Parse( programVer ) > VersionInfoHelper.Parse( FileVersionInfo.GetVersionInfo( System.Reflection.Assembly.GetExecutingAssembly().Location ) ) )
				{
					MessageBox.Show( "A new version of this program is available.  Go to https://github.com/PunishedPineapple/WaymarkLibrarian/releases to download the latest version.", "New Version" );
				}
				//	Only check for configuration updates if the program is up to date (since file formats could conceivably change).
				else
				{
					//	Update game data config.
					if(	gameDataCfgVer.Length > 0 &&
						gameDataCfgVer != GameDataSettings.GameVersion &&
					MessageBox.Show( "A new version of the game data configuration file was found.  Update now?", "Update?", MessageBoxButtons.YesNo ) == DialogResult.Yes )
					{
						try
						{
							byte[] rawData = httpClient.GetByteArrayAsync( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/GameData.cfg" ).Result;
							File.WriteAllBytes( GameDataSettings.ConfigFilePath, rawData );
						}
						catch( Exception exception )
						{
							MessageBox.Show( "Offsets update failed: " + exception.ToString(), "Failure!" );
						}
					}

					//	Update zone dictionary.
					if(	zoneDictionaryDatVer.Length > 0 &&
						zoneDictionaryDatVer != ZoneInfoSettings.GameVersion &&
						MessageBox.Show( "A new version of the zone dictionary file was found.  Update now?", "Update?", MessageBoxButtons.YesNo ) == DialogResult.Yes )
					{
						try
						{
							byte[] rawData = httpClient.GetByteArrayAsync( "https://punishedpineapple.github.io/WaymarkLibrarian/Support/ZoneDictionary.dat" ).Result;
							File.WriteAllBytes( ZoneInfoSettings.ConfigFilePath, rawData );
						}
						catch( Exception exception )
						{
							MessageBox.Show( "Zone info update failed: " + exception.ToString(), "Failure!" );
						}
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
