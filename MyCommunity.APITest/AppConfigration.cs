using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using MyCommunity.DAL;


namespace MyCommunity.APITest
{
    public class AppConfigration
    {
        private static IConfigurationRoot _config;
        public AppConfigration(){
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        public MongoDBConfig  GetMongoDBConfig()
        {   
            var config = new ServerConfig();            
            _config.Bind(config);
            return config.MongoDB; 
        }

        public bool  IsRealMongoDbUsed()
        {   
            return _config.GetValue<bool>("IsRealMongoDbUsed");
        }
    }
}