using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class WaymarkPreset : IEnumerable
	{
		public WaymarkPreset()
		{
			if( mWaymarkIDs == null || mWaymarkIDs.Count < 1 ) throw new Exception( "Error constructing WaymarkPreset object: The waymark ID list is either null or empty." );

			Name			= "Unknown";
			ZoneID			= 0;
			LastModified	= DateTimeOffset.UtcNow;
			Waymarks		= new Waymark[mWaymarkIDs.Count];
			for( uint i = 0u; i < Waymarks.Length; ++i )
			{
				Waymarks[i] = new Waymark( mWaymarkIDs[(int)i] );
			}
		}

		public WaymarkPreset( WaymarkPreset objToCopy )
		{
			Name = objToCopy.Name;
			ZoneID = objToCopy.ZoneID;
			LastModified = objToCopy.LastModified;
			Waymarks = new Waymark[objToCopy.Waymarks.Length];
			for( uint i = 0u; i < Waymarks.Length; ++i )
			{
				Waymarks[i] = new Waymark( objToCopy.Waymarks[i] );
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
				str += GetWaymarkID( i ) + ": " + Waymarks[i].GetWaymarkDataString() + "\r\n";
			}
			str += "\r\nZone ID: " + ZoneID.ToString() + "\r\nLast Modified: " + LastModified.ToLocalTime().ToString( "G" );
			return str;
		}

		public static void SetWaymarkIDOrder( string IDs )
		{
			mWaymarkIDs = new List<char>();
			foreach( char c in IDs )
			{
				mWaymarkIDs.Add( c );
			}
		}

		public static char GetWaymarkID( uint key )
		{
			if( key >= mWaymarkIDs.Count ) throw new Exception( "Error in GetWaymarkID: Invalid waymark index passed." );
			return mWaymarkIDs.ElementAt((int)key);
		}

		public static int GetWaymarkNumber( char key )
		{
			return mWaymarkIDs.IndexOf( key );
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new WaymarkEnumerator( Waymarks );
		}

		//	Members
		public string Name { get; set; }
		public DateTimeOffset LastModified { get; set; }
		public UInt16 ZoneID { get; set; }
		public Waymark[] Waymarks { get; protected set; }
		protected static List<char> mWaymarkIDs;
	}

	//	Enumerator class to support IEnumerable.
	class WaymarkEnumerator : IEnumerator
	{
		public WaymarkEnumerator( Waymark[] waymarks )
		{
			mWaymarks = waymarks;
		}

		public bool MoveNext()
		{
			++pos;
			return pos < mWaymarks.Length;
		}

		public void Reset()
		{
			pos = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return mWaymarks[pos];
			}
		}

		protected int pos = -1;
		protected Waymark[] mWaymarks;
	}
}