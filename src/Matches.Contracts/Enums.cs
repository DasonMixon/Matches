namespace Matches.Contracts
{
    public class Enums
    {
        public enum CreateLobbyStatus
        {
            Unknown = 0,
            Rejected = 1,
            Created = 2
        }

        public enum JoinLobbyStatus
        {
            Unknown = 0,
            Rejected = 1,
            Joined = 2
        }

        public enum ModifyLobbyPreferencesStatus
        {
            Unknown = 0,
            Rejected = 1,
            Completed = 2
        }

        public enum LeaveLobbyStatus
        {
            Unknown = 0,
            Rejected = 1,
            Completed = 2
        }

        public enum MatchmakingTicketStatus
        {
            Unknown = 0,
            NotSubmitted = 1,
            Submitted = 2,
            Revoked = 3
        }

        public enum CustomValueTypes
        {
            Unknown = 0,
            String = 1,
            Integer = 2,
            Float = 3,
            Bool = 4
        }
    }
}
