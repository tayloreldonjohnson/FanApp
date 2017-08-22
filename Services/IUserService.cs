using Hello.Data;
using System.Collections.Generic;

namespace Hello.Services
{
    public interface IUserService
    {
        List<ApplicationUser> GetAllUsers();
        ApplicationUser  GetUserProfile( string email);
    }
}