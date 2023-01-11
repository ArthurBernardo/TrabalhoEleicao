using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoEleicao.Entities.Context;
using TrabalhoEleicao.Entities.Models;

namespace TrabalhoEleicao.Controllers
{
    public class PartidoesController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            var partidos = db.Partidos.ToList();
            return View(partidos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Sigla,Numero,Nome")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Partidos.Add(partido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partido);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sigla,Numero,Nome")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partido);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partidos.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partido partido = db.Partidos.Find(id);
            db.Partidos.Remove(partido);
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
