using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GestionDemploie;
using GestionDemploie.Models;
using GestionDemploie.RepositoryEmploie;
using Microsoft.Ajax.Utilities;

namespace GestionDemploie.Controllers
{
    public class EmploiesController : Controller
    {
        private Model1Container db = new Model1Container();

        private readonly IEmploieServices _emploieServices;

        public EmploiesController(IEmploieServices emploie)
        {
            _emploieServices = emploie;
        }
        public EmploiesController() : this(new EmploieServices())
        {

        }
        public ActionResult Index()
        {
            var emp = _emploieServices.getEmp();
            return View(emp);
        }


        [HttpPost]
        public List<EmploiViewModel> AfficherEmploi(string groupName)
        {
            var aff = _emploieServices.AfficherEmploi(groupName);
            return aff;
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emploie emploie = db.emploies.Find(id);
            if (emploie == null)
            {
                return HttpNotFound();
            }
            return View(emploie);
        }

        public ActionResult Create()
        {
            ViewBag.Formateur_idFormateur = new SelectList(db.Formateurs, "idFormateur", "NomFormateur");
            ViewBag.Module_idModule = new SelectList(db.Modules, "idModule", "NomModule");
            ViewBag.Groupe_idGroupe = new SelectList(db.Groupes, "idGroupe", "NomGroupe");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmploie,Jours,Heure_Debut,Heure_Fin,Formateur_idFormateur,Module_idModule,Groupe_idGroupe")] emploie emploie)
        {
            if (ModelState.IsValid)
            {
                db.emploies.Add(emploie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Formateur_idFormateur = new SelectList(db.Formateurs, "idFormateur", "NomFormateur", emploie.Formateur_idFormateur);
            ViewBag.Module_idModule = new SelectList(db.Modules, "idModule", "NomModule", emploie.Module_idModule);
            ViewBag.Groupe_idGroupe = new SelectList(db.Groupes, "idGroupe", "NomGroupe", emploie.Groupe_idGroupe);
            return View(emploie);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emploie emploie = db.emploies.Find(id);
            if (emploie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Formateur_idFormateur = new SelectList(db.Formateurs, "idFormateur", "NomFormateur", emploie.Formateur_idFormateur);
            ViewBag.Module_idModule = new SelectList(db.Modules, "idModule", "NomModule", emploie.Module_idModule);
            ViewBag.Groupe_idGroupe = new SelectList(db.Groupes, "idGroupe", "NomGroupe", emploie.Groupe_idGroupe);
            return View(emploie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmploie,Jours,Heure_Debut,Heure_Fin,Formateur_idFormateur,Module_idModule,Groupe_idGroupe")] emploie emploie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Formateur_idFormateur = new SelectList(db.Formateurs, "idFormateur", "NomFormateur", emploie.Formateur_idFormateur);
            ViewBag.Module_idModule = new SelectList(db.Modules, "idModule", "NomModule", emploie.Module_idModule);
            ViewBag.Groupe_idGroupe = new SelectList(db.Groupes, "idGroupe", "NomGroupe", emploie.Groupe_idGroupe);
            return View(emploie);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            emploie emploie = db.emploies.Find(id);
            if (emploie == null)
            {
                return HttpNotFound();
            }
            return View(emploie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            emploie emploie = db.emploies.Find(id);
            db.emploies.Remove(emploie);
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
