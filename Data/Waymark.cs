using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class Waymark
	{
		//	Types
		public class Point3D
		{
			public Point3D()
			{
				X = 0.0;
				Y = 0.0;
				Z = 0.0;
			}
			public Point3D( Point3D objToCopy )
			{
				X = objToCopy.X;
				Y = objToCopy.Y;
				Z = objToCopy.Z;
			}
			public Point3D( double x, double y, double z )
			{
				X = x;
				Y = y;
				Z = z;
			}
			public double X { get; set; }
			public double Y { get; set; }
			public double Z { get; set; }
		}

		//	Construction
		public Waymark( char id )
		{
			IsEnabled = false;
			Pos = new Point3D();
			ID = id;
		}

		public Waymark( Waymark oldObj )
		{
			IsEnabled = oldObj.IsEnabled;
			Pos = new Point3D( oldObj.Pos );
			ID = oldObj.ID;
		}
		public string GetWaymarkDataString()
		{
			return IsEnabled ? ( Pos.X.ToString( " 000.00;-000.00" ) + ", " + Pos.Y.ToString( " 000.00;-000.00" ) + ", " + Pos.Z.ToString( " 000.00;-000.00" ) ) : "Unused";
		}

		//	Members
		public bool IsEnabled { get; set; }
		public Point3D Pos { get; set; }
		public char ID { get; set; }
	}
}