using System;
using System.IO;
using Vatuu.NintendoFileLibrary;

namespace Yaz0er
{
    class Program
    {
        static void Main(string[] args)
        {
            String file = args[0];
            byte[] uncompressed = Yaz0.Decompress(file);
            String output = Path.GetDirectoryName(file) + "test.notyaz0";
            File.WriteAllBytes(output, uncompressed);
        }
    }
}
