using System;
using System.Collections.Generic;
using Matches.Contracts.Member;

namespace Matches.Contracts.Lobby
{
    public class CreateLobbyResult
    {
        public Guid LobbyId { get; set; }
        public Guid MemberId { get; set; }
        public string JoinCode { get; set; }
        public Guid LobbyModificationKey { get; set; }
        public Enums.CreateLobbyStatus Status { get; set; }
        public string Details { get; set; }
        public List<MemberViewModel> Members { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
    }
}
