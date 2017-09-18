using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Pymes.Controllers
{
    public class ProveedorController : Controller
    {
        private DefaultConnection2 db = new DefaultConnection2();

        // GET: /Proveedor/
        public async Task<ActionResult> Index()
        {
            var proveedor = db.Proveedor.Include(p => p.Persona);
            return View(await proveedor.ToListAsync());
        }

        // GET: /Proveedor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = await db.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: /Proveedor/Create
        public ActionResult Create()
        {
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            return View();
        }

        // POST: /Proveedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdProveedor,IdPersona,Nombres,Correo,Telefono,activo")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Proveedor.Add(proveedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", proveedor.IdPersona);
            return View(proveedor);
        }

        // GET: /Proveedor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = await db.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", proveedor.IdPersona);
            return View(proveedor);
        }

        // POST: /Proveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdProveedor,IdPersona,Nombres,Correo,Telefono,activo")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", proveedor.IdPersona);
            return View(proveedor);
        }

        // GET: /Proveedor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = await db.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: /Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Proveedor proveedor = await db.Proveedor.FindAsync(id);
            db.Proveedor.Remove(proveedor);
            await db.SaveChangesAsync();
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
