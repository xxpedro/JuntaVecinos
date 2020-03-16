using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using JuntaCentral.Models;
using Microsoft.AspNet.Identity;
using WebMatrix.WebData;

namespace JuntaCentral.Controllers
{
    

    public  class UsuariosController : Controller
    {
        private JuntaVecinosEntities db = new JuntaVecinosEntities();
        User us;
        public void GetLogger()
        {
           
        }


        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.userId = User.Identity.GetUserId();
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginUser login)
        {
            
            if (ModelState.IsValid)
            {
                if (db.Usuarios.Where(m => m.Email == login.Correo && m.Pass == login.Pass && m.Rol == "admin").Count() > 0)
                {
                    Session["Nombre"] = login.Correo;
                    return RedirectToAction("Index", "Administrador");

                }
                else if (db.Usuarios.Where(m => m.Email == login.Correo && m.Pass == login.Pass && m.Rol == "usuario").Count() > 0)
                {
                    Session["correo"] = login.Correo;
                    return RedirectToAction("Index", "Publicacions");
                }
                else
                {
                    return View("Login");
                }
            }
            return View();
        }



        [HttpGet]
        public ActionResult Resgistrar()
        {

           

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resgistrar([Bind(Include = "id,Nombre,Apellido,Fecha_Nacimiento,Email,Pass,Cedula,Telefono,Celular,Rol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Rol = "usuario";
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
            }

            return View(usuarios);
        }
    }
}

