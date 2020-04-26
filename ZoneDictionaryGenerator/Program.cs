using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ZoneDictionaryGenerator
{
	class Program
	{
		static void Main( string[] args )
		{
			//	Get the file path to read and validate it.
			string inFile = "";
			if( args.Length == 1 )
			{
				inFile = args[0];
			}
			while( !File.Exists( inFile ) )
			{
				Console.WriteLine( "Invalid File: " + inFile );
				Console.WriteLine( "Please enter the path to the EXD file to process:" );
				inFile = Console.ReadLine();
			}

			//	Read the file in as lines.
			List<string> lines = File.ReadLines( inFile ).ToList();

			//	Get the header row with column names, then strip the three header rows from the list.
			string headerLine = lines[1];
			lines.RemoveAt( 0 );
			lines.RemoveAt( 0 );
			lines.RemoveAt( 0 );

			//	Find the columns that we want (PlaceName, ExclusiveType, and The blank column with the actual Territory ID).  Maybe just make them magic nubmers for now?
			uint zoneIDColNum = 11u;
			uint zoneTypeColNum = 9u;
			uint zoneNameColNum = 6u;

			//	Go through each entry and construct our dictionary.
			uint linesProcessed = 0u;
			uint validZones = 0u;
			uint duplicateZones = 0u;
			Dictionary<UInt16, string> zoneDictionary = new Dictionary<ushort, string>();
			foreach( string line in lines )
			{
				++linesProcessed;
				string[] entries = line.Split( ',' );
				if( byte.Parse( entries[zoneTypeColNum].Trim() ) == 2 )
				{
					++validZones;
					if( !zoneDictionary.TryAdd( UInt16.Parse( entries[zoneIDColNum].Trim() ), entries[zoneNameColNum].Trim() ) )
					{
						++duplicateZones;
					}
				}
			}

			//	*****TODO: Export to file.*****

			//	Tell the user what happened.
			Console.WriteLine( "Processed " + linesProcessed + " Zones with " + validZones + " valid entries (" + duplicateZones + " duplicates)." );
		}
	}
}
