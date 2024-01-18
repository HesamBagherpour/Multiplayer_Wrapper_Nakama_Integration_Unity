namespace Runtime.NakamaConfig.OpCode
{
    public class OpCodeRegister
    {
        public string key;
        public long opCode;

        public OpCodeRegister(string key, long opCode)

        {
            this.key = key;
            this.opCode = opCode;
        }
    }
}