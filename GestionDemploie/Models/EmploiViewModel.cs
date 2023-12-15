using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionDemploie.Models 
{
    public class EmploiViewModel
    {
        public ICollection<emploie> emploies = new List<emploie>();

        public List<Formateur> formateurs = new List<Formateur>();

        public List<Module> modules = new List<Module>();

        public List<Salle> salles = new List<Salle>();

        public List<Filiere> filieres = new List<Filiere>();

        public List<Groupe> groupes = new List<Groupe>();

    }
}