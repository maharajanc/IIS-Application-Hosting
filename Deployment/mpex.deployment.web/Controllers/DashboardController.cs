using mpex.deployment.web.Models;
using mpex.deployment.web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mpex.deployment.web.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext dbo = null;
        public ApplicationDbContext db
        {
            get
            {
                if (Session["Server"] == null)
                {
                    if (dbo == null)
                    {
                        dbo = new ApplicationDbContext();
                    }
                }
                else
                {
                    if (dbo == null)
                    {

                        dbo = new ApplicationDbContext(((Server)Session["Server"]).ServerName);
                    }
                }

                return dbo;
            }
        }

        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveSession(Server s)
        {
            if (s != null)
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
                Session["Server"] = objServer.FirstOrDefault(i => i.ServerId == s.ServerId);
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }

        }


        public JsonResult GetReport(Calender parm)
        {
            DateTime startDate = Convert.ToDateTime(parm.start);
            DateTime endDate = Convert.ToDateTime(parm.end);
            List<Calender> objCalender = new List<Calender>();

            try
            {
                var MultiFileDeployments = db.MultiFileDeployments
                                           .Where(dt => dt.updateDate >= startDate && dt.updateDate <= endDate).ToList()
                                           .GroupBy(d => d.updateDate)
                                           .Select(i => i.FirstOrDefault());
                if (MultiFileDeployments.Count() > 0)
                {
                    foreach (MultiFileDeployment item in MultiFileDeployments)
                    {
                        objCalender.Add(new Calender()
                        {
                            start = item.updateDate.ToString("yyyy-MM-dd"),
                            end = item.updateDate.ToString("yyyy-MM-dd"),
                            color = "#4284f4",
                            title = String.Format("IIS ({0}  Client)", item.fkClientIdList.Split(',').Count()),
                            url = Url.Action("IIS", "Reports")
                        });
                    }
                }

                  var ScriptFiles = db.ScriptFiles
                                           .Where(dt => dt.UpdateDate >= startDate && dt.UpdateDate <= endDate).ToList()
                                           .GroupBy(d => d.UpdateDate)
                                           .Select(i => i.FirstOrDefault());
                  if (ScriptFiles.Count() > 0)
                  {
                      foreach (ScriptFile item in ScriptFiles)
                      {
                          objCalender.Add(new Calender()
                          {
                              start = item.UpdateDate.ToString("yyyy-MM-dd"),
                              end = item.UpdateDate.ToString("yyyy-MM-dd"),
                              color = "#ea4335",
                              title = String.Format("DB ({0}  Client)", item.fkClientIdList.Split(',').Count()),
                              url = Url.Action("ErrorsOnScriptFile", "Reports", new { id = item.id })
                          });
                      }
                  }


                return Json(objCalender);

            }
            catch (Exception e)
            {
                return Json(new { e.Message });
            }

        }
    }
}