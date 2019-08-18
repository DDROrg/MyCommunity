using System;
using System.Collections.Generic;
using System.Text;

namespace MyCommunity.DAL
{
    public class ServerConfig
    {
        public MongoDBConfig MongoDB { get; set; } = new MongoDBConfig();
    }
}
