using System;
using System.Linq;
using AutoMapper;
using Matches.Contracts.Lobby;
using Matches.Core.Entities;
using Matches.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Matches.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly ILogger<LobbyController> _logger;
        private readonly LobbyService _lobbyService;
        private readonly JoinCodeService _joinCodeService;
        private readonly IMapper _mapper;

        public LobbyController(ILogger<LobbyController> logger, LobbyService lobbyService,
            JoinCodeService joinCodeService, IMapper mapper)
        {
            _logger = logger;
            _lobbyService = lobbyService;
            _joinCodeService = joinCodeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ping")]
        public static string Ping()
        {
            return "pong";
        }

        // TODO: Need to add an api for a client to "register" with the matchmaking service.
        // This will give them a MemberId that they can send as part of Create and Join
        // Will validate they are actually able to create a lobby or join one

        [HttpPost]
        [Route("create")]
        public CreateLobbyResult Create(CreateLobbyRequest request)
        {
            try
            {
                _logger.LogDebug("Creating lobby");

                var lobby = Lobby.Create(request.Username, _joinCodeService);
                _lobbyService.Create(lobby);
                var result = _mapper.Map<CreateLobbyResult>(lobby);

                _logger.LogDebug($"Lobby created");

                return result;
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to create lobby: {ex.Message}");
                return new CreateLobbyResult
                {
                    Status = Contracts.Enums.CreateLobbyStatus.Rejected,
                    Details = "Unable to create lobby"
                };
            }
        }

        [HttpPost]
        [Route("join")]
        public ActionResult<JoinLobbyResult> Join(JoinLobbyRequest request)
        {
            try
            {
                _logger.LogDebug("Joining lobby");

                var lobby = _lobbyService.Get(request.LobbyId);
                if (lobby == null)
                    return NotFound();

                var member = lobby.AddMember(request.Username);

                _lobbyService.Update(lobby.Id, lobby);

                var result = _mapper.Map<JoinLobbyResult>(lobby);
                result.MemberId = member.Id;

                _logger.LogDebug("Lobby joined");

                return result;
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to join lobby: {ex.Message}");
                return new JoinLobbyResult
                {
                    Status = Contracts.Enums.JoinLobbyStatus.Rejected,
                    Details = "Unable to join lobby"
                };
            }
        }

        [HttpPost]
        [Route("preferences")]
        public ActionResult<ModifyLobbyPreferencesResult> ModifyPreferences(ModifyLobbyPreferencesRequest request)
        {
            try
            {
                _logger.LogDebug("Modifying lobby preferences");

                var lobby = _lobbyService.Get(request.LobbyId);
                if (lobby == null)
                    return NotFound();

                if (lobby.ModificationKey != request.LobbyModificationKey)
                {
                    _logger.LogError($"Attempted to modify lobby '{lobby.Id}' with incorrect modification key '{request.LobbyModificationKey}'");
                    return BadRequest();
                }

                lobby.Preferences = request.Preferences;
                _lobbyService.Update(lobby.Id, lobby);

                var result = new ModifyLobbyPreferencesResult { Status = Contracts.Enums.ModifyLobbyPreferencesStatus.Completed };

                _logger.LogDebug("Lobby preferences modified");

                return result;
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to modify lobby preferences for lobby '{request.LobbyId}': {ex.Message}");
                return new ModifyLobbyPreferencesResult
                {
                    Status = Contracts.Enums.ModifyLobbyPreferencesStatus.Rejected,
                    Details = "Unable to modify lobby preferences"
                };
            }
        }

        [HttpPost]
        [Route("leave")]
        public ActionResult<LeaveLobbyResult> Leave(LeaveLobbyRequest request)
        {
            try
            {
                _logger.LogDebug("Leaving lobby");

                var lobby = _lobbyService.Get(request.LobbyId);
                if (lobby == null)
                    return NotFound();

                var member = lobby.Members.SingleOrDefault(m => m.Id == request.MemberId);
                if (member == null)
                    return BadRequest();

                if (member.IsHost)
                {
                    _joinCodeService.Remove(lobby.JoinCode);
                    _lobbyService.Remove(lobby.Id);
                } else
                {
                    lobby.Members.Remove(member);
                    _lobbyService.Update(lobby.Id, lobby);
                }

                _logger.LogDebug("Lobby left");

                return new LeaveLobbyResult { Status = Contracts.Enums.LeaveLobbyStatus.Completed };
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to leave lobby '{request.LobbyId}' as memeber '{request.MemberId}': {ex.Message}");
                return new LeaveLobbyResult
                {
                    Status = Contracts.Enums.LeaveLobbyStatus.Rejected,
                    Details = "Unable to leave lobby"
                };
            }
        }

        [HttpGet]
        [Route("{lobbyId}")]
        public ActionResult<LobbyDetailsResult> GetDetails(Guid lobbyId)
        {
            _logger.LogDebug("Fetching lobby");

            var lobby = _lobbyService.Get(lobbyId);
            if (lobby == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<LobbyDetailsResult>(lobby);

            _logger.LogDebug($"Lobby fetched");

            return result;
        }
    }
}
