using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TFinal53rdStreet.Models;

namespace TFinal53rdStreet.Controllers
{
    public class MusicalsController : Controller
    {
        private MusicalDB db = new MusicalDB();

        // GET: Musicals
        public ActionResult Index()
        {
            return View(db.Musical.ToList());
        }

        // GET: Musicals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musical musical = db.Musical.Find(id);
            if (musical == null)
            {
                return HttpNotFound();
            }
            return View(musical);
        }

        // GET: Musicals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Musicals/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Musical,Title,Synopsis,Director,Duration,OpeningNight,Ticket,Poster")] Musical musical)
        {
            if (ModelState.IsValid)
            {
                db.Musical.Add(musical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musical);
        }

        // GET: Musicals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musical musical = db.Musical.Find(id);
            if (musical == null)
            {
                return HttpNotFound();
            }
            return View(musical);
        }

        // POST: Musicals/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Musical,Title,Synopsis,Director,Duration,OpeningNight,Ticket,Poster")] Musical musical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musical);
        }

        // GET: Musicals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musical musical = db.Musical.Find(id);
            if (musical == null)
            {
                return HttpNotFound();
            }
            return View(musical);
        }

        // POST: Musicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musical musical = db.Musical.Find(id);
            db.Musical.Remove(musical);
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
