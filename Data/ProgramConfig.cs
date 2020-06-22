using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace WaymarkLibrarian
{
	class ProgramConfig
	{
		public ProgramConfig( string configFilePath )
		{
			ConfigFilePath = configFilePath;
			SetDefaultConfig();
			ReadSavedConfig();
		}

		protected void SetDefaultConfig()
		{
			CharacterDataFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + "\\My Games\\FINAL FANTASY XIV - A Realm Reborn";
			DefaultCharacterID = "";
			UpdateRequestTimeout_Sec = 20;
			UpdateCheckFrequency_Days = 1.0;
			LastUpdateCheck = DateTimeOffset.FromUnixTimeSeconds( 0 );
			LastConfigUpdateCheck = DateTimeOffset.FromUnixTimeSeconds( 0 );
			ShowInitialWarning = true;
			LastProgramUpdateVersionSeen = VersionInfoHelper.Parse( "0.9.0.0" );	//	First released version.
			EarliestPresetTimestampAllowed = new DateTimeOffset( 2020, 2, 18, 10, 00, 00, new TimeSpan( 0 ) );	//	Time of servers up for patch 5.2.
		}

		protected void ReadSavedConfig()
		{
			//	Read the config if we have it.
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					if( line.Split( '=' ).First().Trim().Equals( "CharacterDataFolderPath" ) )			CharacterDataFolderPath = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "DefaultCharacterID" ) )				DefaultCharacterID = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "UpdateRequestTimeout_Sec" ) )			UpdateRequestTimeout_Sec = int.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "UpdateCheckFrequency_Days" ) )		UpdateCheckFrequency_Days = double.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "LastUpdateCheck" ) )					LastUpdateCheck = DateTimeOffset.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "LastConfigUpdateCheck" ) )			LastConfigUpdateCheck = DateTimeOffset.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "ShowInitialWarning" ) )				ShowInitialWarning = bool.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "LastProgramUpdateVersionSeen" ) )		LastProgramUpdateVersionSeen = VersionInfoHelper.Parse( line.Split('=').Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "EarliestPresetTimestampAllowed" ) )	EarliestPresetTimestampAllowed = DateTimeOffset.Parse( line.Split( '=' ).Last().Trim() );
				}
			}
		}

		public void SaveConfig()
		{
			if( Directory.Exists( Path.GetDirectoryName( ConfigFilePath ) ) )
			{
				string cfgString = "";
				cfgString += "CharacterDataFolderPath" + " = " + CharacterDataFolderPath + "\r\n";
				cfgString += "DefaultCharacterID" + " = " + DefaultCharacterID + "\r\n";
				cfgString += "UpdateRequestTimeout_Sec" + " = " + UpdateRequestTimeout_Sec.ToString() + "\r\n";
				cfgString += "UpdateCheckFrequency_Days" + " = " + UpdateCheckFrequency_Days.ToString() + "\r\n";
				cfgString += "LastUpdateCheck" + " = " + LastUpdateCheck.ToString( "u" ) + "\r\n";
				cfgString += "LastConfigUpdateCheck" + " = " + LastConfigUpdateCheck.ToString( "u" ) + "\r\n";
				cfgString += "ShowInitialWarning" + " = " + ShowInitialWarning.ToString() + "\r\n";
				cfgString += "LastProgramUpdateVersionSeen" + " = " + LastProgramUpdateVersionSeen.ToString() + "\r\n";
				cfgString += "EarliestPresetTimestampAllowed" + " = " + EarliestPresetTimestampAllowed.ToString( "u" ) + "\r\n";

				if ( File.Exists( ConfigFilePath ) ) File.Copy( ConfigFilePath, ConfigFilePath + ".bak", true );
				File.WriteAllText( ConfigFilePath, cfgString );
			}
		}

		public string CharacterDataFolderPath { get; set; }
		public string DefaultCharacterID { get; set; }
		public DateTimeOffset LastUpdateCheck { get; set; }
		public DateTimeOffset LastConfigUpdateCheck { get; set; }
		public int UpdateRequestTimeout_Sec { get; protected set; }
		public double UpdateCheckFrequency_Days { get; protected set; }
		public bool ShowInitialWarning { get; set; }
		public VersionInfoHelper LastProgramUpdateVersionSeen { get; set; }
		public DateTimeOffset EarliestPresetTimestampAllowed { get; set; }
		protected string ConfigFilePath { get; set; }
	}
}
