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
    public class InventarioProductoController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: /InventarioProducto/
        public async Task<ActionResult> Index()
        {
            var inventarioproducto = db.InventarioProducto.Include(i => i.Producto);
            return View(await inventarioproducto.ToListAsync());
        }

        // GET: /InventarioProducto/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioProducto inventarioproducto = await db.InventarioProducto.FindAsync(id);
            if (inventarioproducto == null)
            {
                return HttpNotFound();
            }
            return View(inventarioproducto);
        }

        // GET: /InventarioProducto/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto");
            return View();
        }

        // POST: /InventarioProducto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdInventarioProcducto,IdProducto,UnidadMedida,ValorxUnidad,CantidaExistencia")] InventarioProducto inventarioproducto)
        {
            if (ModelState.IsValid)
            {
                db.InventarioProducto.Add(inventarioproducto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", inventarioproducto.IdProducto);
            return View(inventarioproducto);
        }

        // GET: /InventarioProducto/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioProducto inventarioproducto = await db.InventarioProducto.FindAsync(id);
            if (inventarioproducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", inventarioproducto.IdProducto);
            return View(inventarioproducto);
        }

        // POST: /InventarioProducto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdInventarioProcducto,IdProducto,UnidadMedida,ValorxUnidad,CantidaExistencia")] InventarioProducto inventarioproducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventarioproducto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", inventarioproducto.IdProducto);
            return View(inventarioproducto);
        }

        // GET: /InventarioProducto/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioProducto inventarioproducto = await db.InventarioProducto.FindAsync(id);
            if (inventarioproducto == null)
            {
                return HttpNotFound();
            }
            return View(inventarioproducto);
        }

        // POST: /InventarioProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InventarioProducto inventarioproducto = await db.InventarioProducto.FindAsync(id);
            db.InventarioProducto.Remove(inventarioproducto);
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
