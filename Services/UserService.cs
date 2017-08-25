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
		public ApplicationDbContext _db;
        public UserService(UserManager<ApplicationUser> uManager, ApplicationDbContext db)
        {
            _uManager = uManager;
			_db = db;
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

        public void AddUserProfile(UserVM userVm)
        {

			//         var IntendedUser = _uManager.Users.Where(m => m.Email == user.Email).FirstOrDefault();
			//         IntendedUser.AboutMe = user.AboutMe;
			//IntendedUser.ImageUrl = user.ImageUrl;
			//         await _uManager.UpdateAsync(IntendedUser);
			// _db.SaveChanges();

			//return;

			var user = _db.ApplicationUser.Where(u => u.Email == userVm.Email).FirstOrDefault();
			user.AboutMe = userVm.AboutMe;
			user.ImageUrl = userVm.ImageUrl;
			_db.Update(user);
			_db.SaveChanges();
        }

    }
  
}
