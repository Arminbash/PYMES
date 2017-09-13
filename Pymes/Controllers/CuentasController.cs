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
    public class CuentasController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        // GET: /Cuentas/
        public async Task<ActionResult> Index()
        {
            var cuentasxpagarproveedor = db.CuentasxPagarProveedor.Include(c => c.MateriaPrima).Include(c => c.Proveedor);
            return View(await cuentasxpagarproveedor.ToListAsync());
        }

        // GET: /Cuentas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasxPagarProveedor cuentasxpagarproveedor = await db.CuentasxPagarProveedor.FindAsync(id);
            if (cuentasxpagarproveedor == null)
            {
                return HttpNotFound();
            }
            return View(cuentasxpagarproveedor);
        }

        // GET: /Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima");
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres");
            return View();
        }

        // POST: /Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,IdProveedor,IdMateriaPrima,CantidadComprada,precio,Total")] CuentasxPagarProveedor cuentasxpagarproveedor)
        {
            if (ModelState.IsValid)
            {
                db.CuentasxPagarProveedor.Add(cuentasxpagarproveedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", cuentasxpagarproveedor.IdMateriaPrima);
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres", cuentasxpagarproveedor.IdProveedor);
            return View(cuentasxpagarproveedor);
        }

        // GET: /Cuentas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasxPagarProveedor cuentasxpagarproveedor = await db.CuentasxPagarProveedor.FindAsync(id);
            if (cuentasxpagarproveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", cuentasxpagarproveedor.IdMateriaPrima);
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres", cuentasxpagarproveedor.IdProveedor);
            return View(cuentasxpagarproveedor);
        }

        // POST: /Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,IdProveedor,IdMateriaPrima,CantidadComprada,precio,Total")] CuentasxPagarProveedor cuentasxpagarproveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentasxpagarproveedor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdMateriaPrima = new SelectList(db.MateriaPrima, "IdMatriaPrima", "NombreMateriaPrima", cuentasxpagarproveedor.IdMateriaPrima);
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres", cuentasxpagarproveedor.IdProveedor);
            return View(cuentasxpagarproveedor);
        }

        // GET: /Cuentas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasxPagarProveedor cuentasxpagarproveedor = await db.CuentasxPagarProveedor.FindAsync(id);
            if (cuentasxpagarproveedor == null)
            {
                return HttpNotFound();
            }
            return View(cuentasxpagarproveedor);
        }

        // POST: /Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CuentasxPagarProveedor cuentasxpagarproveedor = await db.CuentasxPagarProveedor.FindAsync(id);
            db.CuentasxPagarProveedor.Remove(cuentasxpagarproveedor);
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
