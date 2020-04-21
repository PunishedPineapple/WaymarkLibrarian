using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class WaymarkPresets
	{
		public WaymarkPresets( uint numPresets, uint waymarksPerPreset )
		{
			Presets = new WaymarkPreset[numPresets];
			for( uint i = 0u; i < Presets.Length; ++i )
			{
				Presets[i] = new WaymarkPreset( waymarksPerPreset );
			}
		}

		public WaymarkPreset this[uint key]
		{
			get { return Presets[(int)key]; }
		}

		public string GetDataString()
		{
			string str = "";
			for( uint i = 0u; i < Presets.Length; ++i )
			{
				str += "Slot " + (i+1).ToString() + ":\r\n" + Presets[i].GetPresetDataString() + "\r\n";
			}
			return str;
		}

		//	Members
		public WaymarkPreset[] Presets { get; protected set; }
	}
}
