using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserManagement.Repository;

namespace UserManagement.Controllers
{
    public class LoginController : ApiController
    {
        IUserRepository _irepository;

        public LoginController(IUserRepository irepository)
        {
            _irepository = irepository;
        }

        [Route("UserLogin/{username}/{password}")]
        [HttpGet]
        public IHttpActionResult UserLogin(string username, string password)
        {
            int validationStatus = _irepository.LoginUser(username, password);

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
    }
}
