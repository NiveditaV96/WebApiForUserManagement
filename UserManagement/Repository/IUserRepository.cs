using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Repository
{
    public interface IUserRepository
    {
        
        bool CreateUser(string Username, string Password, string Role);

        //change para
        bool UpdateUser(UserModel user);
        int LoginUser(string Username, string Password);

        IEnumerable<UserModel> GetRoles(string UserId);
        IEnumerable<UserModel> GetUsersByRole(string Role);
        IEnumerable<UserModel> GetUsersBySearchKeyword(string searchKeyword);

        bool DeleteUser(string UserName, string Password);
    }
}
