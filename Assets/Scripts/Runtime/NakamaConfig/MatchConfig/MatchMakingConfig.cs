using System.Collections.Generic;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public class MatchMakingConfig
    {
        public int minPlayers;
        public int maxPlayers;
        public string query;
        public Dictionary<string, string> matchmakingProperties;

        public MatchMakingConfig(int minPlayers, int maxPlayers, string query, Dictionary<string, string> matchmakingProperties)
        {
            this.minPlayers = minPlayers;
            this.maxPlayers = maxPlayers;
            this.query = query;
            this.matchmakingProperties = matchmakingProperties;
        }


        public MatchMakingConfig()
        {
            query = "properties.engine:unity";
            matchmakingProperties = new Dictionary<string, string>
            {
                { "engine", "unity" }
            };

            this.minPlayers = 2;
            this.maxPlayers = 4;
        }
    }
}