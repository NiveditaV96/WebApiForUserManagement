using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace UserManagement.ExceptionFilters
{
    public class UserCreationFailExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //public override void OnException(HttpActionExecutedContext context)
        //{
        //    if (context.Exception is NotImplementedException)
        //    {
        //        context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        //    }
        //}
    }
}