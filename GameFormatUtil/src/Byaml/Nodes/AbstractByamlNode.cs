using System;

namespace Vatuu.NintendoFileLibrary.Internal
{
    public abstract class AbstractByamlNode<T>
    {
    
        public AbstractByamlNode(EnumNodeType identifier, T value, String name)
        {
            NodeIdentifier = identifier;
            Value = value;
            NodeName = name;
        }

        #region Properties

        public EnumNodeType NodeIdentifier { get; private set; }

        public T Value { get; private set; }

        public string NodeName { get; private set; }

        #endregion

        public AbstractByamlNode<T> UpdateValue(T update)
        {
            Value = update;
            return this;
        }

        public AbstractByamlNode<T> UpdateName(String name)
        {
            NodeName = name;
            return this;
        }

        public abstract byte[] GetValueBytes();
    }
}
