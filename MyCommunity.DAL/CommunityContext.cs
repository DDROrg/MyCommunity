using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MyCommunity.DAL.Models;
namespace MyCommunity.DAL
{
    public class CommunityContext : ICommunityContext
    {
        private readonly IMongoDatabase _db;

        public CommunityContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Appartment> Appartments => _db.GetCollection<Appartment>("Appartments");
        public IMongoCollection<User> Users => _db.GetCollection<User>("Users");
    }
}
