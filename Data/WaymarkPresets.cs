using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class WaymarkPresets
	{
		public WaymarkPresets( uint numPresets )
		{
			Presets = new WaymarkPreset[numPresets];
			for( uint i = 0u; i < Presets.Length; ++i )
			{
				Presets[i] = new WaymarkPreset();
			}
		}

		public WaymarkPresets( WaymarkPresets objToCopy )
		{
			Presets = new WaymarkPreset[objToCopy.Presets.Length];
			for( uint i = 0u; i < Presets.Length; ++i )
			{
				Presets[i] = new WaymarkPreset( objToCopy.Presets[i] );
			}
		}

		public WaymarkPreset this[uint key]
		{
			get { return Presets[(int)key]; }
		}

		public void ReplacePreset( uint index, WaymarkPreset preset )
		{
			if( index < Presets.Length ) Presets[index] = preset;
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
