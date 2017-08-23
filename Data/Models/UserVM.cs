using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class UserVM
    {
        public string Email { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
		public DateTime DateCreated { get; set; }
		public string AboutMe { get; set; }
		public string ImageUrl { get; set; }

	}
}
