using System;
using System.Collections.Generic;
using System.Linq;
using Matches.Core.Database;
using Matches.Core.Entities;
using Matches.Core.Utilities;
using MongoDB.Driver;

namespace Matches.Core.Services
{
    public class JoinCodeService : IJoinCodeService
    {
        private readonly IMongoCollection<JoinCode> _joinCodes;

        // TODO: Use repository pattern
        public JoinCodeService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _joinCodes = database.GetCollection<JoinCode>("joincode");
        }

        public List<JoinCode> Get() => _joinCodes.Find(joinCode => true).ToList();

        public JoinCode Create(JoinCode joinCode)
        {
            _joinCodes.InsertOne(joinCode);
            return joinCode;
        }

        public JoinCode Generate()
        {
            JoinCode joinCode = null;

            try
            {
                var currentJoinCodes = Get().Select(jc => jc.Id);
                joinCode = JoinCodeUtility.Generate(currentJoinCodes);
                return Create(joinCode);
            } catch (MongoWriteException mwe)
            {
                if (mwe.WriteError.Category == ServerErrorCategory.DuplicateKey && joinCode != null)
                {
                    throw new AggregateException($"Unable to insert join code '{joinCode.Id}' as it already exists", mwe);
                }

                throw;
            }
        }

        public void Remove(string id) => _joinCodes.DeleteOne(joinCode => joinCode.Id == id);
    }
}
