using System;

namespace Matches.Contracts.Lobby
{
    public class JoinLobbyRequest
    {
        public Guid LobbyId { get; set; }
        public string Username { get; set; }
    }
}
