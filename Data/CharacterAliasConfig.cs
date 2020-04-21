using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaymarkLibrarian
{
	class CharacterAliasConfig
	{
		public CharacterAliasConfig( string configFilePath )
		{
			mAliases = new Dictionary<string, string>();
			ConfigFilePath = configFilePath;
			SetDefaultConfig();
			ReadSavedConfig();
		}
		protected void SetDefaultConfig()
		{
		}
		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					mAliases.Add( line.Split( '=' ).First().Trim(), line.Split( '=' ).Last().Trim() );
				}
			}
		}

		public string GetAlias( string characterID )
		{
			string alias = "";
			if( mAliases.TryGetValue( characterID, out alias ) )	return alias;
			else													return characterID;
		}

		protected Dictionary<string, string> mAliases;
		protected string ConfigFilePath { get; set; }
	}
}
