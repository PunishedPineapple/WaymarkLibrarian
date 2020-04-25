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

		public void SaveConfig()
		{
			if( Directory.Exists( Path.GetDirectoryName( ConfigFilePath ) ) )
			{
				string cfgString = "";
				foreach( KeyValuePair<string, string> entry in mAliases )
				{
					cfgString += entry.Key + " = " + entry.Value + "\r\n";
				}
				File.WriteAllText( ConfigFilePath, cfgString );
			}
		}

		public string GetAlias( string characterID )
		{
			string alias = "";
			if( mAliases.TryGetValue( characterID, out alias ) )	return alias;
			else													return characterID;
		}

		public void SetAlias( string characterID, string alias )
		{
			if( mAliases.ContainsKey( characterID ) )
			{
				mAliases[characterID] = alias;
			}
			else
			{
				mAliases.Add( characterID, alias );
			}
		}

		protected Dictionary<string, string> mAliases;
		protected string ConfigFilePath { get; set; }
	}
}
