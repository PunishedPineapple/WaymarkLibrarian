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
		}

		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					if( line.Split( '=' ).First().Trim().Equals( "CharacterDataFolderPath" ) )	CharacterDataFolderPath = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "DefaultCharacterID" ) )		DefaultCharacterID = line.Split( '=' ).Last().Trim();

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

				File.WriteAllText( ConfigFilePath, cfgString );
			}
		}

		public string CharacterDataFolderPath { get; set; }
		public string DefaultCharacterID { get; set; }
		protected string ConfigFilePath { get; set; }
	}
}
