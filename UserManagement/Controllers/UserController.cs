using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserManagement.DAL;
using UserManagement.Model;
using UserManagement.Models;
using UserManagement.Repository;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace UserManagement.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        IUserRepository _irepository;

        //ResourceManager rm = new ResourceManager("UserControllerMessages",
        //        Assembly.GetExecutingAssembly());

        public UserController(IUserRepository irepository)
        {
            _irepository = irepository;
        }
        
        //include exception handling here 
        //make a separate ctlr for login

        [Route("Create/{username}/{password}/{role}")]
        [HttpPost]

        //public IHttpActionResult Create([FromBody]string username, [FromBody]string password, [FromBody]string role)
        public IHttpActionResult Create(string username, string password, string role)
        //public IHttpActionResult UserCreation([FromBody] JObject data)
        {
            try
            {
               // UserModel user = data["Username"].ToObject<UserModel>();
               // MembershipModel membership = data["Password"].ToObject<MembershipModel>();
               // RoleModel role = data["RoleName"].ToObject<RoleModel>();

                var creationStatus = _irepository.CreateUser(username, password, role);

                if (creationStatus)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, $"User created {username} successfully."));
                    // return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, rm.GetString("creationSuccessful")));

                }

                else
                {
                    return BadRequest("User Creation failed.");

                }
            }
            catch(Exception)
            {
                //              System.Resources.MissingManifestResourceException occurred
                //HResult = 0x80131532

                //Message = Could not find any resources appropriate for the specified culture or the neutral culture.Make sure 
                //"UsingRESX.UserControllerMessages.resources" was correctly embedded or linked into assembly "UserManagement" at compile time, 
                //or that all the satellite assemblies required are loadable and fully signed.
  //  Source =< Cannot evaluate the exception source >
  //  StackTrace:

  //   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
  
  //   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark & stackMark)
  
  //   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark & stackMark)
  
  //   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
  
  //   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
  
  //   at System.Resources.ResourceManager.GetString(String name)
  
  //   at UserManagement.Controllers.UserController.UserCreation(String Username, String Password, String Role) in C:\Users\ee210680\Documents\Visual Studio 2017\Projects\UserManagement\UserManagement\Controllers\UserController.cs:line 62
  
  //   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.<> c__DisplayClass10.< GetExecutor > b__9(Object instance, Object[] methodParameters)
  
  //   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.Execute(Object instance, Object[] arguments)
  
  //   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ExecuteAsync(HttpControllerContext controllerContext, IDictionary`2 arguments, CancellationToken cancellationToken)
  

                  throw;
                //throw new InvalidOperationException("User creation");
            }

        }



        //[Route("Login/{username}/{password}")]
        //[HttpGet]
        //public IHttpActionResult Login(string username, string password)
        //{
        //    int validationStatus = _irepository.LoginUser(username, password);

        //    if (validationStatus == 1)
        //    {
        //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Login successful."));
        //    }


        //    else if (validationStatus == 0)
        //    {
        //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Sorry. Please enter the correct password."));
        //        //return BadRequest("Sorry. Invalid Username or Password.");
        //    }

        //    else
        //    {
        //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Username does not exist."));
        //    }

        //}
        
        [HttpPut]
        [Route("UpdateUserName/{currentUsername}/{newUsername}")]
        public IHttpActionResult UpdateUserName(string currentUsername, string newUsername)
        {
            bool updateUsernameStatus = _irepository.UpdateUserName(currentUsername, newUsername);

            if(updateUsernameStatus)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Username updated."));
            }
            else
            {
                return BadRequest("Username updation failed. Please retry.");
            }
        }

        [HttpPut]
        [Route("UpdateUserRole/{currentUsername}/{currentRole}/{newRole}")]
        public IHttpActionResult UpdateUserRole(string currentUsername, string currentRole, string newRole)
        {
            bool updateUserRoleStatus = _irepository.UpdateUserRole(currentUsername, currentRole, newRole);

            //how do we pass a variable value in the create respone method
            //like <User025> role has been updated successully to <HR>
            if (updateUserRoleStatus)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "User Role updated."));
            }
            else
            {
                return BadRequest("User Role updation failed. Please retry.");
            }
        }


        [Route("Delete/{UserName}")]
        [HttpDelete]
        public IHttpActionResult Delete(string userName)
        {
            bool deleteUserStatus = _irepository.DeleteUser(userName);
           // string Userid = UserId;

            if(deleteUserStatus)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,"Requested user has been deleted."));
            }

            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Requested user " + userName + " has not been deleted."));
            }

        }

        [Route("GetUsersByRole/{role}")]
        [HttpGet]
        public IHttpActionResult GetUsersByRole(string role)
        {
            IEnumerable<string> usersList = _irepository.GetUsersByRole(role);
            //Object[] username = _irepositoryGetUsersByRole(Role);
            var message = string.Format("List of users with {0} role {1}", role, usersList);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, message));

        }
    }
}
