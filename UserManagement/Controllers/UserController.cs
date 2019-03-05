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
      

        [Route("UserCreation/{Username}/{Password}")]
        [HttpPost]
     
        public IHttpActionResult UserCreation(string Username, string Password)
        {
            
            bool creationStatus = _irepos.CreateUser(Username, Password);

            if(creationStatus)
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
            bool validationStatus = _irepos.ValidateUser(Username, Password);

            if(validationStatus)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Login successful."));
            }

           
            else 
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Sorry. Invalid Username or Password."));
                //return BadRequest("Sorry. Invalid Username or Password.");
            }



        }
    }
}
