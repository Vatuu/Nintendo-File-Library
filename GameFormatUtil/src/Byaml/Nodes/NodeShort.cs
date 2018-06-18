using System;
using Vatuu.NintendoFileLibrary.Internal;

namespace Vatuu.NintendoFileLibrary
{
    public class NodeFloat : AbstractByamlNode<float>
    {

        public NodeFloat(String name, float value) : base(EnumNodeType.FLOAT, value, name) { }

        public override byte[] GetValueBytes()
        {
            return BitConverter.GetBytes(Value);
        }
    }
}
