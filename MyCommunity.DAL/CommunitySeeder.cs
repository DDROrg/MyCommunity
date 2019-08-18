using MyCommunity.DAL.Models;
using MyCommunity.Utility.Extensions;
using System;

namespace MyCommunity.DAL
{
    public class CommunitySeeder : ISeed
    {
        protected readonly ICommunityRepository _repo;
        public CommunitySeeder(ICommunityRepository repo)
        {
            _repo = repo;
        }
        public void Seed()
        {
            InsertUser();
            InsertAppartment();
        }
        private async void InsertAppartment()
        {
            //Guid aptId = Guid.NewGuid();
            Guid aptId = new Guid("d14ed311-6ea5-4d0c-8e89-b7f7fe10b9ff");
            Appartment apt = new Appartment()
            {
                id = aptId.ToString(),
                customerId = aptId.ToFriendlyId(),
                name = "SUPPORT",
                status = "ACTIVE",
                createdOn = DateTime.Now,
                modifiedOn = DateTime.Now,
                address = new Address() { buildingNo = "90/A/2", street = "Gouranga Sarani" }
            };

            var aptCount = await _repo.GetAppartmentCount();
            if (aptCount == 0)
            {
                await _repo.CreateAppartment(apt);
                User user = new User()
                {
                    id = Guid.NewGuid().ToString(),
                    appartmentId = apt.id,
                    loginId = "SYSADMIN",
                    role = "SYSADMIN",
                    password = "MLP@pass1234",
                    name = "SyaAdmin",
                    email = "support@malinator.com",
                    status = "ACTIVE",
                    createdOn = DateTime.Now,
                    modifiedOn = DateTime.Now
                };
                await _repo.CreateUser(user);
            }
        }

        private void InsertUser()
        {
        }
    }
}
