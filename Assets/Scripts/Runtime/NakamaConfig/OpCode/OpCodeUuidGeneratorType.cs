using System;

namespace Runtime.NakamaConfig.OpCode
{
    [Serializable]
    public enum OpCodeUuidGeneratorType
    {
        Static,
        ServerConfig,
        MultiPlayerNetwork
    }
}