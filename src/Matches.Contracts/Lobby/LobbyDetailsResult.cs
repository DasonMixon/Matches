using System;
using System.Collections.Generic;
using Matches.Contracts.Member;

namespace Matches.Contracts.Lobby
{
    public class LobbyDetailsResult
    {
        public Guid Id { get; set; }
        public string JoinCode { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
        public List<MemberViewModel> Members { get; set; }
    }
}
