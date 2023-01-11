using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.Entities.Models;
using TrabalhoEleicao.Services.PartidoService;
using TrabalhoEleicao.Services.VotosService;
using TrabalhoEleicao.ViewModels;

namespace TrabalhoEleicao.Controllers
{
    public class VotosController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            var candidatos = db.Candidatos.ToList();
            Partido partidoService;
            partidoService = new Partido(new PartidoService());
            ViewBag.Partidos = partidoService.GetPartidos();

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
            }).
            ToList();                   
            return View(importArciveViewModel);
        }

        [HttpPost]
        public ActionResult CreateImportArcive(ImportArciveViewModel importArciveViewModel)
        {
            VotosService votosService;
            votosService = new VotosService(new VotosService());

            var array = System.IO.File.ReadAllLines("C:\\Users\\ArthurBernardoGuedes\\Downloads\\" + importArciveViewModel.File.FileName);
            votosService.ImportFileVotos(array);
            return RedirectToAction("Index", "Votos");
        }

        public ActionResult CreateManual(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato candidato = db.Candidatos.Find(id);
            if (candidato == null)
            {
                return HttpNotFound();
            }
            return View(candidato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManual([Bind(Include = "Id,Nome,Email,DataNascimento,TipoCandidato,Partido,VotosValidos,VotosNulos,VotosBrancos")] Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidato);
        }
    }
}

