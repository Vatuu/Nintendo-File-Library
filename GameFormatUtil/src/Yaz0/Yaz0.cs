using System;
using System.IO;
using System.Text;
using Syroot.BinaryData;

namespace Vatuu.NintendoFileLibrary
{

    public static class Yaz0
    {

        #region Constants

        private const String MAGIC = "Yaz0";
        private const long DATA_START = 16;

        #endregion

        #region Decompress

        #region Overloads

        public static byte[] Decompress(String file, bool littleEndian = true)
        {
            return Decompress(File.ReadAllBytes(file), Encoding.ASCII, littleEndian);
        }

        public static byte[] Decompress(String file, Encoding encoding, bool littleEndian = true)
        {
            return Decompress(File.ReadAllBytes(file), encoding, littleEndian);
        }

        public static byte[] Decompress(byte[] data, bool littleEndian = true)
        {
            return Decompress(data, Encoding.ASCII, littleEndian);
        }

        #endregion

        public static byte[] Decompress(byte[] data, Encoding enc, bool littleEndian = true)
        {
            using (BinaryStream i = new BinaryStream(new MemoryStream(data), littleEndian ? ByteConverter.Little : ByteConverter.Big, enc))
            {
                //Header
                byte[] magic = i.ReadBytes(4);
                if (!IsYaz0Compressed(magic))
                    throw new Yaz0Exception("File is not Yaz0-compressed! MagicId = " + Encoding.ASCII.GetString(magic));
                long uncompressedSize = i.ReadUInt32();
                i.Move(8L); //Skip Unknown Long

                //Decoding starts here
                long dataEnd = data.Length;
                byte[] uncompressedData = new byte[uncompressedSize];

                long uncompressedPos = 0;

                byte groupHeader = (byte)i.ReadByte();

                while (i.Position < dataEnd && uncompressedPos < uncompressedSize)
                {
                    bool broken = false;
                    for (int c = 0; c < 8; c++)
                    {
                        if (i.Position >= dataEnd || uncompressedPos >= uncompressedSize)
                        {
                            broken = true;
                            break;
                        }

                        if (GetBitFromByte(groupHeader, c))
                        {
                            uncompressedData[uncompressedPos] = (byte)i.ReadByte();
                            uncompressedPos++;
                        }
                        else
                        {
                            byte byte1 = (byte)i.ReadByte();
                            byte byte2 = (byte)i.ReadByte();

                            long src = uncompressedPos - ((byte1 & 0x0F) << 8 | byte2) - 1;

                            byte index = (byte)(byte1 >> 4);

                            if (index != 0)
                                index = (byte)(i.ReadByte() + 0x12);
                            else
                                index += 2;

                            for (int c1 = 0; c1 < index; c1++)
                            {
                                uncompressedData[uncompressedPos] = uncompressedData[src];
                                uncompressedPos++;
                                src++;
                            }
                        }
                        groupHeader <<= 1;
                    }

                    if (!broken)
                    {
                        if (!(i.Position >= dataEnd || uncompressedPos >= uncompressedSize))
                            groupHeader = (byte)i.ReadByte();
                    }
                }
                return uncompressedData;
            }
        }

        #endregion

        #region Compress

        #region Overloads

        /*public static byte[] Compress(String file, Yaz0Compression level, bool littleEndian = true)
        {
            return Compress(File.ReadAllBytes(file), level, Encoding.ASCII, littleEndian);
        }

        public static byte[] Compress(String file, Yaz0Compression level, Encoding encoding, bool littleEndian = true)
        {
            return Compress(File.ReadAllBytes(file), level, encoding, littleEndian);
        }

        public static byte[] Compress(byte[] data, Yaz0Compression level, bool littleEndian = true)
        {
            return Compress(data, level, Encoding.ASCII, littleEndian);
        }*/

        #endregion

       /* public static byte[] Compress(byte[] data, Yaz0Compression level, Encoding encoding, bool littleEndian = true)
        {
            UInt32 uncompressedSize = (UInt32)data.Length;

            using(BinaryStream o = new BinaryStream(new MemoryStream(), littleEndian ? ByteConverter.Little : ByteConverter.Big, Encoding.ASCII))
            {
                //Header
                o.WriteString(MAGIC);
                o.WriteUInt32(uncompressedSize);
                o.WriteUInt64(0); //Unknown Long


            }
        }*/

        #endregion

        #region Utils

        public static bool IsYaz0Compressed(byte[] bytes)
        {
            if (bytes.Length < 4)
                throw new Yaz0Exception("Data is too short to be Yaz0 compressed!");

            byte[] magic = new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] };
            Console.WriteLine("Magic: " + MAGIC + " | " + Encoding.ASCII.GetString(magic));


            return Encoding.ASCII.GetString(magic).Equals(MAGIC);
        }

        private static bool GetBitFromByte(byte b, int index)
        {
            return (b & (1 << index - 1)) != 0;
        }

        #endregion

    }
}
