using System;
using System.Collections.Generic;
using UserManagement.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace UserManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["SqlConString"].ConnectionString;
       

       
        /// <summary>
        ///  Method to create user.
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public bool CreateUser(string Username, string Password, string Role)
        {
           
            
            string insertUserName = "Insert Into aspnet_Users ([ApplicationId], [UserId], [UserName], [LoweredUserName], [LastActivityDate] )" +
                              "values((select[ApplicationId] from aspnet_Applications where ApplicationName = 'UserApplication'), NEWID(), '"
                              + Username + "', LOWER('" + Username + "'), GETDATE())";

            string insertPassword = "Insert Into aspnet_Membership ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [IsApproved], [IsLockedOut], [CreateDate]," +
                              " [LastLoginDate],[LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart],[FailedPasswordAnswerAttemptCount], " +
                              "[FailedPasswordAnswerAttemptWindowStart]) values ((select[ApplicationId] from aspnet_Users where UserName = '" + Username + "'), " +
                              "(select[UserId] from aspnet_Users where UserName = '" + Username + "'), '" + Password + "', 0, "+
                              "'NA', 0, 0, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE())";

            
            string insertRole = "Insert into [dbo].[aspnet_UsersInRoles] (UserId, RoleId) select u.UserId,r.RoleId from[aspnet_Users] u inner join[aspnet_Roles] r on u.ApplicationId = r.ApplicationId " +
                                "inner join [dbo].[aspnet_Applications] a on r.ApplicationId=a.ApplicationId where u.[UserName] = '" + Username + "' and a.[ApplicationName] = 'UserApplication' " +
                                "and r.RoleName= '"+Role+"'";


           

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdForUN = new SqlCommand(insertUserName, con);
                    con.Open();
                    cmdForUN.ExecuteReader();
                    
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdForPW = new SqlCommand(insertPassword, con);
                    con.Open();
                    cmdForPW.ExecuteReader();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdForRole = new SqlCommand(insertRole, con);
                    con.Open();
                    cmdForRole.ExecuteReader();
                }
                return true;
            }

            catch (Exception e)
            {
                throw e;

            }
        }


        //public bool ValidateUser(string Username, string Password)
        //{

        //    string unm = Username;
        //    string pwd = Password;
        //    string usernameQuery = "select count([UserName]) from aspnet_Users where UserName = '" + Username + "'";
        //    string passwordQuery = "select count([Password]) from aspnet_Membership  where Password = '" + Password + "'";

        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {

        //            //UserModel userr = new UserModel();
        //            //string login_Query = "select [UserName] from aspnet_Users where UserName = '" + Username + "'";
        //            SqlCommand cmd = new SqlCommand(usernameQuery, con);
        //            con.Open();
        //            int userRowCount = (int)cmd.ExecuteScalar();
        //            if (userRowCount == 1)
        //            {
        //                userStatus = true;
        //            }
        //        }

        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {

        //            //UserModel userr = new UserModel();
        //            //string login_Query = "select [UserName] from aspnet_Users where UserName = '" + Username + "'";
        //            SqlCommand cmd = new SqlCommand(passwordQuery, con);
        //            con.Open();
        //            int pwdRowCount = (int)cmd.ExecuteScalar();
        //            if (pwdRowCount == 1)
        //            {
        //                pwdStatus = true;
        //            }
        //        }

        //        if (userStatus == true && pwdStatus == true)

        //            return true;

        //        else
        //            return false;
        //    }

        //    catch(Exception e)
        //    {
        //        throw e;
        //    }

        //}

        
        /// <summary>
        /// Method for User login
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int LoginUser(string Username, string Password)
        {
            string userIdCountQuery = "select count(m.[UserId]) from[dbo].[aspnet_Users] u inner join[dbo].[aspnet_Membership] m" +
                                      "on u.UserId = m.UserId" +
                                      "where m.[Password] = '" + Password + "' and u.[UserName] = '" + Username + "'" +
                                      "and u.Applicationid = (select ApplicationId from aspnet_Applications where [ApplicationName] = 'UserApplication')";


            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    SqlCommand cmd1 = new SqlCommand(userIdCountQuery, con);
                    //SqlCommand cmd2 = new SqlCommand(passwordQuery, con);

                    con.Open();

                    int userIdRowCount = (int)cmd1.ExecuteScalar();
                    //int pwdRowCount = (int)cmd2.ExecuteScalar();

                    //valid user credentials
                    if (userIdRowCount == 1)
                    {
                       
                        return 1;
                    }

                    //invalid credentials
                    else
                    {
                        return 0;
                    }

                   
                }
            }

            catch (Exception e)
            {
                throw e;
            }

        }

     /// <summary>
     /// Updates current Username with new username
     /// </summary>
     /// <param name="currentUsername"></param>
     /// <param name="newUsername"></param>
     /// <returns></returns>
    public bool UpdateUserName(string currentUsername, string newUsername)
    {
            string updateUserNameQuery = "update[dbo].[aspnet_Users]" +
                                         "set [UserName] = '" + newUsername + "', [LoweredUserName] = lower('" + newUsername + "')" +
                                         "where [ApplicationId] = (select [ApplicationId] from [dbo].[aspnet_Applications] " +
                                         "where [ApplicationName]='UserApplication')" +
                                         "and [UserName] = '" + currentUsername + "'";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdUpdaterUsername = new SqlCommand(updateUserNameQuery, con);
                    con.Open();
                    int rowUpdateCount = cmdUpdaterUsername.ExecuteNonQuery();
                    if (rowUpdateCount != 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }

            catch (Exception e)
            {
                throw e;
            }
            //int a = NewCmd.ExecuteNonQuery();

            //string
            //throw new NotImplementedException();
    }


    /// <summary>
    /// Updates current User role with new user role
    /// </summary>
    /// <param name="currentUsername"></param>
    /// <param name="currentRole"></param>
    /// <param name="newRole"></param>
    /// <returns></returns>
    public bool UpdateUserRole(string currentUsername, string currentRole, string newRole)
    {
            string updateUserRoleQuery = "update [dbo].[aspnet_UsersInRoles]" +
                                         "set [RoleId] = (select [RoleId] from [dbo].[aspnet_Roles] where [RoleName] = '" + newRole + "'" +
                                         " and [ApplicationId] = (select [ApplicationId] from [dbo].[aspnet_Applications] where [ApplicationName] = 'UserApplication'))" +
                                         "where [RoleId] = (select [RoleId] from [dbo].[aspnet_Roles] where [RoleName] = '" + currentRole + "' " +
                                         "and [ApplicationId] = (select [ApplicationId] from [dbo].[aspnet_Applications] where [ApplicationName] = 'UserApplication'))" +
                                         "and [UserId] = (select [UserId] from [dbo].[aspnet_Users] where [UserName] = '" + currentUsername + "')";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdUpdaterUserRole = new SqlCommand(updateUserRoleQuery, con);
                    con.Open();
                    int rowUpdateCount = cmdUpdaterUserRole.ExecuteNonQuery();
                    if (rowUpdateCount != 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }

            catch (Exception e)
            {
                throw e;
            }


            throw new NotImplementedException();
    }
     
    /// <summary>
    /// Deletes a user record
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    public bool DeleteUser(string UserName, string Password)
    {
            UserModel user = new UserModel();

            Guid userIdGuid;

            

            string userIdQuery = "select u.UserId from [dbo].[aspnet_Users] u inner join [dbo].[aspnet_Membership] m on u.UserId=m.UserId" +
                                 "where u.UserName = '" + UserName + "' and m.Password = '" + Password + "'";

           
            string deleteUserFromUsersQuery;
            string deleteUserFromMembershipQuery;
            string deleteUserFromUsersInRolesQuery;

            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdUserId = new SqlCommand(userIdQuery, con);
                    con.Open();
                    SqlDataReader dr = cmdUserId.ExecuteReader();


                    while (dr.Read())
                    {
                        // user.UserID = dr["UserId"].ToString();

                        //converting string to guid
                        userIdGuid = Guid.Parse(dr["UserId"].ToString());

                        //storing converted guid value in userid properrty which is of guid type
                        user.UserID = userIdGuid;

                    }
                    dr.Close();   

                }

                deleteUserFromUsersInRolesQuery = "delete from aspnet_UsersInRoles where UserId = '" + user.UserID + "'"; 
                deleteUserFromMembershipQuery = "delete from aspnet_Membership where UserId = '" + user.UserID + "'";
                deleteUserFromUsersQuery = "delete from aspnet_Users where UserId = '" + user.UserID + "'";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd1 = new SqlCommand(deleteUserFromUsersInRolesQuery, con);
                    con.Open();
                    cmd1.ExecuteReader();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd2 = new SqlCommand(deleteUserFromMembershipQuery, con);
                    con.Open();
                    cmd2.ExecuteReader();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd3 = new SqlCommand(deleteUserFromUsersQuery, con);
                    con.Open();
                    cmd3.ExecuteReader();
                }


                return true;
            }
           
            catch (Exception e)
            {
                throw e;
            }

    }

   

        /// <summary>
        /// Get users with a particular role corresponding to a particular application name -- check
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
    public List<string> GetUsersByRole(string Role)
    {
            string getUsersByRoleQuery = "SELECT u.UserName FROM dbo.aspnet_Users u INNER JOIN dbo.aspnet_UsersInRoles ur" +
                                         "ON u.UserId = ur.UserId INNER JOIN [dbo].[aspnet_Applications] a " +
                                         "ON u.ApplicationId = a.ApplicationId" +
                                         "WHERE  u.ApplicationId = (select ApplicationId from aspnet_Applications WHERE [ApplicationName] = 'UserApplication')" +
                                         "AND ur.RoleId in (select [RoleId] from [dbo].[aspnet_Roles] where [RoleName] = '" + Role + "')" +
                                         "ORDER BY u.UserName";

            List<string> usernameList = new List<string>();

        

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdUsers = new SqlCommand(getUsersByRoleQuery, con);
                    con.Open();
                    SqlDataReader dr = cmdUsers.ExecuteReader();

                    //Object[] username = new Object[dr.FieldCount];


                    
                    foreach (string username in usernameList)
                    {

                    }

                    while (dr.Read())
                    {
                        string username = dr["UserName"].ToString();
                        usernameList.Add(username);
                        //int count = dr.GetValues(username);
                    }

                    return usernameList;
                }
            }


             catch (Exception e)
            {
                throw e;
            }

            
        }

    public IEnumerable<UserModel> GetUsersBySearchKeyword(string searchKeyword)
    {
        throw new NotImplementedException();
    }

      
   }
}