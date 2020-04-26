using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace WaymarkLibrarian
{
	class Config
	{
		public Config()
		{
			//	Get the path to the settings folder.
			ConfigFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + "\\PunishedPineapple\\WaymarkLibrarian\\";

			//	Create our children to handle the specific settings.
			ProgramSettings = new ProgramConfig( ConfigFolderPath + "Options.cfg" );
			GameDataSettings = new GameDataConfig( ConfigFolderPath + "GameData.cfg" );
			ZoneInfoSettings = new ZoneInfo( ConfigFolderPath + "ZoneDictionary.dat" );
			
			//	The alias file lives one directory up since we may want to share it with other programs in the future.
			CharacterAliasSettings = new CharacterAliasConfig( Directory.GetParent( ConfigFolderPath ).Parent.FullName + "\\CharacterAliases.cfg" );
		}

		public void SaveConfig()
		{
			//	Create the directories that hold the config files if they don't exist.
			Directory.CreateDirectory( ConfigFolderPath );

			//	*****TODO: Copy old files for backup first.*****

			//	Ask our children to save themselves.  The game data config should never be written, only read.  The same applies to the zone dictionary.
			ProgramSettings.SaveConfig();
			CharacterAliasSettings.SaveConfig();
		}

		public ProgramConfig ProgramSettings { get; protected set; }
		public GameDataConfig GameDataSettings { get; protected set; }
		public ZoneInfo ZoneInfoSettings { get; protected set; }
		public CharacterAliasConfig CharacterAliasSettings { get; protected set; }
		public string ConfigFolderPath { get; protected set; }
	}
}
