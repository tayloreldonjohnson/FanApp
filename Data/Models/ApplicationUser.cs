using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hello.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Hello.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public partial class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //{
        //	this.UserPosts = new HashSet<UserPost>();
        //}

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public string AboutMe { get; set; }
        public string ImageUrl { get; set; }

        //public ICollection<UserPost> UserPosts { get; set; }
        public List<Post> Posts { get; set; }
        public ICollection<Comment> Comment {get; set;}
	}
}
