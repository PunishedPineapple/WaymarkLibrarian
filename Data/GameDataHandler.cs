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

			//	Determine the offset into the file where the waymark data is located.
			if( !File.Exists( fileName ) ) throw new Exception( "Error while reading game data file: File does not exist (" + fileName + ")" );
			uint waymarkDataLocation = GetWaymarkDataOffset( fileName );
			if( waymarkDataLocation == 0 ) throw new Exception( "Error while reading game data file: Could not find waymark section." );

			//	Read the raw data from the file.
			FileStream fs = File.OpenRead( fileName );
			if( fs.Length < waymarkDataLocation + mGameDataConfig.BytesPerPreset() * mGameDataConfig.NumberOfPresets ) throw new Exception( "Error while reading game data file: Unexpected file size." );
			byte[] rawData = new byte[mGameDataConfig.NumberOfPresets * mGameDataConfig.BytesPerPreset()];
			int numBytesToRead = (int)mGameDataConfig.NumberOfPresets * (int)mGameDataConfig.BytesPerPreset();
			fs.Seek( (int)waymarkDataLocation, SeekOrigin.Begin );
			uint bytesRead = (uint)fs.Read( rawData, 0, numBytesToRead );
			if( bytesRead != ( mGameDataConfig.NumberOfPresets * mGameDataConfig.BytesPerPreset() ) ) throw new Exception( "Error while reading game data file: Unexpected number of bytes read from file: " + bytesRead.ToString() );
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

			//	Write the data to the file.
			if( !File.Exists( fileName ) ) throw new Exception( "Error while preparing to write game data file: File does not exist (" + fileName + ")" );
			uint waymarkDataLocation = GetWaymarkDataOffset( fileName );
			if( waymarkDataLocation == 0 ) throw new Exception( "Error while preparing to write game data file: Could not find waymark section." );
			//Copying to a backup has occasionally changed the timestamps on the containing folder, so don't do it for now.  Maybe place it somewhere else too so there's less evidence of messing around in the configuration folders.
			//File.Copy( fileName, fileName + ".bak", true );
			FileStream fs = File.OpenWrite( fileName );
			if( fs.Length < waymarkDataLocation + mGameDataConfig.BytesPerPreset() * mGameDataConfig.NumberOfPresets ) throw new Exception( "Error while preparing to write game data file: Unexpected file size." );
			fs.Seek( (int)waymarkDataLocation, SeekOrigin.Begin );
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

		//	Copied in from a barebones UISAVE file reader and stripped to do just enough processing to find the waymark section.  It is going to be less error-prone to use this to find the offset into the file than rewriting this entire class to handle the file properly.
		public uint GetWaymarkDataOffset( string fileName )
		{
			//	Check that we can do stuff.
			if( !File.Exists( fileName ) ) throw new Exception( "File does not exist (" + fileName + ")" );
			if( !BitConverter.IsLittleEndian ) throw new Exception( "BitConverter is reporting Big-Endian, and we're not set up to deal with this." );

			//	Read in the raw data.
			byte[] rawData = File.ReadAllBytes( fileName );

			//	Parse the parts of the (16 byte) header that we care about.
			if( rawData.Length < 16 ) throw new Exception( "The file was not long enough to contain a full header.  Either the file or the header is corrupt, or the file was not completely read." );
			UInt32 numValidBytes = BitConverter.ToUInt32( rawData, 8 );

			//	Obtain the rest of the valid data and unpad it.
			if( rawData.Length < numValidBytes + 16 ) throw new Exception( "The file was shorter than the header indicated.  Either the file or the header is corrupt, or the file was not completely read." );
			byte[] correctedData = CorrectData( ReadBytes( rawData, 16u, numValidBytes, false ) );

			//	We don't care about the next 16 bytes.

			//	Locals for processing sections.
			uint currentOffset = 16u;
			UInt16 sectionID;
			UInt32 sectionLength;
			uint waymarkSectionOffset = 0u;

			//	And now we can start processing the sections.
			while( currentOffset < numValidBytes )
			{
				if( numValidBytes > currentOffset + 16u )
				{
					//	Read Section Header
					sectionID = BitConverter.ToUInt16( correctedData, (int)currentOffset );
					sectionLength = BitConverter.ToUInt32( correctedData, (int)currentOffset + 8 );
				}
				else
				{
					throw new Exception( "Expected section header, but encountered premature end of valid file region." );
				}

				if( numValidBytes > currentOffset + sectionLength )
				{
					//	If we found the section that we wanted, we can get out now.
					if( sectionID == mGameDataConfig.WaymarkDataSectionIndex )
					{
						//	Remember to offset by the 16 byte file header that we're not counting when looping through the sections, as well as the 16 byte section header.
						waymarkSectionOffset = currentOffset + 32 + mGameDataConfig.NumWaymarkDataLeadingBytes;
						break;
					}

					//	Move our current offset to the end of the section, including the header and the four bytes of trailing padding.
					currentOffset += 16u + sectionLength + 4u;
				}
				else
				{
					throw new Exception( "Encountered premature end of valid file region while processing a section." );
				}
			}

			return waymarkSectionOffset;
		}

		//	Data Members
		protected GameDataConfig mGameDataConfig;
	}
}