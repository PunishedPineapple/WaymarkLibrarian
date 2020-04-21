using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class GameDataConfig
	{
		//	Construction/Destruction
		public GameDataConfig( string waymarkDataFileName, uint waymarkDataOffset, uint numberOfPresets, uint numberOfWaymarks, uint bytesPerWaymark, uint configBytesPerPreset, byte configFileMagicNumber )
		{
			WaymarkDataFileName = waymarkDataFileName;
			WaymarkDataOffset = waymarkDataOffset;
			NumberOfPresets = numberOfPresets;
			NumberOfWaymarks = numberOfWaymarks;
			BytesPerWaymark = bytesPerWaymark;
			ConfigBytesPerPreset = configBytesPerPreset;
			ConfigFileMagicNumber = configFileMagicNumber;
		}

		//	Member Functions
		public uint BytesPerPreset() { return NumberOfWaymarks * BytesPerWaymark + ConfigBytesPerPreset; }

		//	Data Members
		public string WaymarkDataFileName { get; protected set; }
		public uint WaymarkDataOffset { get; protected set; }
		public uint NumberOfPresets { get; protected set; }
		public uint NumberOfWaymarks { get; protected set; }
		public uint BytesPerWaymark { get; protected set; }
		public uint ConfigBytesPerPreset { get; protected set; }
		public byte ConfigFileMagicNumber { get; protected set; }
	}
}
