using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserManagement.DAL;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagement.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        IUserRepository _irepos;
        public UserController(IUserRepository irepos)
        {
            _irepos = irepos;
        }


        [Route("UserCreation/{Username}/{Password}/{Role}")]
        [HttpPost]

        public IHttpActionResult UserCreation(string Username, string Password, string Role)
        {

            bool creationStatus = _irepos.CreateUser(Username, Password, Role);

            if (creationStatus)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, "User created successfully"));
            }

            else
            {
                return BadRequest("User Creation failed.");

            }

        }



        [Route("UserLogin/{Username}/{Password}")]
        [HttpGet]
        public IHttpActionResult UserLogin(string Username, string Password)
        {
            int validationStatus = _irepos.ValidateUser(Username, Password);

            if (validationStatus == 1)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Login successful."));
            }


            else if (validationStatus == 0)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Sorry. Please enter the correct password."));
                //return BadRequest("Sorry. Invalid Username or Password.");
            }

            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Username does not exist."));
            }




        }

        [Route("DeleteUser/{UserName}/{Password}")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(string UserName, string Password)
        {
            bool deleteUserStatus = _irepos.DeleteUser(UserName, Password);
           // string Userid = UserId;

            if(deleteUserStatus)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,"Requested user has been deleted."));
            }

            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Requested user has not been deleted."));
            }

        }
    }
}
