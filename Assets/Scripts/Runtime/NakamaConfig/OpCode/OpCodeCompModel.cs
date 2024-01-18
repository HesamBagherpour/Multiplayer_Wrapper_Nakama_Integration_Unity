using System;

namespace Runtime.NakamaConfig.OpCode
{
    [Serializable]
    public class OpCodeCompModel
    {
        public string Key;

        // [HideInInspector]
        public long OpCode;

        // [HideInInspector]
        public string Uuid;
        public OpCodeUuidGeneratorType UuidGeneratorType = OpCodeUuidGeneratorType.Static;
    }
}
