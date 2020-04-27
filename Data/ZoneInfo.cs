using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaymarkLibrarian
{
	class ZoneInfo
	{
		public ZoneInfo( string configFilePath )
		{
			ConfigFilePath = configFilePath;
			SetDefaultConfig();
			Reload();
		}

		public void Reload()
		{
			ReadSavedConfig();
		}

		protected void SetDefaultConfig()
		{
			GameVersion = "UNKNOWN";
		}

		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				if( lines.Count > 0 && lines[0].Split( '=' ).First().Trim() == "GameVersion" )
				{
					GameVersion = lines[0].Split( '=' ).Last().Trim();
					lines.RemoveAt( 0 );
				}
				mIDToNameDict.Clear();
				foreach( string line in lines )
				{
					mIDToNameDict.Add( UInt16.Parse( line.Split( '=' ).First().Trim() ),  line.Split( '=' ).Last().Trim() );
				}
			}
		}

		public string GetZoneName( UInt16 zoneID )
		{
			string zoneName;
			if( !mIDToNameDict.TryGetValue( zoneID, out zoneName ) ) zoneName = "Unknown Zone (" + zoneID.ToString() + ")";
			return zoneName;
		}

		public bool ZoneDataExists( UInt16 zoneID )
		{
			return mIDToNameDict.ContainsKey(zoneID);
		}

		public bool ZoneDataIndexExists( int index )
		{
			return index >= 0 && mIDToNameDict.Count > index;
		}

		public int GetIndex( UInt16 zoneID )
		{
			return mIDToNameDict.Keys.ToList().IndexOf( zoneID );
		}

		public UInt16 GetKeyFromIndex( int index )
		{
			return mIDToNameDict.Keys.ToList()[index];
		}

		public string GetValueFromIndex( int index )
		{
			return mIDToNameDict[mIDToNameDict.Keys.ToList()[index]];
		}

		//	Members
		public string ConfigFilePath { get; protected set; }
		public string GameVersion { get; protected set; }

		//	*****TODO: Dictionary is a most likely poor choice for the underlying data structure since we want to be able to access by index as well.  Probably keep two lists and manage them ourselves.*****
		protected Dictionary<UInt16, string> mIDToNameDict = new Dictionary<UInt16, string>();
	}
}
