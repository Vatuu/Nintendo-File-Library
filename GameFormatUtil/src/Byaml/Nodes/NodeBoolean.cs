using System;
using Vatuu.NintendoFileLibrary.Internal;

namespace Vatuu.NintendoFileLibrary
{
    public class NodeBoolean : AbstractByamlNode<Boolean>
    {
        public NodeBoolean(String name, bool value) : base(EnumNodeType.BOOLEAN, value, name) { }

        public override byte[] GetValueBytes()
        {
            if (Value)
                return new byte[] { 0x00, 0x00, 0x00, 0x01 };
            else
                return new byte[] { 0x00, 0x00, 0x00, 0x00 };
        }
    }
}
