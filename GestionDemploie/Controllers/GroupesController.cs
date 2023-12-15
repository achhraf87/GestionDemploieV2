using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionDemploie;

namespace GestionDemploie.Controllers
{
    public class GroupesController : Controller
    {
        private Model1Container db = new Model1Container();


        public ActionResult Index()
        {

            int cnt = getTotaLGroup();
            ViewBag.groupe = cnt;

            int totalCount = GetTotalCount();
            ViewBag.TotalCount = totalCount;

            var groupes = db.Groupes.Include(g => g.Filiere);
            return View(groupes.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupes.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }


        public ActionResult Create()
        {
            ViewBag.Filiere_idFiliere = new SelectList(db.Filieres, "idFiliere", "NomFiliere");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idGroupe,NomGroupe,Filiere_idFiliere")] Groupe groupe)
        {
            if (ModelState.IsValid)
            {
                db.Groupes.Add(groupe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Filiere_idFiliere = new SelectList(db.Filieres, "idFiliere", "NomFiliere", groupe.Filiere_idFiliere);
            return View(groupe);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupes.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Filiere_idFiliere = new SelectList(db.Filieres, "idFiliere", "NomFiliere", groupe.Filiere_idFiliere);
            return View(groupe);
        }

        public int GetTotalCount()
        {
            int totalCount = 0;

            using (var dbContext = new Model1Container())
            {
                totalCount = dbContext.Filieres.Count();
            }

            return totalCount;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idGroupe,NomGroupe,Filiere_idFiliere")] Groupe groupe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Filiere_idFiliere = new SelectList(db.Filieres, "idFiliere", "NomFiliere", groupe.Filiere_idFiliere);
            return View(groupe);
        }

        public int getTotaLGroup()
        {
            int totalCount = 0;

            using (var dbContext = new Model1Container())
            {
                totalCount = dbContext.Groupes.Count();
            }
            return totalCount;
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupes.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groupe groupe = db.Groupes.Find(id);
            db.Groupes.Remove(groupe);
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
