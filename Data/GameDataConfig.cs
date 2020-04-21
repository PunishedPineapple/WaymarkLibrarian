using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class GameDataConfig
	{
		public GameDataConfig()
		{
			SetDefaultConfig();
		}

		//	Member Functions
		public uint BytesPerPreset() { return NumberOfWaymarks * BytesPerWaymark + ConfigBytesPerPreset; }
		protected void SetDefaultConfig()
		{
			WaymarkDataFileName = "UISAVE.DAT";
			WaymarkDataOffset = 0x6C97;
			NumberOfPresets = 5u;
			NumberOfWaymarks = 8u;
			BytesPerWaymark = 12u;
			ConfigBytesPerPreset = 8u;
			ConfigFileMagicNumber = 0x31;
		}

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
