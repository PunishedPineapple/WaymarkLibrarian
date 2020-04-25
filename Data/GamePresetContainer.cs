using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class GamePresetContainer : IEnumerable
	{
		public GamePresetContainer( uint numPresets )
		{
			Presets = new WaymarkPreset[numPresets];
			for( uint i = 0u; i < Presets.Length; ++i )
			{
				Presets[i] = new WaymarkPreset();
			}
		}

		public GamePresetContainer( GamePresetContainer objToCopy )
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

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new GamePresetsEnumerator( Presets );
		}

		//	Members
		public WaymarkPreset[] Presets { get; protected set; }
	}

	//	Enumerator class to support IEnumerable.
	class GamePresetsEnumerator : IEnumerator
	{
		public GamePresetsEnumerator( WaymarkPreset[] presets )
		{
			mPresets = presets;
		}

		public bool MoveNext()
		{
			++pos;
			return pos < mPresets.Length;
		}

		public void Reset()
		{
			pos = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return mPresets[pos];
			}
		}

		protected int pos = -1;
		protected WaymarkPreset[] mPresets;
	}
}
