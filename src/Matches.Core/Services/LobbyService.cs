using System;
using System.Collections.Generic;
using Matches.Core.Database;
using Matches.Core.Entities;
using MongoDB.Driver;

namespace Matches.Core.Services
{
    public class LobbyService
    {
        private readonly IMongoCollection<Lobby> _lobbies;

        public LobbyService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _lobbies = database.GetCollection<Lobby>("lobby");
        }

        public List<Lobby> Get() => _lobbies.Find(lobby => true).ToList();

        public Lobby Get(Guid id) => _lobbies.Find(lobby => lobby.Id == id).FirstOrDefault();

        public Lobby Create(Lobby lobby)
        {
            _lobbies.InsertOne(lobby);
            return lobby;
        }

        public void Update(Guid id, Lobby lobby) => _lobbies.ReplaceOne(lobby => lobby.Id == id, lobby);

        public void Remove(Guid id) => _lobbies.DeleteOne(lobby => lobby.Id == id);
    }
}
