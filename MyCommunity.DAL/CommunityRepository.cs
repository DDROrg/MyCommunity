using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCommunity.DAL.Models;

namespace MyCommunity.DAL
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ICommunityContext _context;
        public CommunityRepository(ICommunityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appartment>> GetAllAppartments()
        {
            return await _context.Appartments.Find(_ => true).ToListAsync();
        }

        public async Task<Appartment> GetAppartment(string id)
        {
            FilterDefinition<Appartment> filter = Builders<Appartment>.Filter.Eq(_apt => _apt.id, id);
            return await _context.Appartments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAppartment(Appartment apt)
        {
            await _context.Appartments.InsertOneAsync(apt);
        }

        public async Task<bool> UpdateAppartment(Appartment apt)
        {
            FilterDefinition<Appartment> filter = Builders<Appartment>.Filter.Eq(_apt => _apt.id, apt.id);
            ReplaceOneResult res = await _context.Appartments.ReplaceOneAsync(filter: filter, replacement: apt);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAppartment(string id)
        {
            FilterDefinition<Appartment> filter = Builders<Appartment>.Filter.Eq(_apt => _apt.id, id);
            DeleteResult res = await _context.Appartments.DeleteOneAsync(filter: filter);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public async Task<long> GetAppartmentCount()
        {
            long count = 0;
            try
            {
                count = await _context.Appartments.CountDocumentsAsync(new BsonDocument());
            }
            catch (Exception) { }
            return count;
        }

        //=====================================================================

        public async Task<IEnumerable<User>> GetUsers(string apparementId)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(_user => _user.appartmentId, apparementId);
            return await _context.Users.Find(filter).ToListAsync();
        }

        public async Task<User> GetUser(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(_user => _user.id, id);
            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<long> GetUserCount()
        {
            long count = 0;
            try
            {
                count = await _context.Users.CountDocumentsAsync(new BsonDocument());
            }
            catch (Exception) { }
            return count;
        }

        //=======================================================        
    }
}
