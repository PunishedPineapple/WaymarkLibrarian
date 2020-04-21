using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class WaymarkPreset
	{
		public WaymarkPreset( uint numWaymarks )
		{
			Name			= "Unknown";
			ZoneID			= 0;
			LastModified	= DateTime.UtcNow;
			Waymarks		= new Waymark[(int)numWaymarks];
			for( uint i = 0u; i < Waymarks.Length; ++i )
			{
				Waymarks[i] = new Waymark();
			}
		}

		public Waymark this[uint key]
		{
			get { return Waymarks[(int)key]; }
		}

		public string GetPresetDataString()
		{
			string str = "";
			for( uint i = 0u; i < Waymarks.Length; ++i )
			{
				str += GetWaymarkName( i ) + ": " + Waymarks[i].GetWaymarkDataString() + "\r\n";
			}
			str += "\r\nZone ID: " + ZoneID.ToString() + "\r\nLast Modified: " + LastModified.ToLocalTime().ToString() + "\r\n";
			return str;
		}

		public string GetWaymarkName( uint key )
		{
			string[] names = { "A", "B", "C", "D", "1", "2", "3", "4" };
			if( key >= names.Length ) return "Error: Invalid waymark number.";
			return names[(int)key];
		}

		//	Members
		public string Name { get; set; }
		public DateTime LastModified { get; set; }
		public UInt16 ZoneID { get; set; }
		public Waymark[] Waymarks { get; protected set; }
	}
}