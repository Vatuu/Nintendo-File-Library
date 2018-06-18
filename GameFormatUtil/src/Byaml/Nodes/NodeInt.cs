using System;
using Vatuu.NintendoFileLibrary.Internal;

namespace Vatuu.NintendoFileLibrary
{
    public class NodeInt : AbstractByamlNode<Int32>
    {
        public NodeInt(String name, int value) : base(EnumNodeType.INTEGER, value, name) { }

        public override byte[] GetValueBytes()
        {
            return BitConverter.GetBytes(Value);
        }
    }
}
