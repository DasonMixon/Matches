using System;
using System.Collections.Generic;
using Matches.Contracts.Matchmaking;
using Matches.Contracts.Member;

namespace Matches.Contracts.Lobby
{
    public class LobbyData
    {
        public LobbyData()
        {
            Members = new List<MemberData>();
            Preferences = new Dictionary<string, object>();
        }

        public Guid Id { get; set; }
        public List<MemberData> Members { get; set; }
        public int MemberCount => Members.Count;
        public Dictionary<string, object> Preferences { get; set; }
        public int PreferencesCount => Preferences.Count;
        public MatchmakingTicket MatchmakingTicket { get; set; }
    }
}
