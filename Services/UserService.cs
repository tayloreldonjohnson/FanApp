using Hello.Data;
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
        public ApplicationUser  GetUserProfile( string email )
        {

            var user = _uManager.Users.Where(m => m.Email == email).Select(m => new ApplicationUser
            {
                UserName = m.UserName
               
            }).FirstOrDefault();
            return user;

        }

    }
  
}
