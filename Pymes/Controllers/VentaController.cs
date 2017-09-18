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
    public class VentaController : Controller
    {
        private DefaultConnection2 db = new DefaultConnection2();

        // GET: /Venta/
        public async Task<ActionResult> Index()
        {
            var venta = db.Venta.Include(v => v.Persona).Include(v => v.Usuario);
            return View(await venta.ToListAsync());
        }

        // GET: /Venta/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = await db.Venta.FindAsync(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: /Venta/Create
        public ActionResult Create()
        {
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUser");
            return View();
        }

        // POST: /Venta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdVenta,IdPersona,IdUsuario,Fecha,Direccion,CantidaPedita,Total,activo")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", venta.IdPersona);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUser", venta.IdUsuario);
            return View(venta);
        }

        // GET: /Venta/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = await db.Venta.FindAsync(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", venta.IdPersona);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUser", venta.IdUsuario);
            return View(venta);
        }

        // POST: /Venta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdVenta,IdPersona,IdUsuario,Fecha,Direccion,CantidaPedita,Total,activo")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", venta.IdPersona);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUser", venta.IdUsuario);
            return View(venta);
        }

        // GET: /Venta/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = await db.Venta.FindAsync(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: /Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venta venta = await db.Venta.FindAsync(id);
            db.Venta.Remove(venta);
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
