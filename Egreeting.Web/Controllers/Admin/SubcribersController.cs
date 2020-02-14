using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Models.AppContext;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Egreeting.Web.Controllers.Admin
{
    //[LogAction]
    //[RoleAuthorize(Roles = "Admin")]
    public class SubcribersController : BaseAdminController
    {
        private ApplicationUserManager _userManager;
        private ISubcriberBusiness SubcriberBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private IEgreetingRoleBusiness EgreetingRoleBusiness;
        public SubcribersController(ISubcriberBusiness SubcriberBusiness, IEgreetingUserBusiness EgreetingUserBusiness, IEgreetingRoleBusiness EgreetingRoleBusiness)
        {
            this.SubcriberBusiness = SubcriberBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
            this.EgreetingRoleBusiness = EgreetingRoleBusiness;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Subcribers
        public ActionResult Index(string search, int page = 1, int pageSize = 10, bool draft = false)
        {
            var listModel = new List<Subcriber>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = UserManager.Users.Where(x => x.EgreetingUser.Draft != true && x.EgreetingUser.Subcriber != null).Where(x => x.Email.Contains(search)).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).Select(x => x.EgreetingUser.Subcriber).ToList();
            }
            else
            {
                ViewBag.totalItem = UserManager.Users.Count(x => x.EgreetingUser.Draft != true && x.EgreetingUser.Subcriber != null);
                listModel = UserManager.Users.Where(x => x.EgreetingUser.Draft != true && x.EgreetingUser.Subcriber != null).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).Select(x => x.EgreetingUser.Subcriber).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminSubcribersIndex, listModel);
        }

        // GET: Subcribers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            Subcriber subcriber = SubcriberBusiness.Find(id);
            if (subcriber == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.AdminSubcribersDetails, subcriber);
        }

        // GET: Subcribers/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminSubcribersCreate);
        }

        // POST: Subcribers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Subcriber subcriber)
        {
            if (ModelState.IsValid)
            {
                var egreetingUser = new EgreetingUser
                {
                    CreatedDate = DateTime.Now,
                    Avatar = System.IO.File.ReadAllBytes($"{Startup.StaticHostEnvironment.WebRootPath}/Content/Admin/dist/img/avatar.png"),
                    Email = subcriber.Email,

                };
                var applicationUser = new ApplicationUser { Email = egreetingUser.Email, UserName = egreetingUser.Email, EgreetingUser = egreetingUser };
                var result = await UserManager.CreateAsync(applicationUser, "delete123456Aa");
                using (var context = new EgreetingContext())
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    userManager.AddToRole(applicationUser.Id, "Subcriber");
                }
                if (result.Succeeded)
                {
                    using (var context = new EgreetingContext())
                    {
                        var eUser = context.Set<EgreetingUser>().Where(x => x.Email.Equals(egreetingUser.Email)).FirstOrDefault();
                        eUser.EgreetingUserRoles = (ICollection<EgreetingUserRole>) context.Set<EgreetingRole>().Where(x => x.EgreetingRoleName.Equals("Subcriber")).Select(x => x.EgreetingUserRoles).ToList();
                        subcriber.EgreetingUser = eUser;
                        context.Set<Subcriber>().Attach(subcriber);
                        context.Entry(subcriber).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(ViewNamesConstant.AdminSubcribersCreate, subcriber);
        }

        // POST: Subcribers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            Subcriber subcriber = SubcriberBusiness.Find(ItemID);
            subcriber.EgreetingUser.ModifiedDate = DateTime.Now;
            subcriber.EgreetingUser.Draft = true;
            subcriber.ModifiedDate = DateTime.Now;
            subcriber.Draft = true;
            SubcriberBusiness.Update(subcriber);
            SubcriberBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SubcriberBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
