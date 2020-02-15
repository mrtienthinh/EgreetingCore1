using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using System.IO;
using Egreeting.Models.AppContext;
using Egreeting.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Egreeting.Web.Controllers.Admin
{
    //[LogAction]
    //[RoleAuthorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class EcardsController : BaseAdminController
    {
        private IEcardBusiness EcardBusiness;
        private ICategoryBusiness CategoryBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public EcardsController(IEcardBusiness EcardBusiness, ICategoryBusiness CategoryBusiness, IEgreetingUserBusiness EgreetingUserBusiness, IWebHostEnvironment webHostEnvironment)
        {
            this.EcardBusiness = EcardBusiness;
            this.CategoryBusiness = CategoryBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Ecards
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Ecard>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = EcardBusiness.All.Where(x => x.EcardName.Contains(search) && x.Draft != true).OrderBy(x => x.EcardID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = EcardBusiness.All.Count(x => x.EcardName.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = EcardBusiness.All.Count(x => x.Draft != true);
                listModel = EcardBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.EcardID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminEcardsIndex, listModel);
        }

        // GET: Ecards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.AdminEcardsDetails, ecard);
        }

        // GET: Ecards/Create
        public ActionResult Create()
        {
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsCreate);
        }

        // POST: Ecards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ecard ecard, string ListCategoryString)
        {
            if (ModelState.IsValid)
            {
                var thumbnailFile = Request.Form.Files["Thumbnail"];
                var ecardFile = Request.Form.Files["EcardFile"];
                string pathThumbnail = $"{_webHostEnvironment.WebRootPath}Uploads/Thumbnails/";
                string pathEcardFiles = $"{_webHostEnvironment.WebRootPath}Uploads/EcardFiles/";

                if (!Directory.Exists(pathEcardFiles))
                {
                    Directory.CreateDirectory(pathEcardFiles);
                    if (!Directory.Exists(pathThumbnail))
                    {
                        Directory.CreateDirectory(pathThumbnail);
                    }
                }


                if (ecardFile != null)
                {
                    if (!Path.GetExtension(ecardFile.FileName).CheckExtentionFile(ecard.EcardType))
                    {
                        ModelState.AddModelError(string.Empty, "File upload not support!");
                        ViewBag.Categories = CategoryBusiness.All.ToList();
                        return View(ViewNamesConstant.AdminEcardsCreate, ecard);
                    }
                    ecard.EcardUrl = "EcardUrl_" + DateTime.Now.ToFileTime() + Path.GetExtension(ecardFile.FileName);
                    using (var fileStream = new FileStream(pathEcardFiles + ecard.EcardUrl, FileMode.Create))
                    {
                        ecardFile.CopyTo(fileStream);
                    }

                    if (thumbnailFile != null && Path.GetExtension(thumbnailFile.FileName).CheckExtentionFile())
                    {
                        ecard.ThumbnailUrl = "Thumbnail_" + DateTime.Now.ToFileTime() + Path.GetExtension(thumbnailFile.FileName);
                        using (var fileStream = new FileStream(pathThumbnail + ecard.ThumbnailUrl, FileMode.Create))
                        {
                            thumbnailFile.CopyTo(fileStream);
                        }

                    }
                    else
                    {
                        ecard.ThumbnailUrl = "thumbnail.png";
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is no is ecard file found!");
                    ViewBag.Categories = CategoryBusiness.All.ToList();
                    return View(ViewNamesConstant.AdminEcardsCreate, ecard);
                }

                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    var lstCategoryID = ListCategoryString.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                    var lstCategory = context.Set<Category>().Where(x => lstCategoryID.Contains(x.CategoryID)).Select(x => x.CategoryEcards).ToList();
                    ecard.CategoryEcards = (ICollection<CategoryEcard>)lstCategory;
                    if (context.Set<Ecard>().Any(x => ecard.EcardSlug.Equals(x.EcardSlug)))
                    {
                        ecard.EcardSlug = ecard.EcardSlug + DateTime.Now.ToFileTime();
                    }

                    if (HttpContext.User != null && User.Identity.IsAuthenticated)
                    {
                        string email = "";
                        email = User.Identity.Name;
                        var user = context.Set<EgreetingUser>().Where(x => x.Email.Equals(email)).FirstOrDefault();
                        ecard.EgreetingUser = user;
                    }
                    ecard.CreatedDate = DateTime.Now;
                    context.Set<Ecard>().Add(ecard);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsCreate, ecard);
        }

        // GET: Ecards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsEdit, ecard);
        }

        // POST: Ecards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ecard ecard)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    var ecardUpdate = context.Set<Ecard>().Find(ecard.EcardID);

                    var thumbnailFile = Request.Form.Files["Thumbnail"];
                    var ecardFile = Request.Form.Files["EcardFile"];
                    string pathThumbnail = $"{_webHostEnvironment.WebRootPath}Uploads/Thumbnails/";
                    string pathEcardFiles = $"{_webHostEnvironment.WebRootPath}Uploads/EcardFiles/";
                    if (!Directory.Exists(pathEcardFiles))
                    {
                        Directory.CreateDirectory(pathEcardFiles);
                        if (!Directory.Exists(pathThumbnail))
                        {
                            Directory.CreateDirectory(pathThumbnail);
                        }
                    }

                    if (ecardFile != null)
                    {
                        if (!Path.GetExtension(ecardFile.FileName).CheckExtentionFile(ecard.EcardType))
                        {
                            ModelState.AddModelError(string.Empty, "File upload not support!");
                            ViewBag.Categories = CategoryBusiness.All.ToList();
                            return View(ViewNamesConstant.AdminEcardsCreate, ecard);
                        }
                        ecard.EcardUrl = "EcardUrl_" + DateTime.Now.ToFileTime() + Path.GetExtension(ecardFile.FileName);
                        using (var fileStream = new FileStream(pathEcardFiles + ecard.EcardUrl, FileMode.Create))
                        {
                            ecardFile.CopyTo(fileStream);
                        }

                        if (thumbnailFile != null && Path.GetExtension(thumbnailFile.FileName).CheckExtentionFile())
                        {
                            ecard.ThumbnailUrl = "Thumbnail_" + DateTime.Now.ToFileTime() + Path.GetExtension(thumbnailFile.FileName);
                            using (var fileStream = new FileStream(pathThumbnail + ecard.ThumbnailUrl, FileMode.Create))
                            {
                                thumbnailFile.CopyTo(fileStream);
                            }
                        }
                        else
                        {
                            ecard.ThumbnailUrl = "thumbnail.png";
                        }
                    }

                    ecardUpdate.EcardName = ecard.EcardName;
                    if (!string.IsNullOrEmpty(ecard.EcardSlug))
                    {
                        if (!context.Set<Ecard>().Any(x => ecard.EcardSlug.Equals(x.EcardSlug)))
                        {
                            ecardUpdate.EcardSlug = ecard.EcardSlug;
                        }
                        else
                        {
                            ecardUpdate.EcardSlug = ecard.EcardSlug + DateTime.Now.ToFileTime();
                        }
                    }
                    ecardUpdate.EcardType = ecard.EcardType;
                    ecardUpdate.Price = ecard.Price;
                    ecardUpdate.ModifiedDate = DateTime.Now;

                    context.Set<Ecard>().Attach(ecardUpdate);
                    context.Entry(ecardUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsEdit, ecard);
        }

        // POST: Ecards/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            Ecard ecard = EcardBusiness.Find(ItemID);
            ecard.Draft = true;
            EcardBusiness.Update(ecard);
            EcardBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EcardBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}