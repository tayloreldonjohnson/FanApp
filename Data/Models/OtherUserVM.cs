using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class OtherUserVM
    {
        public string OtherUserId { get; set; }
        public string OtherEmail { get; set; }
        public string OtherUserName { get; set; }
        public string OtherFirstName { get; set; }
        public string OtherLastName { get; set; }
        public DateTime OtherDateCreated { get; set; }
        public string OtherAboutMe { get; set; }
        public string OtherImageUrl { get; set; }
        public List<Post> Posts { get; set; }

    }
}
