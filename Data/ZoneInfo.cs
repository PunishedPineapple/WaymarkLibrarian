using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class ZoneInfo
	{
		//	TODO: Construct with file info from zone dictionary file.
		public ZoneInfo()
		{
			//todo: populate mIDToNameDict

			//	Populate reverse dictionary.
			mNameToIDDict = mIDToNameDict.ToDictionary( (i) => i.Value, (i) => i.Key );
		}

		//	Safe zone ID to zone name conversion.
		public bool GetZoneName( UInt16 zoneID, out string rZoneName )
		{
			if( mIDToNameDict.TryGetValue( zoneID, out rZoneName ) )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//	Unsafe zone ID to zone name conversion.
		public string GetZoneName( UInt16 zoneID )
		{
			string zoneName;
			if( !GetZoneName( zoneID, out zoneName ) ) zoneName = "Unknown";
			return zoneName;
		}

		//	Safe zone name to Zone ID conversion.
		public bool GetZoneID( string zoneName, out UInt16 rZoneID )
		{
			if( mNameToIDDict.TryGetValue( zoneName, out rZoneID ) )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//	Unsafe zone name to Zone ID conversion.
		public UInt16 GetZoneName( string zoneName )
		{
			UInt16 zoneID;
			if( !GetZoneID( zoneName, out zoneID ) ) zoneID = 0;
			return zoneID;
		}

		//	Members
		protected Dictionary<UInt16, string> mIDToNameDict = new Dictionary<UInt16, string>();
		protected Dictionary<string, UInt16> mNameToIDDict = new Dictionary<string, UInt16>();
	}
}
