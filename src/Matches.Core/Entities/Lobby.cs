using System;
using System.Collections.Generic;
using Matches.Core.Services;
using MongoDB.Bson.Serialization.Attributes;

namespace Matches.Core.Entities
{
    public class Lobby
    {
        [BsonId]
        public Guid Id { get; set; }

        public string JoinCode { get; set; }

        public Guid ModificationKey { get; set; }

        public Dictionary<string, object> Preferences { get; set; }

        public List<Member> Members { get; set; }

        public class Member
        {
            public Guid Id { get; set; }
            public bool IsHost { get; set; }
            public Dictionary<string, object> Attributes { get; set; }
        }

        public static Lobby Create(string hostUsername, IJoinCodeService joinCodeService)
        {
            if (hostUsername == null)
                throw new ArgumentNullException("hostUsername");

            if (joinCodeService == null)
                throw new ArgumentNullException("joinCodeService");

            return new Lobby
            {
                Id = Guid.NewGuid(),
                JoinCode = joinCodeService.Generate().Id,
                Members = new List<Member>
                {
                    new Member
                    {
                        Id = Guid.NewGuid(),
                        IsHost = true,
                        Attributes = new Dictionary<string, object>
                        {
                            { "Username", hostUsername }
                        }
                    }
                },
                ModificationKey = Guid.NewGuid(),
                Preferences = new Dictionary<string, object>()
            };
        }

        public Member AddMember(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");

            var member = new Member
            {
                Id = Guid.NewGuid(),
                IsHost = false,
                Attributes = new Dictionary<string, object>
                {
                    { "Username", username }
                }
            };

            Members.Add(member);

            return member;
        }
    }
}
