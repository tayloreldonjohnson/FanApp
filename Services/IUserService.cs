using Hello.Data;
using Hello.Data.Models;
using System.Collections.Generic;

namespace Hello.Services
{
    public interface IUserService
    {
        List<ApplicationUser> GetAllUsers();
        UserVM GetUserProfile( string email);
    }
}