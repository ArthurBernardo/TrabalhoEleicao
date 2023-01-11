using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.ViewModels;

namespace TrabalhoEleicao.Controllers
{
    public class EleicaoController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            var candidatos = db.Candidatos.ToList();
            var importArciveViewModel = new List<ImportArciveViewModel>();
            importArciveViewModel = candidatos.Select(x => new ImportArciveViewModel
            {
                Nome = x.Nome,
                DataNascimento = x.DataNascimento,
                TipoCandidato = x.TipoCandidato,
                Partido = x.Partido,
                VotosValidos = x.VotosValidos,
                VotosNulos = x.VotosNulos,
                VotosBrancos = x.VotosBrancos,
                Id = x.Id
            }).Where(x => x.VotosValidos.HasValue).ToList();
            return View(importArciveViewModel);
        }
    }
}