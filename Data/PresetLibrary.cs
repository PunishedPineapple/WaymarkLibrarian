using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace WaymarkLibrarian
{
	class PresetLibrary
	{
		public PresetLibrary( string configFilePath )
		{
			ConfigFilePath = configFilePath;
			Presets = new List<WaymarkPreset>();
			SetDefaultConfig();
			ReadSavedConfig();
		}
		public bool AddPreset( WaymarkPreset preset )
		{
			Presets.Add( preset );
			Presets.Sort( PresetSortCompare );
			return true;
			//	*****TODO: Probably check for identical presets and don't add if one already exists.  Return true if added, false if not added due to being duplicate.*****
			//return false;
		}

		protected void SetDefaultConfig()
		{
			//	Not really any defaults for the library.
		}

		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load( ConfigFilePath );
				XmlNode LibraryNode = xmldoc.SelectSingleNode( "Library" );
				XmlNodeList presetNodes = LibraryNode.SelectNodes( "Preset" );
				foreach( XmlNode presetNode in presetNodes )
				{
					//	Create preset object, then assign Name, Territory, etc.
					Presets.Add( new WaymarkPreset() );
					XmlNodeList waymarkNodes = presetNode.SelectNodes( "Waymark" );
					Presets.Last().Name = presetNode.Attributes.GetNamedItem( "Name" ).Value;
					Presets.Last().ZoneID = UInt16.Parse( presetNode.Attributes.GetNamedItem( "TerritoryID" ).Value );
					Presets.Last().LastModified = DateTimeOffset.Parse( presetNode.Attributes.GetNamedItem( "Time" ).Value );

					//	Assign the data for each waymark in the preset.
					foreach( XmlNode waymarkNode in waymarkNodes )
					{
						int waymarkNumber = WaymarkPreset.GetWaymarkNumber( waymarkNode.Attributes.GetNamedItem( "ID" ).Value );
						if( waymarkNumber < 0 ) throw new Exception( "Error: Invalid waymark ID while reading waymark library." );
						Presets.Last().Waymarks[waymarkNumber].IsEnabled = bool.Parse( waymarkNode.Attributes.GetNamedItem( "Active" ).Value );
						XmlNode coordsNode = waymarkNode.SelectSingleNode( "Coordinates" );
						Presets.Last().Waymarks[waymarkNumber].Pos.X = Double.Parse( coordsNode.Attributes.GetNamedItem( "X" ).Value );
						Presets.Last().Waymarks[waymarkNumber].Pos.X = Double.Parse( coordsNode.Attributes.GetNamedItem( "Y" ).Value );
						Presets.Last().Waymarks[waymarkNumber].Pos.X = Double.Parse( coordsNode.Attributes.GetNamedItem( "Z" ).Value );
					}
				}
			}
		}

		protected int PresetSortCompare( WaymarkPreset a, WaymarkPreset b )
		{
			if( a == null || b == null ) return 0;
			else return a.ZoneID.CompareTo( b.ZoneID );
		}

		public List<WaymarkPreset> Presets { get; protected set; }

		protected string ConfigFilePath { get; set; }
	}
}