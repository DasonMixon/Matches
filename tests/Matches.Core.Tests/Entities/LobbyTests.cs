using System;
using System.Collections.Generic;
using Matches.Core.Entities;
using Matches.Core.Services;
using Moq;
using Shouldly;
using Xunit;

namespace Matches.Core.Tests.Entities
{
    public class LobbyTests : IDisposable
    {
		private Mock<IJoinCodeService> joinCodeServiceMock;

        private readonly string _joinCode = "ABCDEF";

        public LobbyTests()
        {
            joinCodeServiceMock = new Mock<IJoinCodeService>();

            joinCodeServiceMock.Setup(j => j.Generate()).Returns(new JoinCode
            {
                Id = _joinCode
            });
        }

        public void Dispose()
        {
        }

		[Fact]
        public void Validate_Create_throws_exception_for_null_hostUsername()
        {
            Should.Throw<ArgumentNullException>(() => Lobby.Create(null, joinCodeServiceMock.Object));
        }

        [Fact]
        public void Validate_Create_throws_exception_for_null_joinCodeService()
        {
            Should.Throw<ArgumentNullException>(() => Lobby.Create("user1", null));
        }

        [Fact]
        public void Validate_Create_populates_initial_state()
        {
            var result = Lobby.Create("user1", joinCodeServiceMock.Object);

            result.Id.ShouldNotBe(Guid.Empty);
            result.JoinCode.ShouldBe(_joinCode);
            result.ModificationKey.ShouldNotBe(Guid.Empty);
            result.Preferences.ShouldNotBeNull();
            result.Preferences.Count.ShouldBe(0);
            result.Members.ShouldNotBeNull();
            result.Members.Count.ShouldBe(1);

            var member = result.Members[0];
            member.Id.ShouldNotBe(Guid.Empty);
            member.IsHost.ShouldBeTrue();
            member.Attributes.ShouldNotBeNull();
            member.Attributes.Count.ShouldBe(1);

            var memberAttribute = member.Attributes["Username"];
            memberAttribute.ShouldBe("user1");
        }

        [Fact]
        public void Validate_AddMember_throws_exception_for_null_username()
        {
            var lobby = new Lobby
            {
                Members = new List<Lobby.Member>()
            };

            Should.Throw<ArgumentNullException>(() => lobby.AddMember(null));
        }

        [Fact]
        public void Validate_AddMember_adds_to_members_list()
        {
            var existingMemeber = new Lobby.Member
            {
                Id = Guid.NewGuid(),
                IsHost = true,
                Attributes = new Dictionary<string, object>
                {
                    { "Username", "user1" }
                }
            };

            var lobby = new Lobby
            {
                Members = new List<Lobby.Member> { existingMemeber }
            };

            var result = lobby.AddMember("user2");

            lobby.Members.Count.ShouldBe(2);
            lobby.Members.ShouldContain(existingMemeber);
            lobby.Members.ShouldContain(result);

            result.Id.ShouldNotBe(Guid.Empty);
            result.IsHost.ShouldBeFalse();
            result.Attributes.ShouldNotBeNull();
            result.Attributes.Count.ShouldBe(1);

            var memberAttribute = result.Attributes["Username"];
            memberAttribute.ShouldBe("user2");
        }
    }
}
