using System;

namespace Matches.Contracts.Lobby
{
    public class LeaveLobbyRequest
    {
        public Guid LobbyId { get; set; }
        public Guid MemberId { get; set; }
    }
}
