using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xfire_server.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserAlias { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }        
        public string UserEmailId { get; set; }
        public string UserPassword { get; set; }
    }
}
