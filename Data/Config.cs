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
			//Get the path to the settings folder.
			ConfigFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + "\\WaymarkLibrarian\\";

			//	Instantiate members.
			ProgramSettings = new ProgramConfig( ConfigFolderPath + "Options.cfg" );
			GameDataSettings = new GameDataConfig();
			CharacterAliasSettings = new CharacterAliasConfig( ConfigFolderPath + "CharacterAliases.cfg" );
		}

		public void SaveConfig()
		{
			ProgramSettings.SaveConfig();
			CharacterAliasSettings.SaveConfig();
			//	*****TODO: Copy old files for backup first.*****
		}

		public ProgramConfig ProgramSettings{ get; protected set; }
		public GameDataConfig GameDataSettings{ get; protected set; }
		public CharacterAliasConfig CharacterAliasSettings { get; protected set; }
		protected string ConfigFolderPath { get; set; }
	}
}
