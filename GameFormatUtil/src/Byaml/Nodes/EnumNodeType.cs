namespace Vatuu.NintendoFileLibrary
{
    public enum EnumNodeType : byte
    {
        STRING = 0xA0,
        PATH = 0xA1,
        ARRAY = 0xC0,
        DICTIONARY = 0xC1,
        STRING_TABLE = 0xC2,
        PATH_TABLE = 0xC3,
        BOOLEAN = 0xD0,
        INTEGER = 0xD1,
        FLOAT = 0xD2,
        HASH = 0xD3
    }
}
