using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Egreeting.Web.Controllers.Admin
{
    //[LogAction]
    //[RoleAuthorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class CategoriesController : BaseAdminController
    {
        private ICategoryBusiness CategoryBusiness;
        public CategoriesController(ICategoryBusiness CategoryBusiness)
        {
            this.CategoryBusiness = CategoryBusiness;
        }

        // GET: Categorys
        public IActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Category>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = CategoryBusiness.All.Where(x => x.CategoryName.Contains(search) && x.Draft != true).OrderBy(x => x.CategoryID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = CategoryBusiness.All.Count(x => x.CategoryName.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = CategoryBusiness.All.Count(x => x.Draft != true);
                listModel = CategoryBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.CategoryID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminCategoriesIndex, listModel);
        }

        // GET: Categorys/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Category Category = CategoryBusiness.Find(id);
            if (Category == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.AdminCategoriesDetails,Category);
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminCategoriesCreate);
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category Category)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Category.CategorySlug) || string.IsNullOrEmpty(Category.CategoryName))
                {
                    ModelState.AddModelError(string.Empty, "This name or slug must not be null, try with other name or slug please!");
                    return View(ViewNamesConstant.AdminCategoriesCreate, Category);
                }
                if (CategoryBusiness.AllNoTracking.Any(x => x.CategorySlug.Equals(Category.CategorySlug)))
                {
                    ModelState.AddModelError(string.Empty, "This slug had been created, try with other slug please!");
                    return View(ViewNamesConstant.AdminCategoriesCreate, Category);
                }
                CategoryBusiness.Insert(Category);
                CategoryBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminCategoriesCreate, Category);
        }

        // GET: Categorys/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Category Category = CategoryBusiness.Find(id);
            if (Category == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.AdminCategoriesEdit, Category);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category Category)
        {
            if (ModelState.IsValid)
            {
                CategoryBusiness.Update(Category);
                CategoryBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminCategoriesEdit, Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int ItemID)
        {
            var category = CategoryBusiness.Find(ItemID);
            category.Draft = true;
            CategoryBusiness.Update(category);
            CategoryBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CategoryBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
