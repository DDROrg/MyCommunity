using System;
using System.Collections.Generic;
using System.Text;

namespace MyCommunity.DAL.DTO
{
    public class UserDTO
    {
        public string id { get; set; }
        public string appartmentId { get; set; }
        public string loginId { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public bool isDeleted { get; set; }
    }
}
