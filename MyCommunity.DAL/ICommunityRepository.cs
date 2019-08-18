using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyCommunity.DAL.Models;
namespace MyCommunity.DAL
{
    public interface ICommunityRepository
    {
        // api/[GET]
        Task<IEnumerable<Appartment>> GetAllAppartments();
        // api/1/[GET]
        Task<Appartment> GetAppartment(string id);
        // api/[POST]
        Task CreateAppartment(Appartment apt);
        // api/[PUT]
        Task<bool> UpdateAppartment(Appartment apt);
        // api/1/[DELETE]
        Task<bool> DeleteAppartment(string id);
        Task<long> GetAppartmentCount();

        //=============================================
        Task<User> GetUser(string id);
        // api/[POST]
        Task CreateUser(User user);
        Task<long> GetUserCount();
    }
}
