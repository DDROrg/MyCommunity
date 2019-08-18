using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyCommunity.DAL.Models
{
    public class User
    {
        [BsonId]
        public string id { get; set; }
        public string appartmentId { get; set; }
        public string loginId { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
    }
}
