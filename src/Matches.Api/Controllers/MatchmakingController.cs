using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Matches.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchmakingController : ControllerBase
    {
        private readonly ILogger<MatchmakingController> _logger;

        public MatchmakingController(ILogger<MatchmakingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ping")]
        public static string Ping()
        {
            return "pong";
        }

        /*
         * What things should the matchmaking controller be able do?
         *  1. Allow a lobby to submit a matchmaking request ticket (can be 1 to n players in the lobby)
         *  2. Allow a lobby to revoke their matchmaking request ticket
         */
    }
}
