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
        
        bool CreateUser(string username, string password, string role);

        int LoginUser(string username, string password);

        bool UpdateUserName(string currentUsername, string newUsername);
        bool UpdateUserRole(string currentUsername, string currentRole, string newRole);

        IEnumerable<string> GetUsersByRole(string role);

        //
        IEnumerable<UserModel> GetUsersBySearchKeyword(string searchKeyword);

        bool DeleteUser(string userName);
    }
}
