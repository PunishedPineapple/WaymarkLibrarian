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

			//	Set default options to start.
			SetDefaultConfig();

			//	Replace them with what's configured if we can.
			//if( File.Exists( ConfigFilePath ) ) ReadSavedConfig();
		}

		protected void ReadSavedConfig()
		{
		//	***** TODO: Game Data and Zone Info probably need to be separate files to make updating easier. *****
			/*if( File.Exists( ConfigFilePath ) )
			{
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load( ConfigFilePath );
				//XmlNamespaceManager nsmgr = new XmlNamespaceManager( xmldoc.NameTable );
				//nsmgr.AddNamespace( string.Empty, string.Empty );
				XmlNode programSettingsNode = xmldoc.DocumentElement.SelectSingleNode( "ProgramSettings" );
				XmlNode characterAliasesNode = xmldoc.DocumentElement.SelectSingleNode( "CharacterAliases" );
			}*/
		}

		protected void SetDefaultConfig()
		{

		}

		public void SaveConfig()
		{
			ProgramSettings.SaveConfig();
			//copy old files for backup first.
		}

		public ProgramConfig ProgramSettings{ get; protected set; }
		public GameDataConfig GameDataSettings{ get; protected set; }
		public CharacterAliasConfig CharacterAliasSettings { get; protected set; }
		protected string ConfigFolderPath { get; set; }
	}
}
