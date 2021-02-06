namespace Matches.Contracts.Lobby
{
    public class LeaveLobbyResult
    {
        public Enums.LeaveLobbyStatus Status { get; set; }
        public string Details { get; set; }
    }
}
