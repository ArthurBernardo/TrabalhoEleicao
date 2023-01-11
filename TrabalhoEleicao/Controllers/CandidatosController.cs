using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.Entities.Models;
using TrabalhoEleicao.Services.PartidoService;

namespace TrabalhoEleicao.Controllers
{
    public class CandidatosController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            var candidatos = db.Candidatos.ToList();
            Partido partidoService;
            partidoService = new Partido(new PartidoService());
            ViewBag.Partidos = partidoService.GetPartidos();
            return View(candidatos);
        }

        public ActionResult Create()
        {
            Partido partidoService;
            partidoService = new Partido(new PartidoService());
            var partidos = partidoService.GetPartidos();
            ViewBag.Partidos = partidos.Select(t => new SelectListItem()
            {
                Text = t.Nome.ToString(),
                Value = t.Id.ToString()
            });
            return View();
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,DataNascimento,TipoCandidato,Partido")] Candidato candidato)
        {
            var partidos = db.Partidos.ToList();
            var partidoSelecionado = partidos.First(x => x.Id == int.Parse(candidato.Partido));
            candidato.Partido = partidoSelecionado.Nome;
            if (ModelState.IsValid)
            {
                db.Candidatos.Add(candidato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidato);
        }

   
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,DataNascimento,TipoCandidato,VotosValidos,VotosNulos,VotosBrancos")] Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidato);
        }

  
        public ActionResult Delete(int? id)
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

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidato candidato = db.Candidatos.Find(id);
            db.Candidatos.Remove(candidato);
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
