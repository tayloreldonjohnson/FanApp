using Hello.Data;
using Hello.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Services
{
    public interface IUserService
    {
        List<ApplicationUser> GetAllUsers();
        UserVM GetUserProfile( string email);
        void AddUserProfile(UserVM userVM);
		UserVM GetUserwithpost(string email);
		UserVM GetOtherUserwithpost(string id);
	}
}