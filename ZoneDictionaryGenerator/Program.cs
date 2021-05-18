using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ZoneDictionaryGenerator
{
	class Program
	{
		//	This is a list of ContentFinderCondition IDs that we want to include in the list, regardless of whether they meet the normal conditions for inclusion.
		private static readonly List<UInt16> mKludgeZones = new List<UInt16>(){
			760,	//Delubrum Reginae
			761		//Delubrum Reginae (Savage)
		};

		//	This is a list of names that we want to hardcode for some reason.  The initial use case for this is removed Unreal trials, so that people aren't just left with a blank duty name.
		private static readonly Dictionary<UInt16, string> mHardcodedDutyNames = new Dictionary<ushort, string>()
		{
			{ 744, "The Akh Afah Amphitheatre (Unreal)" },	//	Shiva Unreal
			{ 757, "The Navel (Unreal)" }	//	Titan Unreal
		};

		public static void Main( string[] args )
		{
			//	Get the file paths to read and validate them.
			string contentFinderConditionFilePath;
			string territoryTypeFilePath;
			bool haveValidSheetPaths = false;
			do
			{
				Console.WriteLine( "Please enter the path to the folder containing the ContentFinderCondition and TerritoryType sheets to process:" );
				string sheetFolderPath = Console.ReadLine();
				contentFinderConditionFilePath = Path.Combine( sheetFolderPath, "ContentFinderCondition.csv" );
				territoryTypeFilePath = Path.Combine( sheetFolderPath, "TerritoryType.csv" );

				if( File.Exists( contentFinderConditionFilePath ) && File.Exists( territoryTypeFilePath ) )
				{
					haveValidSheetPaths = true;
				}
				else
				{
					Console.WriteLine( "Specified path does not contain the required sheets." );
				}
			} while( !haveValidSheetPaths );

			//	Ask for the game version too.
			Console.WriteLine( "Specify the game's version corresponding to this territory info:" );
			string gameVersion = Console.ReadLine();

			//	Read the files in as lines.
			List<string> territoryTypeLines = File.ReadLines( territoryTypeFilePath ).ToList();
			List<string> contentFinderConditionLines = File.ReadLines( contentFinderConditionFilePath ).ToList();

			//	Strip the three header rows from the lists.
			for( int i = 0; i < 3; ++i )
			{
				territoryTypeLines.RemoveAt( 0 );
				contentFinderConditionLines.RemoveAt( 0 );
			}

			//	Find the columns that we want.  Just use magic numbers for now.
			uint zoneIDColNum = 11u;
			uint zoneTypeColNum = 9u;

			uint contentFinderConditionIDColNum = 0u;
			uint contentFinderConditionNameColNum = 38u;


			//	Construct ContentFinderCondition dictionary.
			SortedDictionary<UInt16, string> contentFinderConditionDictionary = new SortedDictionary<UInt16, string>();
			foreach( string line in contentFinderConditionLines )
			{
				string[] entries = line.Split( ',' );
				if( entries.Length > 1 )
				{
					UInt16 contentFinderConditionID;
					if( UInt16.TryParse( entries[contentFinderConditionIDColNum].Trim(), out contentFinderConditionID ) )
					{
						string dutyName = entries[contentFinderConditionNameColNum].Trim().Trim( '\"' );
						contentFinderConditionDictionary.TryAdd( contentFinderConditionID, dutyName );
					}
				}
			}

			//	Construct final dictionary.
			uint linesProcessed = 0u;
			uint validZones = 0u;
			uint duplicateZones = 0u;
			SortedDictionary<UInt16, string> zoneDictionary = new SortedDictionary<UInt16, string>();
			foreach( string line in territoryTypeLines )
			{
				++linesProcessed;
				string[] entries = line.Split( ',' );
				UInt16 contentFinderConditionID = UInt16.Parse( entries[zoneIDColNum].Trim() );
				string dutyName = contentFinderConditionDictionary[contentFinderConditionID].Trim().Trim( '\"' );
				if( byte.Parse( entries[zoneTypeColNum].Trim() ) == 2 ||	//	Most zones that can save/load waymark presets are ExclusiveType 2
					mKludgeZones.Contains( contentFinderConditionID ) )		//	Use a list of specific ones that are not.  See commentary in the XL plugin zone dictionary initialization for detailed information.
				{
					++validZones;

					//	Capitalize the first letter to make things look nice.
					if( dutyName.Length > 0 ) dutyName = dutyName[0].ToString().ToUpper() + dutyName.Substring( 1 );

					//	Use the hardcoded name if applicable.  See the comment on that dictionary for more information.
					if( mHardcodedDutyNames.ContainsKey( contentFinderConditionID ) ) dutyName = mHardcodedDutyNames[contentFinderConditionID];

					//	Add the entry.
					if( !zoneDictionary.TryAdd( contentFinderConditionID, dutyName ) ) ++duplicateZones;
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
