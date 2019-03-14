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

        int LoginUser(string Username, string Password);

        bool UpdateUserName(string currentUsername, string newUsername);
        bool UpdateUserRole(string currentUsername, string currentRole, string newRole);

        List<string> GetUsersByRole(string Role);

        //
        IEnumerable<UserModel> GetUsersBySearchKeyword(string searchKeyword);

        bool DeleteUser(string UserName, string Password);
    }
}
