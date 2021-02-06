using System.Collections.Generic;
using Matches.Core.Entities;

namespace Matches.Core.Services
{
    public interface IJoinCodeService
    {
        List<JoinCode> Get();
        JoinCode Create(JoinCode joinCode);
        JoinCode Generate();
        void Remove(string id);
    }
}
