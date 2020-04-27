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
			SortPresets();
			return true;
			//	*****TODO: Probably check for identical presets and don't add if one already exists.  Return true if added, false if not added due to being duplicate.*****
			//return false;
		}
		public void RemovePreset( int index )
		{
			Presets.RemoveAt( index );
			SortPresets();
		}

		public void SortPresets()
		{
			Presets.Sort( PresetSortCompare );
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
				XmlNode libraryNode = xmldoc.SelectSingleNode( "Library" );
				XmlNode presetsNode = libraryNode.SelectSingleNode( "Presets" );
				XmlNodeList presetNodes = presetsNode.SelectNodes( "Preset" );
				foreach( XmlNode presetNode in presetNodes )
				{
					//	Create preset object, then assign Name, Territory, etc.
					Presets.Add( new WaymarkPreset() );
					Presets.Last().Name = presetNode.Attributes.GetNamedItem( "Name" ).Value;
					Presets.Last().ZoneID = UInt16.Parse( presetNode.Attributes.GetNamedItem( "ZoneID" ).Value );
					Presets.Last().LastModified = DateTimeOffset.Parse( presetNode.Attributes.GetNamedItem( "Time" ).Value );
					XmlNodeList waymarkNodes = presetNode.SelectNodes( "Waymarks/Waymark" );

					//	Assign the data for each waymark in the preset.
					foreach( XmlNode waymarkNode in waymarkNodes )
					{
						int waymarkNumber = WaymarkPreset.GetWaymarkNumber( waymarkNode.Attributes.GetNamedItem( "ID" ).Value.First() );
						if( waymarkNumber < 0 ) throw new Exception( "Error: Invalid waymark ID while reading waymark library." );
						Presets.Last().Waymarks[waymarkNumber].IsEnabled = bool.Parse( waymarkNode.Attributes.GetNamedItem( "Active" ).Value );
						XmlNode coordsNode = waymarkNode.SelectSingleNode( "Coordinates" );
						Presets.Last().Waymarks[waymarkNumber].Pos.X = Double.Parse( coordsNode.Attributes.GetNamedItem( "X" ).Value );
						Presets.Last().Waymarks[waymarkNumber].Pos.Y = Double.Parse( coordsNode.Attributes.GetNamedItem( "Y" ).Value );
						Presets.Last().Waymarks[waymarkNumber].Pos.Z = Double.Parse( coordsNode.Attributes.GetNamedItem( "Z" ).Value );
					}
				}
			}
		}

		public void SaveConfig()
		{
			//	Create the directories that hold the config file if they don't exist.
			Directory.CreateDirectory( Path.GetDirectoryName( ConfigFilePath ) );

			//	Construct the XML from our data, and save it to disk.
			XmlDocument xmldoc = new XmlDocument();
			XmlNode libraryNode = xmldoc.CreateElement( "Library" );
			XmlNode presetsNode = xmldoc.CreateElement( "Presets" );
				
			foreach( WaymarkPreset preset in Presets )
			{
				XmlNode presetNode = xmldoc.CreateElement( "Preset" );
				XmlAttribute attr = xmldoc.CreateAttribute( "Name" );
				attr.Value = preset.Name;
				presetNode.Attributes.Append( attr );
				attr = xmldoc.CreateAttribute( "ZoneID" );
				attr.Value = preset.ZoneID.ToString();
				presetNode.Attributes.Append( attr );
				attr = xmldoc.CreateAttribute( "Time" );
				attr.Value = preset.LastModified.ToString( "u" );
				presetNode.Attributes.Append( attr );
				XmlNode waymarksNode = xmldoc.CreateElement( "Waymarks" );
					
				foreach( Waymark waymark in preset )
				{
					XmlNode waymarkNode = xmldoc.CreateElement( "Waymark" );
					attr = xmldoc.CreateAttribute( "ID" );
					attr.Value = waymark.ID.ToString();
					waymarkNode.Attributes.Append( attr );
					attr = xmldoc.CreateAttribute( "Active" );
					attr.Value = waymark.IsEnabled.ToString();
					waymarkNode.Attributes.Append( attr );

					XmlNode coordsNode = xmldoc.CreateElement( "Coordinates" );
					attr = xmldoc.CreateAttribute( "X" );
					attr.Value = waymark.Pos.X.ToString();
					coordsNode.Attributes.Append( attr );
					attr = xmldoc.CreateAttribute( "Y" );
					attr.Value = waymark.Pos.Y.ToString();
					coordsNode.Attributes.Append( attr );
					attr = xmldoc.CreateAttribute( "Z" );
					attr.Value = waymark.Pos.Z.ToString();
					coordsNode.Attributes.Append( attr );

					waymarkNode.AppendChild( coordsNode );
					waymarksNode.AppendChild( waymarkNode );
				}

				presetNode.AppendChild( waymarksNode );
				presetsNode.AppendChild( presetNode );
			}

			libraryNode.AppendChild( presetsNode );
			xmldoc.AppendChild( libraryNode );
			if( File.Exists( ConfigFilePath ) ) File.Copy( ConfigFilePath, ConfigFilePath + ".bak", true );
			xmldoc.Save( ConfigFilePath );
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