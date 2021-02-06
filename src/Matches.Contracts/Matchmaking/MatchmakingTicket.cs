using System;

namespace Matches.Contracts.Matchmaking
{
    public class MatchmakingTicket
    {
        public Guid Id { get; set; }
        public Enums.MatchmakingTicketStatus Status { get; set; }

        public static MatchmakingTicket New => new MatchmakingTicket
        {
            Id = Guid.NewGuid(),
            Status = Enums.MatchmakingTicketStatus.NotSubmitted
        };
    }
}
