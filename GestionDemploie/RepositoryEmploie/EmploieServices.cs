using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using GestionDemploie.Models;

namespace GestionDemploie.RepositoryEmploie
{
    public class EmploieServices : IEmploieServices
    {
        Model1Container db = new Model1Container();

        public List<emploie> getEmp()
        {
            var emploies = db.emploies.Include(e => e.Formateur).Include(e => e.Module).Include(e => e.Groupe).ToList();
            return emploies;
        }

        public List<EmploiViewModel> AfficherEmploi(string emp)
        {
            var result = db.Database.SqlQuery<EmploiViewModel>("Afficher_Groupe @groupe", new SqlParameter("@groupe", emp)).ToList();
            return result;
        }
    }
}