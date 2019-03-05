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
        //for now create user has only 2 parameters
        //role to be added
        bool CreateUser(string Username, string Password);

        //change para
        bool UpdateUser(UserModel user);
        bool ValidateUser(string Username, string Password);

        IEnumerable<UserModel> GetRoles(int UserId);
        IEnumerable<UserModel> GetUsersByRole(string Role);
        IEnumerable<UserModel> GetUsersBySearchKeyword(string searchKeyword);

        bool Delete(int UserId);
    }
}
