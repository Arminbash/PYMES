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
    public class MateriaPrimaController : Controller
    {
        private DefaultConnection2 db = new DefaultConnection2();

        // GET: /MateriaPrima/
        public async Task<ActionResult> Index()
        {
            var materiaprima = db.MateriaPrima.Include(m => m.Proveedor);
            return View(await materiaprima.ToListAsync());
        }

        // GET: /MateriaPrima/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaPrima materiaprima = await db.MateriaPrima.FindAsync(id);
            if (materiaprima == null)
            {
                return HttpNotFound();
            }
            return View(materiaprima);
        }

        // GET: /MateriaPrima/Create
        public ActionResult Create()
        {
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres");
            return View();
        }

        // POST: /MateriaPrima/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="IdMatriaPrima,IdProveedor,NombreMateriaPrima,PrecioXUnidad,Descripcion,activo")] MateriaPrima materiaprima)
        {
            if (ModelState.IsValid)
            {
                db.MateriaPrima.Add(materiaprima);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres", materiaprima.IdProveedor);
            return View(materiaprima);
        }

        // GET: /MateriaPrima/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaPrima materiaprima = await db.MateriaPrima.FindAsync(id);
            if (materiaprima == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres", materiaprima.IdProveedor);
            return View(materiaprima);
        }

        // POST: /MateriaPrima/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="IdMatriaPrima,IdProveedor,NombreMateriaPrima,PrecioXUnidad,Descripcion,activo")] MateriaPrima materiaprima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materiaprima).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdProveedor = new SelectList(db.Proveedor, "IdProveedor", "Nombres", materiaprima.IdProveedor);
            return View(materiaprima);
        }

        // GET: /MateriaPrima/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaPrima materiaprima = await db.MateriaPrima.FindAsync(id);
            if (materiaprima == null)
            {
                return HttpNotFound();
            }
            return View(materiaprima);
        }

        // POST: /MateriaPrima/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MateriaPrima materiaprima = await db.MateriaPrima.FindAsync(id);
            db.MateriaPrima.Remove(materiaprima);
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
