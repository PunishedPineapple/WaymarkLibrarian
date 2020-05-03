using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WaymarkLibrarian
{
	class VersionInfoHelper : IEquatable<VersionInfoHelper>, IComparable<VersionInfoHelper>
	{
		public VersionInfoHelper( string str )
		{
			string[] nums = str.Split( '.' );
			if( nums.Length > 0 ) Major = int.Parse( nums[0] );
			if( nums.Length > 1 ) Minor = int.Parse( nums[1] );
			if( nums.Length > 2 ) Build = int.Parse( nums[2] );
			if( nums.Length > 3 ) Revision = int.Parse( nums[3] );
		}

		public VersionInfoHelper( FileVersionInfo fv ) : this( fv.FileVersion )
		{
		}

		public static VersionInfoHelper Parse( string str )
		{
			return new VersionInfoHelper( str );
		}

		public static VersionInfoHelper Parse( FileVersionInfo fv )
		{
			return new VersionInfoHelper( fv );
		}

		public static bool operator ==( VersionInfoHelper lhs, VersionInfoHelper rhs )
		{
			return	lhs.Major == rhs.Major &&
					lhs.Minor == rhs.Minor &&
					lhs.Build == rhs.Build &&
					lhs.Revision == rhs.Revision;
		}

		public static bool operator !=( VersionInfoHelper lhs, VersionInfoHelper rhs )
		{
			return !(lhs == rhs);
		}

		public static bool operator <=( VersionInfoHelper lhs, VersionInfoHelper rhs )
		{
			if( lhs.Major > rhs.Major ) return false;
			else if( lhs.Minor > rhs.Minor ) return false;
			else if( lhs.Build > rhs.Build ) return false;
			else if( lhs.Revision > rhs.Revision ) return false;
			else return true;
		}

		public static bool operator >=( VersionInfoHelper lhs, VersionInfoHelper rhs )
		{

			if( lhs.Major < rhs.Major ) return false;
			else if( lhs.Minor < rhs.Minor ) return false;
			else if( lhs.Build < rhs.Build ) return false;
			else if( lhs.Revision < rhs.Revision ) return false;
			else return true;
		}

		public static bool operator <( VersionInfoHelper lhs, VersionInfoHelper rhs )
		{
			return lhs <= rhs && lhs != rhs;
		}

		public static bool operator >( VersionInfoHelper lhs, VersionInfoHelper rhs )
		{
			return lhs >= rhs && lhs != rhs;
		}

		public override bool Equals( Object other )
		{
			return	other.GetType() == GetType() &&
					(VersionInfoHelper)other == this;
		}

		public bool Equals( VersionInfoHelper other )
		{
			return other == this;
		}

		public int CompareTo( VersionInfoHelper other )
		{
			if( other < this ) return 1;
			else if( other > this ) return -1;
			else return 0;
		}

		public override int GetHashCode()
		{
			return ( Major, Minor, Build, Revision ).GetHashCode();
		}

		public int Major { get; protected set; } = 0;
		public int Minor { get; protected set; } = 0;
		public int Build { get; protected set; } = 0;
		public int Revision { get; protected set; } = 0;
	}
}
