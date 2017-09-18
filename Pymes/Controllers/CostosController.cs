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
    public class CostosController : Controller
    {
        private DefaultConnection2 db = new DefaultConnection2();

        // GET: /Costos/
        public async Task<ActionResult> Index()
        {
            var costoproduccion = db.CostoProduccion.Include(c => c.Producto).Include(c => c.MateriaPrima);
            return View(await costoproduccion.ToListAsync());
        }

        // GET: /Costos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostoProduccion costoproduccion = await db.CostoProduccion.FindAsync(id);
            if (costoproduccion == null)
            {
                return HttpNotFound();
            }
            return View(costoproduccion);
        }

        // GET: /Costos/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto");
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima");
            return View();
        }

        // POST: /Costos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdCosto,IdProducto,IdMateriaPrima,Cantidad,PagoManoObra,ValorUnidad,PreciodeVenta")] CostoProduccion costoproduccion)
        {
            if (ModelState.IsValid)
            {
                db.CostoProduccion.Add(costoproduccion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", costoproduccion.IdProducto);
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", costoproduccion.IdMateriaPrima);
            return View(costoproduccion);
        }

        // GET: /Costos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostoProduccion costoproduccion = await db.CostoProduccion.FindAsync(id);
            if (costoproduccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", costoproduccion.IdProducto);
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", costoproduccion.IdMateriaPrima);
            return View(costoproduccion);
        }

        // POST: /Costos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdCosto,IdProducto,IdMateriaPrima,Cantidad,PagoManoObra,ValorUnidad,PreciodeVenta")] CostoProduccion costoproduccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costoproduccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", costoproduccion.IdProducto);
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", costoproduccion.IdMateriaPrima);
            return View(costoproduccion);
        }

        // GET: /Costos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CostoProduccion costoproduccion = await db.CostoProduccion.FindAsync(id);
            if (costoproduccion == null)
            {
                return HttpNotFound();
            }
            return View(costoproduccion);
        }

        // POST: /Costos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CostoProduccion costoproduccion = await db.CostoProduccion.FindAsync(id);
            db.CostoProduccion.Remove(costoproduccion);
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
