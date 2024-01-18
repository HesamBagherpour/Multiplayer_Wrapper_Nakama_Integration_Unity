using System.Collections.Generic;
using System.Text;
using Nakama.TinyJson;

namespace Runtime.Utilities
{
    public static class GetState
    {
        public static IDictionary<string, string> GetStateAsDictionary(byte[] state) => Encoding.UTF8.GetString(state).FromJson<Dictionary<string, string>>();

        public static string GetStateAsJson(byte[] state) => Encoding.UTF8.GetString(state).ToString();
    }
}