﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionDemploie
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Model1Container : DbContext
    {
        public Model1Container()
            : base("name=Model1Container")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Filiere> Filieres { get; set; }
        public virtual DbSet<Groupe> Groupes { get; set; }
        public virtual DbSet<Formateur> Formateurs { get; set; }
        public virtual DbSet<Salle> Salles { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<emploie> emploies { get; set; }
    }
}