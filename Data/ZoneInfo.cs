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
			ReadSavedConfig();
		}

		protected void SetDefaultConfig()
		{
			//	No default config for the zone dictionary.
		}

		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
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
		protected string ConfigFilePath { get; set; }

		//	*****TODO: Dictionary is a most likely poor choice for the underlying data structure since we want to be able to access by index as well.  Probably keep two lists and manage them ourselves.*****
		protected Dictionary<UInt16, string> mIDToNameDict = new Dictionary<UInt16, string>();
	}
}
