using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using JuntaCentral.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JuntaCentral.Controllers
{
    public class PublicacionsController : Controller
    {
        private JuntaVecinosEntities db = new JuntaVecinosEntities();



        // GET: Publicacions
        public ActionResult Index()
        {
            var publicacion = db.Publicacion.Include(p => p.Usuarios);
            return View(publicacion.ToList());
        }

        // GET: Publicacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // GET: Publicacions/Create
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Edad,Foto,Ultima_Vista,Descripcion,Comentario,Telefono,Celualar,Fecha_Publicacion,User_id")] Publicacion publicacion)
        {

            HttpPostedFileBase upload = Request.Files[0];

            WebImage image = new WebImage(upload.InputStream);
            publicacion.Foto = image.GetBytes();
            

            if (ModelState.IsValid)
            {
                
                db.Publicacion.Add(publicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_id = new SelectList(db.Usuarios, "id", "Nombre", publicacion.User_id);
            return View(publicacion);
        }

        // GET: Publicacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.Usuarios, "id", "Nombre", publicacion.User_id);
            return View(publicacion);
        }

        // POST: Publicacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Edad,Foto,Ultima_Vista,Descripcion,Comentario,Telefono,Celualar,Fecha_Publicacion,User_id")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.Usuarios, "id", "Nombre", publicacion.User_id);
            return View(publicacion);
        }

        // GET: Publicacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // POST: Publicacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicacion publicacion = db.Publicacion.Find(id);
            db.Publicacion.Remove(publicacion);
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
