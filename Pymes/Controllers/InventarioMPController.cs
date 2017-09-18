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
    public class InventarioMPController : Controller
    {
        private DefaultConnection2 db = new DefaultConnection2();

        // GET: /InventarioMP/
        public async Task<ActionResult> Index()
        {
            var inventariomateriaprima = db.InventarioMateriaPrima.Include(i => i.MateriaPrima);
            return View(await inventariomateriaprima.ToListAsync());
        }

        // GET: /InventarioMP/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioMateriaPrima inventariomateriaprima = await db.InventarioMateriaPrima.FindAsync(id);
            if (inventariomateriaprima == null)
            {
                return HttpNotFound();
            }
            return View(inventariomateriaprima);
        }

        // GET: /InventarioMP/Create
        public ActionResult Create()
        {
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima");
            return View();
        }

        // POST: /InventarioMP/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdInventarioMateriaPrima,IdMateriaPrima,UnidadMedida,ValorxUnidad,CantidaExistencia")] InventarioMateriaPrima inventariomateriaprima)
        {
            if (ModelState.IsValid)
            {
                db.InventarioMateriaPrima.Add(inventariomateriaprima);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", inventariomateriaprima.IdMateriaPrima);
            return View(inventariomateriaprima);
        }

        // GET: /InventarioMP/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioMateriaPrima inventariomateriaprima = await db.InventarioMateriaPrima.FindAsync(id);
            if (inventariomateriaprima == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", inventariomateriaprima.IdMateriaPrima);
            return View(inventariomateriaprima);
        }

        // POST: /InventarioMP/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdInventarioMateriaPrima,IdMateriaPrima,UnidadMedida,ValorxUnidad,CantidaExistencia")] InventarioMateriaPrima inventariomateriaprima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventariomateriaprima).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", inventariomateriaprima.IdMateriaPrima);
            return View(inventariomateriaprima);
        }

        // GET: /InventarioMP/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioMateriaPrima inventariomateriaprima = await db.InventarioMateriaPrima.FindAsync(id);
            if (inventariomateriaprima == null)
            {
                return HttpNotFound();
            }
            return View(inventariomateriaprima);
        }

        // POST: /InventarioMP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InventarioMateriaPrima inventariomateriaprima = await db.InventarioMateriaPrima.FindAsync(id);
            db.InventarioMateriaPrima.Remove(inventariomateriaprima);
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
