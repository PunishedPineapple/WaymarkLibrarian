using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaymarkLibrarian
{
	class GameDataConfig
	{
		//	Construction
		public GameDataConfig( string configFilePath )
		{
			ConfigFilePath = configFilePath;
			SetDefaultConfig();
			Reload();
		}

		//	Member Functions
		public uint BytesPerPreset() { return NumberOfWaymarks() * BytesPerWaymark + ConfigBytesPerPreset; }

		public void Reload()
		{
			ReadSavedConfig();
			WaymarkPreset.SetWaymarkIDOrder( WaymarkIDOrder );
		}

		protected void SetDefaultConfig()
		{
			GameVersion = "2020.03.27.0000.0000";
			WaymarkDataFileName = "UISAVE.DAT";
			WaymarkDataSectionIndex = 0x11;
			NumWaymarkDataLeadingBytes = 16u;
			NumberOfPresets = 5u;
			WaymarkIDOrder = "ABCD1234";
			BytesPerWaymark = 12u;
			ConfigBytesPerPreset = 8u;
			ConfigFileMagicNumber = 0x31;
		}

		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					if( line.Split( '=' ).First().Trim().Equals( "GameVersion" ) ) GameVersion = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "WaymarkDataFileName" ) ) WaymarkDataFileName = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "WaymarkDataSectionIndex" ) ) WaymarkDataSectionIndex = uint.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "NumWaymarkDataLeadingBytes" ) ) NumWaymarkDataLeadingBytes = uint.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "NumberOfPresets" ) ) NumberOfPresets = uint.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "WaymarkIDOrder" ) ) WaymarkIDOrder = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "BytesPerWaymark" ) ) BytesPerWaymark = uint.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "ConfigBytesPerPreset" ) ) ConfigBytesPerPreset = uint.Parse( line.Split( '=' ).Last().Trim() );
					if( line.Split( '=' ).First().Trim().Equals( "ConfigFileMagicNumber" ) ) ConfigFileMagicNumber = byte.Parse( line.Split( '=' ).Last().Trim() );
				}
			}
		}

		//	Data Members
		public string GameVersion { get; protected set; }
		public string WaymarkDataFileName { get; protected set; }
		public uint WaymarkDataSectionIndex { get; protected set; }
		public uint NumWaymarkDataLeadingBytes { get; protected set; }
		public uint NumberOfPresets { get; protected set; }
		public uint NumberOfWaymarks() { return (uint)WaymarkIDOrder.Length; }
		public uint BytesPerWaymark { get; protected set; }
		public uint ConfigBytesPerPreset { get; protected set; }
		public byte ConfigFileMagicNumber { get; protected set; }
		public string WaymarkIDOrder { get; protected set; }
		public string ConfigFilePath { get; protected set; }
	}
}
