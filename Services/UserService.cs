using Hello.Data;
using Hello.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Services
{
    public class UserService : IUserService
    {

        public UserManager<ApplicationUser> _uManager;
        public UserService(UserManager<ApplicationUser> uManager)
        {
            _uManager = uManager;
        }
        public List<ApplicationUser> GetAllUsers()
        {
            var users = _uManager.Users.ToList();

            return users;
        }
        public UserVM GetUserProfile( string email )
        {
            
            var user = _uManager.Users.Where(m => m.Email == email).FirstOrDefault();
            var newUser = new UserVM
            {
              
				UserName = user.UserName,
                Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				DateCreated = user.DateCreated,
				AboutMe = user.AboutMe,
				ImageUrl = user.ImageUrl

            };
    
            return newUser;

        }


    }
  
}
