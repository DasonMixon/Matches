using System.Linq;
using AutoMapper;
using Matches.Contracts;
using Matches.Contracts.Lobby;
using Matches.Contracts.Member;
using Matches.Core.Entities;

namespace Matches.Api
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Lobby, CreateLobbyResult>()
                .ForMember(d => d.LobbyId, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.JoinCode, s => s.MapFrom(src => src.JoinCode))
                .ForMember(d => d.LobbyModificationKey, s => s.MapFrom(src => src.ModificationKey))
                .ForMember(d => d.MemberId, s => s.MapFrom(src => src.Members.Single().Id))
                .ForMember(d => d.Preferences, s => s.MapFrom(src => src.Preferences))
                .ForMember(d => d.Status, s => s.MapFrom(src => Enums.CreateLobbyStatus.Created))
                .ForMember(d => d.Members, s => s.MapFrom(src => src.Members))
                .ForMember(d => d.Details, s => s.Ignore());

            CreateMap<Lobby, JoinLobbyResult>()
                .ForMember(d => d.LobbyId, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.JoinCode, s => s.MapFrom(src => src.JoinCode))
                .ForMember(d => d.Preferences, s => s.MapFrom(src => src.Preferences))
                .ForMember(d => d.Status, s => s.MapFrom(src => Enums.JoinLobbyStatus.Joined))
                .ForMember(d => d.Members, s => s.MapFrom(src => src.Members))
                .ForMember(d => d.Details, s => s.Ignore())
                .ForMember(d => d.MemberId, s => s.Ignore());

            CreateMap<Lobby.Member, MemberViewModel>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Attributes, s => s.MapFrom(src => src.Attributes));

            CreateMap<Lobby, LobbyDetailsResult>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.JoinCode, s => s.MapFrom(src => src.JoinCode))
                .ForMember(d => d.Members, s => s.MapFrom(src => src.Members))
                .ForMember(d => d.Preferences, s => s.MapFrom(src => src.Preferences));
        }
    }
}
