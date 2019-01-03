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
using VDWebPortal.App_Code;

namespace VDWebPortal.Controllers
{
    public class SubCategoryMasterController : Controller
    {
        private VaishaliDairyDBEntities db = new VaishaliDairyDBEntities();

        // GET: SubCategoryMaster
        public async Task<ActionResult> Index()
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var m_SubCategoryMaster = db.M_SubCategoryMaster.Include(m => m.M_CategoryMaster);
                return View(await m_SubCategoryMaster.ToListAsync());
            }
        }

        // GET: SubCategoryMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                M_SubCategoryMaster m_SubCategoryMaster = await db.M_SubCategoryMaster.FindAsync(id);
                if (m_SubCategoryMaster == null)
                {
                    return HttpNotFound();
                }
                return View(m_SubCategoryMaster);
            }
        }

        // GET: SubCategoryMaster/Create
        public ActionResult Create()
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.CategoryID = new SelectList(db.M_CategoryMaster, "CategoryID", "CategoryName");
                return View();
            }
        }

        // POST: SubCategoryMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryID,SubCategoryName,SubCategoryDesc,Sequence")] M_SubCategoryMaster m_SubCategoryMaster)
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    m_SubCategoryMaster.CreatedBy = Session["EmailID"].ToString();
                    m_SubCategoryMaster.CreatedDate = DateTime.Now;
                    m_SubCategoryMaster.ModifiedBy = Session["EmailID"].ToString();
                    m_SubCategoryMaster.ModifiedDate = DateTime.Now;
                    m_SubCategoryMaster.Active = true;

                    db.M_SubCategoryMaster.Add(m_SubCategoryMaster);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.CategoryID = new SelectList(db.M_CategoryMaster, "CategoryID", "CategoryName", m_SubCategoryMaster.CategoryID);
                return View(m_SubCategoryMaster);
            }
        }

        // GET: SubCategoryMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                M_SubCategoryMaster m_SubCategoryMaster = await db.M_SubCategoryMaster.FindAsync(id);
                if (m_SubCategoryMaster == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryID = new SelectList(db.M_CategoryMaster, "CategoryID", "CategoryName", m_SubCategoryMaster.CategoryID);
                return View(m_SubCategoryMaster);
            }
        }

        // POST: SubCategoryMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubCategoryID,CategoryID,SubCategoryName,SubCategoryDesc,Sequence,CreatedBy,CreatedDate,Active")] M_SubCategoryMaster m_SubCategoryMaster)
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    m_SubCategoryMaster.ModifiedBy = Session["EmailID"].ToString();
                    m_SubCategoryMaster.ModifiedDate = DateTime.Now;

                    db.Entry(m_SubCategoryMaster).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryID = new SelectList(db.M_CategoryMaster, "CategoryID", "CategoryName", m_SubCategoryMaster.CategoryID);
                return View(m_SubCategoryMaster);
            }
        }

        // GET: SubCategoryMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                M_SubCategoryMaster m_SubCategoryMaster = await db.M_SubCategoryMaster.FindAsync(id);
                if (m_SubCategoryMaster == null)
                {
                    return HttpNotFound();
                }
                return View(m_SubCategoryMaster);
            }
        }

        // POST: SubCategoryMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (!CommonFunctionVD.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                M_SubCategoryMaster m_SubCategoryMaster = await db.M_SubCategoryMaster.FindAsync(id);
                db.M_SubCategoryMaster.Remove(m_SubCategoryMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
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
