using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
			CharacterDataFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + "\\My Games\\FINAL FANTASY XIV - A Realm Reborn\\";
			DefaultCharacterID = "";
			UpdateRequestTimeout_Sec = 20;
			UpdateCheckFrequency_Days = 1.0;
			LastUpdateCheck = DateTimeOffset.FromUnixTimeSeconds( 0 );
			ShowInitialWarning = true;
		}

		protected void ReadSavedConfig()
		{
			//	Read the config if we have it.
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					if( line.Split( '=' ).First().Trim().Equals( "CharacterDataFolderPath" ) )		CharacterDataFolderPath = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "DefaultCharacterID" ) )			DefaultCharacterID = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "UpdateRequestTimeout_Sec" ) )		UpdateRequestTimeout_Sec = int.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "UpdateCheckFrequency_Days" ) )	UpdateCheckFrequency_Days = double.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "LastUpdateCheck" ) )				LastUpdateCheck = DateTimeOffset.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "ShowInitialWarning" ) )			ShowInitialWarning = bool.Parse( line.Split( '=' ).Last().Trim() );
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
				cfgString += "ShowInitialWarning" + " = " + ShowInitialWarning.ToString() + "\r\n";

				if( File.Exists( ConfigFilePath ) ) File.Copy( ConfigFilePath, ConfigFilePath + ".bak", true );
				File.WriteAllText( ConfigFilePath, cfgString );
			}
		}

		public string CharacterDataFolderPath { get; set; }
		public string DefaultCharacterID { get; set; }
		public DateTimeOffset LastUpdateCheck { get; set; }
		public int UpdateRequestTimeout_Sec { get; protected set; }
		public double UpdateCheckFrequency_Days { get; protected set; }
		public bool ShowInitialWarning { get; set; }
		protected string ConfigFilePath { get; set; }
	}
}
