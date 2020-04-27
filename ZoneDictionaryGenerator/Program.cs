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
			Console.WriteLine( "Please enter the path to the territory info file to process:" );
			string inFile = Console.ReadLine();
			while( !File.Exists( inFile ) )
			{
				Console.WriteLine( "Invalid File: \"" + inFile + "\".  Please enter the path to the territory info file to process:" );
				inFile = Console.ReadLine();
			}

			//	Ask for the game version too.
			Console.WriteLine( "Specify the game's version corresponding to this territory info:" );
			string gameVersion = Console.ReadLine();

			//	Read the file in as lines.
			List<string> lines = File.ReadLines( inFile ).ToList();

			//	Get the header row with column names, then strip the three header rows from the list.
			string headerLine = lines[1];
			lines.RemoveAt( 0 );
			lines.RemoveAt( 0 );
			lines.RemoveAt( 0 );

			//	Find the columns that we want (PlaceName, ExclusiveType, and The blank column with the actual Territory ID).  Just use magic numbers for now.
			uint zoneIDColNum = 11u;
			uint zoneTypeColNum = 9u;
			uint zoneNameColNum = 6u;

			//	Go through each entry and construct our dictionary.
			uint linesProcessed = 0u;
			uint validZones = 0u;
			uint duplicateZones = 0u;
			SortedDictionary<UInt16, string> zoneDictionary = new SortedDictionary<ushort, string>();
			foreach( string line in lines )
			{
				++linesProcessed;
				string[] entries = line.Split( ',' );
				if( byte.Parse( entries[zoneTypeColNum].Trim() ) == 2 )
				{
					++validZones;
					if( !zoneDictionary.TryAdd( UInt16.Parse( entries[zoneIDColNum].Trim() ), entries[zoneNameColNum].Trim().Trim( '\"' ) ) )
					{
						++duplicateZones;
					}
				}
			}

			//	Zone ID 0 probably isn't valid for any waymarks, so remove that entry (it's likely been overwritten with duplicates anyway).
			zoneDictionary.Remove( 0 );

			//	Write the dictionary out to file.  Put the version at the top.
			string outString = "GameVersion = " + gameVersion + "\r\n";
			foreach( KeyValuePair<UInt16, string> entry in zoneDictionary )
			{
				outString += entry.Key + " = " + entry.Value + "\r\n";
			}
			File.WriteAllText( "ZoneDictionary.dat", outString );

			//	Tell the user what happened.
			Console.WriteLine( "Processed " + linesProcessed + " Zones with " + validZones + " valid entries (" + duplicateZones + " duplicates)." );
		}
	}
}
