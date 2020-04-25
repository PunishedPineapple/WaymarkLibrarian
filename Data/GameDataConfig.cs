using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaymarkLibrarian
{
	class GameDataConfig
	{
		//	Construction
		public GameDataConfig()
		{
			SetDefaultConfig();
			ReadSavedConfig();

			WaymarkPreset.SetWaymarkIDOrder( WaymarkIDOrder );
		}

		//	Member Functions
		public uint BytesPerPreset() { return NumberOfWaymarks() * BytesPerWaymark + ConfigBytesPerPreset; }

		protected void SetDefaultConfig()
		{
			WaymarkDataFileName = "UISAVE.DAT";
			ExpectedFileLength_Bytes = 0x7420;
			WaymarkDataOffset = 0x6C97;
			NumberOfPresets = 5u;
			WaymarkIDOrder = "ABCD1234";
			BytesPerWaymark = 12u;
			ConfigBytesPerPreset = 8u;
			ConfigFileMagicNumber = 0x31;
		}

		protected void ReadSavedConfig()
		{
			//	*****TODO*****
		}

		//	Data Members
		public string WaymarkDataFileName { get; protected set; }
		public uint ExpectedFileLength_Bytes { get; protected set; }
		public uint WaymarkDataOffset { get; protected set; }
		public uint NumberOfPresets { get; protected set; }
		public uint NumberOfWaymarks() { return (uint)WaymarkIDOrder.Length; }
		public uint BytesPerWaymark { get; protected set; }
		public uint ConfigBytesPerPreset { get; protected set; }
		public byte ConfigFileMagicNumber { get; protected set; }
		public string WaymarkIDOrder { get; protected set; }
	}
}
