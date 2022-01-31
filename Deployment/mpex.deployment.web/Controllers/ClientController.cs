using mpex.deployment.web.Models;
using mpex.deployment.web.Services;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace mpex.deployment.web.Controllers
{
    public class ClientController : Controller
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

        // GET: /Client/ClientInformation/
        public ActionResult Index()
        {
            return View(db.Clients.ToList().OrderBy(n => n.ClientName));
        }

        // GET: /Client/ClientInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInformation clientinformation = db.Clients.Find(id);
            if (clientinformation == null)
            {
                return HttpNotFound();
            }
            return View(clientinformation);
        }

        // GET: /Client/ClientInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Client/ClientInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientName,IISFileLocation,IISPoolName,DBConnectionString,boolIsActive")] ClientInformation clientinformation)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clientinformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientinformation);
        }

        // GET: /Client/ClientInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInformation clientinformation = db.Clients.Find(id);
            if (clientinformation == null)
            {
                return HttpNotFound();
            }
            return View(clientinformation);
        }

        // POST: /Client/ClientInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientName,IISFileLocation,IISPoolName,DBConnectionString,boolIsActive")] ClientInformation clientinformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientinformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientinformation);
        }

        // GET: /Client/ClientInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInformation clientinformation = db.Clients.Find(id);
            if (clientinformation == null)
            {
                return HttpNotFound();
            }
            return View(clientinformation);
        }

        // POST: /Client/ClientInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientInformation clientinformation = db.Clients.Find(id);
            db.Clients.Remove(clientinformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}