using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace UserManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        
        string connectionString = ConfigurationManager.ConnectionStrings["SqlConString"].ConnectionString;
        bool userStatus;
        bool pwdStatus;

        /// <summary>
        /// Method to create user. User role yet to be included 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool CreateUser(string Username, string Password)
        {
           
            string UN = Username;
            string PW = Password;
            string insertUN = "Insert Into aspnet_Users ([ApplicationId], [UserId], [UserName], [LoweredUserName], [LastActivityDate] )" +
                          "values((select[ApplicationId] from aspnet_Applications where ApplicationName = 'WhatsApp'), NEWID(), '" + Username + "', 'N/A', GETDATE())";

            string insertPW = "Insert Into aspnet_Membership ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [IsApproved], [IsLockedOut], [CreateDate]," +
                              " [LastLoginDate],[LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart],[FailedPasswordAnswerAttemptCount], " +
                              "[FailedPasswordAnswerAttemptWindowStart]) values ((select[ApplicationId] from aspnet_Users where UserName = '" + Username + "'), " +
                              "(select[UserId] from aspnet_Users where UserName = '" + Username + "'), '" + Password + "', 0, 'NA', 0, 0, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE()); ";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdForUN = new SqlCommand(insertUN, con);
                    con.Open();
                    cmdForUN.ExecuteReader();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmdForPW = new SqlCommand(insertPW, con);
                    con.Open();
                    cmdForPW.ExecuteReader();
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
        public bool ValidateUser(string Username, string Password)
        {

            string unm = Username;
            string pwd = Password;
            string usernameQuery = "select count([UserName]) from aspnet_Users where UserName = '" + Username + "'";
            string passwordQuery = "select count([Password]) from aspnet_Membership  where Password = '" + Password + "'";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    //UserModel userr = new UserModel();
                    //string login_Query = "select [UserName] from aspnet_Users where UserName = '" + Username + "'";
                    SqlCommand cmd1 = new SqlCommand(usernameQuery, con);
                    SqlCommand cmd2 = new SqlCommand(passwordQuery, con);

                    con.Open();

                    int userRowCount = (int)cmd1.ExecuteScalar();
                    int pwdRowCount = (int)cmd2.ExecuteScalar();

                    if (userRowCount == 1)
                    {
                        userStatus = true;
                    }

                    if (pwdRowCount == 1)
                    {
                        pwdStatus = true;
                    }

                    //valid user credentials
                    if (userStatus == true && pwdStatus == true)

                        return true;
                    //invalid credentials
                    else
                        return false;
                }
            }

            catch (Exception e)
            {
                throw e;
            }

        }








        //3
        public bool UpdateUser(UserModel user)
    {
            throw new NotImplementedException();
        }

   
       
    public bool Delete(int UserId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserModel> GetRoles(int UserId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserModel> GetUsersByRole(string Role)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserModel> GetUsersBySearchKeyword(string searchKeyword)
    {
        throw new NotImplementedException();
    }

      
   }
}