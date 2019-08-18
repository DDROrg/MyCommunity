using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MyCommunity.DAL.Models;
namespace MyCommunity.DAL
{
    public interface ICommunityContext
    {
        IMongoCollection<Appartment> Appartments { get; }
        IMongoCollection<User> Users { get; }
    }
}

