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
		//	Construction
		public GameDataHandler( GameDataConfig gameDataConfig )
		{
			mGameDataConfig = gameDataConfig;
		}

		//	Read and decode the waymark section of the game file.
		public GamePresetContainer ReadGameData( string fileName )
		{
			//	Object to populate with the game data.
			GamePresetContainer presets = new GamePresetContainer( mGameDataConfig.NumberOfPresets );

			//	Read the raw data from the file.
			if( !File.Exists( fileName ) ) throw new Exception( "File does not exist (" + fileName + ")" ) ;
			FileStream fs = File.OpenRead( fileName );
			if( fs.Length != mGameDataConfig.ExpectedFileLength_Bytes ) throw new Exception( "Error while reading game data file: Unexpected file size.  You may not have logged on with this character since the latest patch, or this program may not have been updated for the current patch." );
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
				for( uint waymarkNumber = 0u; waymarkNumber < presets[presetNumber].Waymarks.Length; ++waymarkNumber )
				{
					presets[presetNumber][waymarkNumber].Pos.X = BitConverter.ToInt32( ReadBytes( correctedData, offset + 0u, sizeof( Int32 ), !BitConverter.IsLittleEndian ), 0 ) / 1000.0;
					presets[presetNumber][waymarkNumber].Pos.Y = BitConverter.ToInt32( ReadBytes( correctedData, offset + 4u, sizeof( Int32 ), !BitConverter.IsLittleEndian ), 0 ) / 1000.0;
					presets[presetNumber][waymarkNumber].Pos.Z = BitConverter.ToInt32( ReadBytes( correctedData, offset + 8u, sizeof( Int32 ), !BitConverter.IsLittleEndian ), 0 ) / 1000.0;
					offset += 12u;
				}

				//	Which waymarks are active.
				byte activeMask = correctedData[offset];
				for( uint i = 0u; i < presets[presetNumber].Waymarks.Length; ++i )
				{
					uint testMask = 0x01;
					presets[presetNumber][i].IsEnabled = ( (uint)activeMask & ( testMask << (int)i ) ) > 0;
				}

				//	Skip reserved byte.

				//	Territory ID.
				presets[presetNumber].ZoneID = BitConverter.ToUInt16( ReadBytes( correctedData, offset + 2u, sizeof( Int16 ), !BitConverter.IsLittleEndian ), 0 );

				//	Time last modified.
				presets[presetNumber].LastModified = DateTimeOffset.FromUnixTimeSeconds( BitConverter.ToInt32( ReadBytes( correctedData, offset + 4u, sizeof( Int32 ), !BitConverter.IsLittleEndian ), 0 ) );
			}

			//	Return the processed data.
			return presets;
		}

		//	Encode and write the waymark section of the game file.
		public void WriteGameData( string fileName, byte[] data )
		{
			//	XOR the data as expected by the game.
			byte[] correctedData = CorrectData( data );

			//	Write the data to the file.  Create a backup first.
			if( !File.Exists( fileName ) ) throw new Exception( "File does not exist (" + fileName + ")" );
			File.Copy( fileName, fileName + ".bak", true );
			FileStream fs = File.OpenWrite( fileName );
			if( fs.Length != mGameDataConfig.ExpectedFileLength_Bytes ) throw new Exception( "Error while preparing to write game data file: Unexpected file size." );
			fs.Seek( (int)mGameDataConfig.WaymarkDataOffset, SeekOrigin.Begin );
			fs.Write( correctedData, 0, correctedData.Length );
			fs.Close();
		}

		//	Fill an unencoded byte array with the provided waymark data.
		public byte[] ConstructGameData( GamePresetContainer presets )
		{
			//	Memory to hold the new data.
			byte[] newData = new byte[mGameDataConfig.NumberOfPresets * mGameDataConfig.BytesPerPreset()];

			//	Process the data.
			for( uint presetNumber = 0; presetNumber < mGameDataConfig.NumberOfPresets; ++presetNumber )
			{
				uint offset = presetNumber * mGameDataConfig.BytesPerPreset();
				
				//	Waymark coordinates.
				for( uint waymarkNumber = 0u; waymarkNumber < presets[presetNumber].Waymarks.Length; ++waymarkNumber )
				{
					//	Make sure to write zeros if the waymark is active in order to keep the same format as the game itself writes.
					WriteBytes( BitConverter.GetBytes( presets[presetNumber][waymarkNumber].IsEnabled ? (Int32)( presets[presetNumber][waymarkNumber].Pos.X * 1000.0 ) : 0 ), newData, offset + 0u, !BitConverter.IsLittleEndian );
					WriteBytes( BitConverter.GetBytes( presets[presetNumber][waymarkNumber].IsEnabled ? (Int32)( presets[presetNumber][waymarkNumber].Pos.Y * 1000.0 ) : 0 ), newData, offset + 4u, !BitConverter.IsLittleEndian );
					WriteBytes( BitConverter.GetBytes( presets[presetNumber][waymarkNumber].IsEnabled ? (Int32)( presets[presetNumber][waymarkNumber].Pos.Z * 1000.0 ) : 0 ), newData, offset + 8u, !BitConverter.IsLittleEndian );
					offset += 12u;
				}

				//	Which waymarks are active.
				byte activeMask = 0x00;
				for( uint i = 0u; i < presets[presetNumber].Waymarks.Length; ++i )
				{
					if( presets[presetNumber][i].IsEnabled )
					{
						uint tempMask = 0x01;
						tempMask <<= (int)i;
						activeMask |= (byte)tempMask;
					}
				}
				WriteBytes( new byte[]{ activeMask }, newData, offset, !BitConverter.IsLittleEndian );

				//	Skip reserved byte.

				//	Territory ID.
				WriteBytes( BitConverter.GetBytes( presets[presetNumber].ZoneID ), newData, offset + 2u, !BitConverter.IsLittleEndian );

				//	Time last modified.
				WriteBytes( BitConverter.GetBytes( (Int32)presets[presetNumber].LastModified.ToUnixTimeSeconds() ), newData, offset + 4u, !BitConverter.IsLittleEndian );
			}

			//	Don't correct it here; let the actual write function do that.
			return newData;
		}

		//	Encode or decode raw data as required by the game's file format.
		protected byte[] CorrectData( byte[] data )
		{
			
			byte[] newData = (byte[])data.Clone();
			for( uint i = 0; i < newData.Length; ++i ) newData[i] ^= mGameDataConfig.ConfigFileMagicNumber;
			return newData;
		}

		//	Read specific bytes out of a byte array.
		protected byte[] ReadBytes( byte[] data, uint startPos, uint count, bool swapByteOrder )
		{
			//	*****TODO: Swap byte order if needed.*****
			byte[] newData = new byte[count];
			for( uint i = 0; i < count; ++i )
			{
				if( startPos + i < data.Length ) newData[i] = data[startPos + i];
			}
			return newData;
		}

		//	Write bytes into a specific position in a byte array.
		protected void WriteBytes( byte[] bytesToWrite, byte[] dest, uint startPos, bool swapByteOrder )
		{
			if( startPos + bytesToWrite.Length > dest.Length ) throw new Exception( "Attempt made to write beyond the end of the destination array." );

			//	*****TODO: Swap byte order if needed.*****
			for( uint i = 0u; i < bytesToWrite.Length; ++ i )
			{
				dest[startPos + i] = bytesToWrite[i];
			}
		}

		//	Data Members
		protected GameDataConfig mGameDataConfig;
	}
}