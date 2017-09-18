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
    public class PagoMOController : Controller
    {
        private DefaultConnection2 db = new DefaultConnection2();

        // GET: /PagoMO/
        public async Task<ActionResult> Index()
        {
            var pagomanoobra = db.PagoManoObra.Include(p => p.Persona);
            return View(await pagomanoobra.ToListAsync());
        }

        // GET: /PagoMO/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoManoObra pagomanoobra = await db.PagoManoObra.FindAsync(id);
            if (pagomanoobra == null)
            {
                return HttpNotFound();
            }
            return View(pagomanoobra);
        }

        // GET: /PagoMO/Create
        public ActionResult Create()
        {
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            return View();
        }

        // POST: /PagoMO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdPagoManoObra,IdPersona,HorasTrabajadas,PagoXHora,Total")] PagoManoObra pagomanoobra)
        {
            if (ModelState.IsValid)
            {
                db.PagoManoObra.Add(pagomanoobra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", pagomanoobra.IdPersona);
            return View(pagomanoobra);
        }

        // GET: /PagoMO/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoManoObra pagomanoobra = await db.PagoManoObra.FindAsync(id);
            if (pagomanoobra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", pagomanoobra.IdPersona);
            return View(pagomanoobra);
        }

        // POST: /PagoMO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdPagoManoObra,IdPersona,HorasTrabajadas,PagoXHora,Total")] PagoManoObra pagomanoobra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagomanoobra).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", pagomanoobra.IdPersona);
            return View(pagomanoobra);
        }

        // GET: /PagoMO/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoManoObra pagomanoobra = await db.PagoManoObra.FindAsync(id);
            if (pagomanoobra == null)
            {
                return HttpNotFound();
            }
            return View(pagomanoobra);
        }

        // POST: /PagoMO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PagoManoObra pagomanoobra = await db.PagoManoObra.FindAsync(id);
            db.PagoManoObra.Remove(pagomanoobra);
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
