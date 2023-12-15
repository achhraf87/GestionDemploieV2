using GestionDemploie.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDemploie.RepositoryEmploie
{
    public interface IEmploieServices
    {
        List<EmploiViewModel> AfficherEmploi(string emp);
        List<emploie> getEmp();
    }
}
