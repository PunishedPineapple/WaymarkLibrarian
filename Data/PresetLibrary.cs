using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class PresetLibrary
	{
		public PresetLibrary()
		{
				Presets = new List<WaymarkPreset>();
		}
		public bool AddPreset( WaymarkPreset preset )
		{
			Presets.Add( preset );
			Presets.Sort( PresetSortCompare );
			return true;
			//	*****TODO: Check for identical presets and don't add if one already exists.  Return true if added, false if not added due to being duplicate.*****
			//return false;
		}
		protected int PresetSortCompare( WaymarkPreset a, WaymarkPreset b )
		{
			if( a == null || b == null ) return 0;
			else return a.ZoneID.CompareTo( b.ZoneID );
		}
		public List<WaymarkPreset> Presets { get; protected set; }
	}
}