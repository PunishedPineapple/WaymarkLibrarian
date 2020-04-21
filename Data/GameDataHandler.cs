using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaymarkLibrarian
{
	class GameDataHandler
	{
		public GameDataHandler( GameDataConfig gameDataConfig )
		{
			mGameDataConfig = gameDataConfig;
		}

		public WaymarkPresets ReadGameData( string fileName )
		{
			//	Object to populate with the game data.
			WaymarkPresets waymarkData = new WaymarkPresets( mGameDataConfig.NumberOfPresets, mGameDataConfig.NumberOfWaymarks );

			//	Read the raw data from the file.
			if( !File.Exists( fileName ) ) throw new Exception( "File does not exist (" + fileName + ")" ) ;
			FileStream fs = File.OpenRead( fileName );
			byte[] rawData = new byte[mGameDataConfig.NumberOfPresets * mGameDataConfig.BytesPerPreset()];
			int numBytesToRead = (int)mGameDataConfig.NumberOfPresets * (int)mGameDataConfig.BytesPerPreset();
			fs.Seek( (int)mGameDataConfig.WaymarkDataOffset, SeekOrigin.Begin );
			uint bytesRead = (uint)fs.Read( rawData, 0, numBytesToRead );
			if( bytesRead != ( mGameDataConfig.NumberOfPresets * mGameDataConfig.BytesPerPreset() ) ) throw new Exception( "Unexpected number of bytes read from file: " + bytesRead.ToString() );
			fs.Close();

			//	Process the data.
			byte[] correctedData = CorrectData( rawData );
			for( uint presetNumber = 0; presetNumber < mGameDataConfig.NumberOfPresets; ++presetNumber )
			{
				uint offset = presetNumber * mGameDataConfig.BytesPerPreset();

				//	Waymark coordinates.
				for( uint waymarkNumber = 0u; waymarkNumber < waymarkData.Presets[presetNumber].Waymarks.Length; ++waymarkNumber )
				{
					waymarkData[presetNumber][waymarkNumber].Pos.X = BitConverter.ToInt32( VerifyEndianness( correctedData, offset + 0u, sizeof( Int32 ), BitConverter.IsLittleEndian ), 0 ) / 1000.0;
					waymarkData[presetNumber][waymarkNumber].Pos.Y = BitConverter.ToInt32( VerifyEndianness( correctedData, offset + 4u, sizeof( Int32 ), BitConverter.IsLittleEndian ), 0 ) / 1000.0;
					waymarkData[presetNumber][waymarkNumber].Pos.Z = BitConverter.ToInt32( VerifyEndianness( correctedData, offset + 8u, sizeof( Int32 ), BitConverter.IsLittleEndian ), 0 ) / 1000.0;
					offset += 12u;
				}

				//	Which waymarks are active.
				byte activeMask = correctedData[offset];
				for( uint i = 0u; i < waymarkData[presetNumber].Waymarks.Length; ++i )
				{
					uint testMask = 0x01;
					waymarkData[presetNumber][i].IsEnabled = ( (uint)activeMask & ( testMask << (int)i ) ) > 0;
				}

				//	Skip reserved byte.

				//	Territory ID.
				waymarkData[presetNumber].ZoneID = BitConverter.ToUInt16( VerifyEndianness( correctedData, offset + 2u, sizeof( Int16 ), BitConverter.IsLittleEndian ), 0 );

				//	Time last modified.
				waymarkData[presetNumber].LastModified = DateTimeOffset.FromUnixTimeSeconds( BitConverter.ToInt32( VerifyEndianness( correctedData, offset + 4u, sizeof( Int32 ), BitConverter.IsLittleEndian ), 0 ) ).UtcDateTime;
			}

			//	Return the processed data.
			return waymarkData;
		}

		public void WriteGameData( string fileName, byte[] data )
		{
			//	*****TODO: Verify very carefully that the file is the expected size, etc. before writing.*****
		}

		protected byte[] ConstructGameData( WaymarkPresets presets )
		{
			byte[] newData = new byte[mGameDataConfig.NumberOfPresets * mGameDataConfig.BytesPerPreset()];
			
			return newData;
		}

		protected byte[] CorrectData( byte[] data )
		{
			byte[] newData = (byte[])data.Clone();
			for( uint i = 0; i < newData.Length; ++i ) newData[i] ^= mGameDataConfig.ConfigFileMagicNumber;
			return newData;
		}

		protected byte[] VerifyEndianness( byte[] data, uint startPos, uint count, bool wantLittleEndian )
		{
			//	*****TODO: Swap byte order if needed.*****
			byte[] newData = new byte[count];
			for( uint i = 0; i < count; ++i )
			{
				if( startPos + i < data.Length ) newData[i] = data[startPos + i];
			}
			return newData;
		}

		//	Members
		protected GameDataConfig mGameDataConfig;
	}
}