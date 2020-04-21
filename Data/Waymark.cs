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
			public double X { get; set; }
			public double Y { get; set; }
			public double Z { get; set; }
		}

		//	Construction
		public Waymark()
		{
			IsEnabled = false;
			Pos = new Point3D();
		}

		public string GetWaymarkDataString()
		{
			return IsEnabled ? ( Pos.X.ToString( " 000.00;-000.00" ) + ", " + Pos.Y.ToString( " 000.00;-000.00" ) + ", " + Pos.Z.ToString( " 000.00;-000.00" ) ) : "Unused";
		}

		//	Members
		public bool IsEnabled { get; set; }
		public Point3D Pos { get; set; }
	}
}