using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class ProgramConfig
	{
		public ProgramConfig()
		{
			SetDefaultConfig();
		}
		protected void SetDefaultConfig()
		{
			CharacterDataFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + "\\My Games\\FINAL FANTASY XIV - A Realm Reborn\\";
			DefaultCharacterID = "";
		}
		public string CharacterDataFolderPath { get; set; }
		public string DefaultCharacterID { get; set; }
	}
}
