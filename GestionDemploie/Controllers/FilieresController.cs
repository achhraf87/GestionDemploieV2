using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GestionDemploie;

namespace GestionDemploie.Controllers
{
    public class FilieresController : Controller
    {
        private Model1Container db = new Model1Container();
        public ActionResult Index()
        {
            int totalCount = GetTotalCount();
            ViewBag.TotalCount = totalCount;         
            var Filieres = db.Filieres.ToList();
            return View(Filieres);
        }


        private Task<bool> VerifiFiliére(string fil)
        {
            var check = db.Filieres.AnyAsync(e=>e.NomFiliere == fil);
            return check;
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filiere filiere = db.Filieres.Find(id);
            if (filiere == null)
            {
                return HttpNotFound();
            }
            return View(filiere);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idFiliere,NomFiliere")] Filiere filiere)
        {

            try
            {
                if(await VerifiFiliére(filiere.NomFiliere))
                {
                    string msg = $"{filiere.NomFiliere} "+" Et deja Existe ".ToString().Trim();
                    ViewBag.msg = msg;
                }

                else if(ModelState.IsValid)
                {
                        db.Filieres.Add(filiere);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
            } 
            catch
            {
                Exception ex = new Exception();
                ex.Message.ToString();

            }
            return View(filiere);
        }

        public  int  GetTotalCount()
        {
            int totalCount = 0;

            using (var dbContext = new Model1Container())
            {
                totalCount = dbContext.Filieres.Count();
            }

            return totalCount;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filiere filiere = db.Filieres.Find(id);
            if (filiere == null)
            {
                return HttpNotFound();
            }
            return View(filiere);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFiliere,NomFiliere")] Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filiere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filiere);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filiere filiere = db.Filieres.Find(id);
            if (filiere == null)
            {
                return HttpNotFound();
            }
            return View(filiere);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filiere filiere = db.Filieres.Find(id);
            db.Filieres.Remove(filiere);
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
