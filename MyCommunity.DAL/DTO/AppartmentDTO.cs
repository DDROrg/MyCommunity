using System;
using System.Collections.Generic;
using System.Text;

namespace MyCommunity.DAL.DTO
{
    public class AppartmentDTO
    {
        public string id { get; set; }
        public string customerId { get; set; }
        public string name { get; set; }
        public AddressDTO address { get; set; }
        public string status { get; set; }
    }

    public class AddressDTO
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
