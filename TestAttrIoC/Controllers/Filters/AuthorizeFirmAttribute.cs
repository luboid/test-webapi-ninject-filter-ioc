using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TestAttrIoC.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeFirmAttribute : Attribute, IDisposable
    {
        public AuthorizeFirmAttribute() 
        {
        }

        public void Dispose()
        {

        }
    }

    public class AuthorizeFirmFilterAttribute : AuthorizeAttribute, IDisposable
    {
        IFirmRepository repository;

        public AuthorizeFirmFilterAttribute(IFirmRepository repository)
        {
            Trace.TraceInformation("AuthorizeFirmFilter constructor ...");
            this.repository = repository;
        }

        bool IsUserFirm(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            /*var userId = actionContext.RequestContext.Principal.Identity.GetUserId();
            var firmId = (object)null;
            if (!actionContext.RequestContext.RouteData.Values.TryGetValue("firmId", out firmId) || string.IsNullOrWhiteSpace(firmId as string))
                return false;

            return this.repository.Exists(userId, firmId as string);*/
            Trace.TraceInformation("AuthorizeFirmFilter call #" + this.repository.GetHashCode());
            return this.repository.Exists("123", "111");
        }

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //if (base.IsAuthorized(actionContext))
            //{
            return IsUserFirm(actionContext);
            //}
            //else
            //{
            //    return false;
            //}
        }

        public void Dispose()
        {
            Trace.TraceInformation("AuthorizeFirmFilter was disposed...");
        }
    }
}