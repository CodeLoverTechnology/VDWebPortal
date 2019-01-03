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
    public class NewsMastersController : Controller
    {
        private VaishaliDairyDBEntities db = new VaishaliDairyDBEntities();

        // GET: NewsMasters
        public async Task<ActionResult> Index()
        {
            var t_NewsMasters = db.T_NewsMasters.Include(t => t.M_SubCategoryMaster).Include(t => t.M_Master);
            return View(await t_NewsMasters.ToListAsync());
        }

        // GET: NewsMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_NewsMasters t_NewsMasters = await db.T_NewsMasters.FindAsync(id);
            if (t_NewsMasters == null)
            {
                return HttpNotFound();
            }
            return View(t_NewsMasters);
        }

        // GET: NewsMasters/Create
        public ActionResult Create()
        {
            ViewBag.NewsSubCategoryID = new SelectList(db.M_SubCategoryMaster, "SubCategoryID", "SubCategoryName");
            ViewBag.NewsType = new SelectList(db.M_Master, "MasterID", "MasterValue");
            return View();
        }

        // POST: NewsMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewsID,NewsSubCategoryID,Heading,SubHeading,BodyMessage,VideoLink,Remarks,Location,NoOfViews,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active,NewsType")] T_NewsMasters t_NewsMasters)
        {
            if (ModelState.IsValid)
            {
                db.T_NewsMasters.Add(t_NewsMasters);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NewsSubCategoryID = new SelectList(db.M_SubCategoryMaster, "SubCategoryID", "SubCategoryName", t_NewsMasters.NewsSubCategoryID);
            ViewBag.NewsType = new SelectList(db.M_Master, "MasterID", "MasterValue", t_NewsMasters.NewsType);
            return View(t_NewsMasters);
        }

        // GET: NewsMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_NewsMasters t_NewsMasters = await db.T_NewsMasters.FindAsync(id);
            if (t_NewsMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsSubCategoryID = new SelectList(db.M_SubCategoryMaster, "SubCategoryID", "SubCategoryName", t_NewsMasters.NewsSubCategoryID);
            ViewBag.NewsType = new SelectList(db.M_Master, "MasterID", "MasterValue", t_NewsMasters.NewsType);
            return View(t_NewsMasters);
        }

        // POST: NewsMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewsID,NewsSubCategoryID,Heading,SubHeading,BodyMessage,VideoLink,Remarks,Location,NoOfViews,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Active,NewsType")] T_NewsMasters t_NewsMasters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_NewsMasters).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NewsSubCategoryID = new SelectList(db.M_SubCategoryMaster, "SubCategoryID", "SubCategoryName", t_NewsMasters.NewsSubCategoryID);
            ViewBag.NewsType = new SelectList(db.M_Master, "MasterID", "MasterValue", t_NewsMasters.NewsType);
            return View(t_NewsMasters);
        }

        // GET: NewsMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_NewsMasters t_NewsMasters = await db.T_NewsMasters.FindAsync(id);
            if (t_NewsMasters == null)
            {
                return HttpNotFound();
            }
            return View(t_NewsMasters);
        }

        // POST: NewsMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_NewsMasters t_NewsMasters = await db.T_NewsMasters.FindAsync(id);
            db.T_NewsMasters.Remove(t_NewsMasters);
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
