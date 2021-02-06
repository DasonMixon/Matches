using System;
using System.Collections.Generic;
using Matches.Contracts.Member;

namespace Matches.Contracts.Lobby
{
    public class JoinLobbyResult
    {
        public JoinLobbyResult()
        {
            Members = new List<MemberViewModel>();
            Preferences = new Dictionary<string, object>();
        }

        public Guid LobbyId { get; set; }
        public string JoinCode { get; set; }
        public Guid MemberId { get; set; }
        public Enums.JoinLobbyStatus Status { get; set; }
        public List<MemberViewModel> Members { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
        public string Details { get; set; }
    }
}
