using System;
using System.Collections.Generic;

namespace Matches.Contracts.Lobby
{
    public class ModifyLobbyPreferencesRequest
    {
        public Guid LobbyId { get; set; }
        public Guid LobbyModificationKey { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
    }
}
