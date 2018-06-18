using System;
using Vatuu.NintendoFileLibrary.Internal;

namespace Vatuu.NintendoFileLibrary
{
    public class NodeHash : AbstractByamlNode<Crc32Hash>
    {
        public NodeHash(String name, Crc32Hash value) : base(EnumNodeType.HASH, value, name) { }

        public override byte[] GetValueBytes()
        {
            return Value.Hash;
        }
    }

    public class Crc32Hash
    {
        public byte[] Hash { get; private set; }

        public Crc32Hash(byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new IndexOutOfRangeException("Crc32 Hashes need to have a length of 4 Bytes.");

            Hash = bytes;
        }

        public Crc32Hash(String hash)
        {
            if(hash.Length != 8)
                throw new IndexOutOfRangeException("Crc32 Hash Strings need to have a length of 8 characters.");

            byte[] bytes = new byte[4];
            for(int i = 0; i < 8; i += 2)
                bytes[i / 2] = Convert.ToByte(hash.Substring(i, 2), 16);

            Hash = bytes;
        }

        public Crc32Hash UpdateHash(String hash)
        {
            if (hash.Length != 8)
                throw new IndexOutOfRangeException("Crc32 Hash Strings need to have a length of 8 characters.");

            byte[] bytes = new byte[4];
            for (int i = 0; i < 8; i += 2)
                bytes[i / 2] = Convert.ToByte(hash.Substring(i, 2), 16);

            Hash = bytes;

            return this;
        }

        public Crc32Hash UpdateHash(byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new IndexOutOfRangeException("Crc32 Hashes need to have a length of 4 Bytes.");

            Hash = bytes;

            return this;
        }

        public String GetAsString() 
        {
            return BitConverter.ToString(Hash).Replace("-", "");
        }
    }
}
