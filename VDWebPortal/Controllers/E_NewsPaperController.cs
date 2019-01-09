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
    public class E_NewsPaperController : Controller
    {
        private VaishaliDairyDBEntities db = new VaishaliDairyDBEntities();

        // GET: E_NewsPaper
        public async Task<ActionResult> Index()
        {
            return View(await db.T_E_NewsPaper.ToListAsync());
        }

        // GET: E_NewsPaper/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_E_NewsPaper t_E_NewsPaper = await db.T_E_NewsPaper.FindAsync(id);
            if (t_E_NewsPaper == null)
            {
                return HttpNotFound();
            }
            return View(t_E_NewsPaper);
        }

        // GET: E_NewsPaper/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: E_NewsPaper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TitleNews,FileName,Remarks,Location,NoOfViews")] T_E_NewsPaper t_E_NewsPaper)
        {
            if (ModelState.IsValid)
            {
                string FullPathWithFileName1 = null;
                string FolderPathForImage1 = null;
                string FolderPath = Server.MapPath(Resources.VDResources.eNewsPathShow) + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

                if (!string.IsNullOrEmpty(Request.Files["FileName"].FileName))
                {
                    FullPathWithFileName1 = FolderPath + "\\" + Request.Files["FileName"].FileName;
                    FolderPathForImage1 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["FileName"].FileName;
                }
                if (CommonFunctionVD.IsFolderExist(FolderPath))
                {
                    if (!string.IsNullOrEmpty(Request.Files["FileName"].FileName))
                    {
                        Request.Files["FileName"].SaveAs(FullPathWithFileName1);
                        t_E_NewsPaper.FileName = FolderPathForImage1;
                    }
                }
                t_E_NewsPaper.CreatedBy = Session["EmailID"].ToString();
                t_E_NewsPaper.CreatedDate = DateTime.Now;
                t_E_NewsPaper.ModifiedBy = Session["EmailID"].ToString();
                t_E_NewsPaper.ModifiedDate = DateTime.Now;
                t_E_NewsPaper.Active = true;
                db.T_E_NewsPaper.Add(t_E_NewsPaper);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(t_E_NewsPaper);
        }

        // GET: E_NewsPaper/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_E_NewsPaper t_E_NewsPaper = await db.T_E_NewsPaper.FindAsync(id);
            if (t_E_NewsPaper == null)
            {
                return HttpNotFound();
            }
            return View(t_E_NewsPaper);
        }

        // POST: E_NewsPaper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ENewsID,TitleNews,FileName,Remarks,Location,NoOfViews,CreatedBy,CreatedDate,Active")] T_E_NewsPaper t_E_NewsPaper)
        {
            if (ModelState.IsValid)
            {
                string FullPathWithFileName1 = null;
                string FolderPathForImage1 = null;
                string FolderPath = Server.MapPath(Resources.VDResources.eNewsPathShow) + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

                if (!string.IsNullOrEmpty(Request.Files["FileName"].FileName))
                {
                    FullPathWithFileName1 = FolderPath + "\\" + Request.Files["FileName"].FileName;
                    FolderPathForImage1 = "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["FileName"].FileName;
                }
                if (CommonFunctionVD.IsFolderExist(FolderPath))
                {
                    if (!string.IsNullOrEmpty(Request.Files["FileName"].FileName))
                    {
                        Request.Files["FileName"].SaveAs(FullPathWithFileName1);
                        t_E_NewsPaper.FileName = FolderPathForImage1;
                    }
                }
                t_E_NewsPaper.ModifiedBy = Session["EmailID"].ToString();
                t_E_NewsPaper.ModifiedDate = DateTime.Now;

                db.Entry(t_E_NewsPaper).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(t_E_NewsPaper);
        }

        // GET: E_NewsPaper/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_E_NewsPaper t_E_NewsPaper = await db.T_E_NewsPaper.FindAsync(id);
            if (t_E_NewsPaper == null)
            {
                return HttpNotFound();
            }
            return View(t_E_NewsPaper);
        }

        // POST: E_NewsPaper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_E_NewsPaper t_E_NewsPaper = await db.T_E_NewsPaper.FindAsync(id);
            db.T_E_NewsPaper.Remove(t_E_NewsPaper);
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
