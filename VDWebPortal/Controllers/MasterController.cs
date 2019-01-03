using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VDWebPortal.Models;

namespace VDWebPortal.Controllers
{
    public class MasterController : Controller
    {
        private VaishaliDairyDBEntities db = new VaishaliDairyDBEntities();

        // GET: Master
        public async Task<ActionResult> Index()
        {
            return View(await db.M_Master.ToListAsync());
        }

        // GET: Master/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Master m_Master = await db.M_Master.FindAsync(id);
            if (m_Master == null)
            {
                return HttpNotFound();
            }
            return View(m_Master);
        }

        // GET: Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MasterID,MasterValue,MasterTable,Sequence,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_Master m_Master)
        {
            if (ModelState.IsValid)
            {
                db.M_Master.Add(m_Master);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(m_Master);
        }

        // GET: Master/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Master m_Master = await db.M_Master.FindAsync(id);
            if (m_Master == null)
            {
                return HttpNotFound();
            }
            return View(m_Master);
        }

        // POST: Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MasterID,MasterValue,MasterTable,Sequence,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active")] M_Master m_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_Master).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(m_Master);
        }

        // GET: Master/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_Master m_Master = await db.M_Master.FindAsync(id);
            if (m_Master == null)
            {
                return HttpNotFound();
            }
            return View(m_Master);
        }

        // POST: Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            M_Master m_Master = await db.M_Master.FindAsync(id);
            db.M_Master.Remove(m_Master);
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
