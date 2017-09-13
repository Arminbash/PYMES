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
    public class DetalleController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: /Detalle/
        public async Task<ActionResult> Index()
        {
            var detalleventa = db.DetalleVenta.Include(d => d.Producto).Include(d => d.Venta);
            return View(await detalleventa.ToListAsync());
        }

        // GET: /Detalle/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleventa = await db.DetalleVenta.FindAsync(id);
            if (detalleventa == null)
            {
                return HttpNotFound();
            }
            return View(detalleventa);
        }

        // GET: /Detalle/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto");
            ViewBag.IdVenta = new SelectList(db.Venta, "IdVenta", "Direccion");
            return View();
        }

        // POST: /Detalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdDetalleVenta,IdProducto,IdVenta")] DetalleVenta detalleventa)
        {
            if (ModelState.IsValid)
            {
                db.DetalleVenta.Add(detalleventa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detalleventa.IdProducto);
            ViewBag.IdVenta = new SelectList(db.Venta, "IdVenta", "Direccion", detalleventa.IdVenta);
            return View(detalleventa);
        }

        // GET: /Detalle/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleventa = await db.DetalleVenta.FindAsync(id);
            if (detalleventa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detalleventa.IdProducto);
            ViewBag.IdVenta = new SelectList(db.Venta, "IdVenta", "Direccion", detalleventa.IdVenta);
            return View(detalleventa);
        }

        // POST: /Detalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdDetalleVenta,IdProducto,IdVenta")] DetalleVenta detalleventa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleventa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detalleventa.IdProducto);
            ViewBag.IdVenta = new SelectList(db.Venta, "IdVenta", "Direccion", detalleventa.IdVenta);
            return View(detalleventa);
        }

        // GET: /Detalle/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleventa = await db.DetalleVenta.FindAsync(id);
            if (detalleventa == null)
            {
                return HttpNotFound();
            }
            return View(detalleventa);
        }

        // POST: /Detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DetalleVenta detalleventa = await db.DetalleVenta.FindAsync(id);
            db.DetalleVenta.Remove(detalleventa);
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
