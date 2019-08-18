using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyCommunity.DAL.Models
{
    public class Appartment
    {
        [BsonId]
        public string id { get; set; }
        public string customerId { get; set; }
        public string name { get; set; }
        public Address address { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
    }

    public class Address
    {
        public string buildingNo { get; set; }
        public string street { get; set; }
        public string addLine1 { get; set; }
        public string addLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int pin { get; set; }
    }
}
