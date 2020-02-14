using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class CategoriesController : BaseController
    {
        private ICategoryBusiness CategoryBusiness;
        private IEcardBusiness EcardBusiness;
        public CategoriesController(ICategoryBusiness CategoryBusiness, IEcardBusiness EcardBusiness)
        {
            this.CategoryBusiness = CategoryBusiness;
            this.EcardBusiness = EcardBusiness;
        }

        [Route("Categories/{slug}")]
        public ActionResult Details(string slug, string search, int page = 1, int pageSize = 6, string sorting = "date")
        {
            var listModel = new List<Ecard>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = EcardBusiness.All.Where(x => x.CategoryEcards.Select(y => y.Category.CategorySlug).Contains(slug) && x.EcardName.Contains(search) && x.Draft != true).OrderBy(x => x.EcardID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = EcardBusiness.All.Count(x => x.CategoryEcards.Select(y => y.Category.CategorySlug).Contains(slug) && x.EcardName.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = EcardBusiness.All.Count(x => x.CategoryEcards.Select(y => y.Category.CategorySlug).Contains(slug) && x.Draft != true);
                listModel = EcardBusiness.All.Where(x => x.CategoryEcards.Select(y => y.Category.CategorySlug).Contains(slug) && x.Draft != true).OrderBy(x => x.EcardID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }


            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = listModel.Count > 0 ? search : "";
            ViewBag.sorting = sorting;
            ViewBag.categorySlug = slug;
            ViewBag.categoryName = CategoryBusiness.All.Where(x => x.CategorySlug.Equals(slug)).Select(x => x.CategoryName).FirstOrDefault();
            ViewBag.categories = CategoryBusiness.All.Where(x => x.Draft != true).Select(x => new CategoryViewModel { CategoryName = x.CategoryName, EcardsCount = x.CategoryEcards.Count, CategorySlug = x.CategorySlug }).Take(5).ToList();
            ViewBag.topEcards = EcardBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.Price).Take(12).ToList();
            return View(ViewNamesConstant.FrontendCategoriesDetails, listModel);
        }

        [Route("Categories")]
        public ActionResult IndexDefault(string search, int page = 1, int pageSize = 6, string sorting = "date")
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
            ViewBag.search = listModel.Count > 0 ? search : "";
            ViewBag.sorting = sorting;
            ViewBag.categories = CategoryBusiness.All.Where(x => x.Draft != true).Select(x => new CategoryViewModel { CategoryName = x.CategoryName, EcardsCount = x.CategoryEcards.Count, CategorySlug = x.CategorySlug }).Take(5).ToList();
            ViewBag.topEcards = EcardBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.Price).Take(12).ToList();
            return View(ViewNamesConstant.FrontendCategoriesIndex, listModel);
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
