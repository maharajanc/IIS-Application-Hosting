using mpex.deployment.web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace mpex.deployment.web.Filters
{
    public class ServerSelectFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = filterContext.HttpContext;

            //if server == null => set default server
            if (ctx.Session["Server"] == null)
            {
                List<string> objStrServer = System.Web.Configuration.WebConfigurationManager.AppSettings["servers"].ToString().Split(',').ToList();
                IList<Server> objServer = new List<Server>();
                if (objStrServer != null && objStrServer.Count > 0)
                {
                    foreach (string Obj in objStrServer)
                    {
                        int id = 0;
                        objServer.Add(new Server() { ServerId = Int32.TryParse(Obj.Split(';')[1], out id) ? id : id, ServerName = Obj.Split(';')[0] });
                    }
                }

                ctx.Session["Server"] = objServer.First();
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }


    }
}