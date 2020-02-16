using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Egreeting.Models.AppContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Egreeting.Web.Controllers.Admin
{
    //[LogAction]
    //[RoleAuthorize(Roles = "Admin")]
    [Route("admin/[controller]/[action]")]
    public class EgreetingUsersController : BaseAdminController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private IEgreetingRoleBusiness EgreetingRoleBusiness;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EgreetingUsersController(UserManager<ApplicationUser> userManager, IEgreetingUserBusiness EgreetingUserBusiness, IEgreetingRoleBusiness EgreetingRoleBusiness, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
            this.EgreetingRoleBusiness = EgreetingRoleBusiness;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: EgreetingUsers
        public ActionResult Index(string search, int page = 1, int pageSize = 10, bool draft = false)
        {
            var listModel = new List<ApplicationUser>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = _userManager.Users.Where(x => x.EgreetingUser.Draft != true).Where(x => x.Email.Contains(search)).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = _userManager.Users.Count(x => x.EgreetingUser.Draft != true && x.Email.Contains(search));
            }
            else
            {
                ViewBag.totalItem = _userManager.Users.Count(x => x.EgreetingUser.Draft != true);
                listModel = _userManager.Users.Where(x => x.EgreetingUser.Draft != true).Include(x => x.EgreetingUser).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminEgreetingUsersIndex, listModel);
        }

        // GET: EgreetingUsers/Details/5
        [Route("{id:int}")]
        public ActionResult Details(int? id)
        {
            EgreetingUser egreetingUser = _userManager.Users.Where(x => x.Id == id).Select(x => x.EgreetingUser).FirstOrDefault();
            if (egreetingUser == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersDetails,egreetingUser);
        }

        // GET: EgreetingUsers/Create
        public ActionResult Create()
        {
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersCreate);
        }

        // POST: EgreetingUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EgreetingUser egreetingUser, string ListRole)
        {
            var file = Request.Form.Files["Avatar"];
            byte[] image = new byte[file.Length];
            file.OpenReadStream().Read(image, 0, image.Length);

            if (file.Length == 0)
            {
                image = System.IO.File.ReadAllBytes($"{_webHostEnvironment.WebRootPath}/Admin/dist/img/avatar.png");
            }

            if (ModelState.IsValid)
            {
                egreetingUser.CreatedDate = DateTime.Now;
                egreetingUser.Avatar = image;
                //egreetingUser.EgreetingRoles = lstEgreetingRole;
                var applicationUser = new ApplicationUser { Email = egreetingUser.Email, UserName = egreetingUser.Email, EgreetingUser = egreetingUser };
                var result = await _userManager.CreateAsync(applicationUser, egreetingUser.Password);
                if (result.Succeeded)
                {
                    using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                    {
                        var lstRoleId = ListRole.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                        var eUser = context.Set<EgreetingUser>().Where(x => x.Email.Equals(egreetingUser.Email)).FirstOrDefault();
                        eUser.EgreetingUserRoles = (ICollection<EgreetingUserRole>) context.Set<EgreetingRole>().Where(x => lstRoleId.Contains(x.EgreetingRoleID)).Select(x => x.EgreetingUserRoles).ToList();
                        context.Set<EgreetingUser>().Attach(eUser);
                        context.Entry(eUser).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersCreate,egreetingUser);
        }

        // GET: EgreetingUsers/Edit/5
        [Route("{id:int}")]
        public ActionResult Edit(int? id)
        {
            EgreetingUser egreetingUser = _userManager.Users.Where(x => x.Id == id).Select(x => x.EgreetingUser).FirstOrDefault();
            ViewBag.UserId = id;
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            if (egreetingUser == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersEdit,egreetingUser);
        }

        // POST: EgreetingUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EgreetingUser egreetingUser, int UserId, string ListRole)
        {
            var file = Request.Form.Files["Avatar"];
            byte[] image = new byte[file.Length];
            file.OpenReadStream().Read(image, 0, image.Length);

            if (ModelState.IsValid)
            {
                var user = _userManager.Users.Where(x => x.Id == UserId).Include(x => x.EgreetingUser).FirstOrDefault();
                user.EgreetingUser.EgreetingUserSlug = egreetingUser.EgreetingUserSlug;
                user.EgreetingUser.FirstName = egreetingUser.FirstName;
                user.EgreetingUser.LastName = egreetingUser.LastName;
                user.EgreetingUser.BirthDay = egreetingUser.BirthDay;
                user.EgreetingUser.CreditCardNumber = egreetingUser.CreditCardNumber;
                user.EgreetingUser.CreditCardCVG = egreetingUser.CreditCardCVG;
                user.EgreetingUser.PaymentDueDate = egreetingUser.PaymentDueDate;
                if(file.Length > 0)
                {
                    user.EgreetingUser.Avatar = image;
                }
                if (!string.IsNullOrEmpty(egreetingUser.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, egreetingUser.Password);
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    await _userManager.UpdateAsync(user);
                }
                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    var lstRoleId = ListRole.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                    var eUser = context.Set<EgreetingUser>().Where(x => x.Email.Equals(user.Email)).FirstOrDefault();
                    eUser.EgreetingUserRoles = (ICollection<EgreetingUserRole>)context.Set<EgreetingRole>().Where(x => lstRoleId.Contains(x.EgreetingRoleID)).Select(x => x.EgreetingUserRoles).ToList();
                    context.Set<EgreetingUser>().Attach(eUser);
                    context.Entry(eUser).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersEdit,egreetingUser);
        }

        // POST: EgreetingUsers/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int? ItemID)
        {
            var user = _userManager.Users.Where(x => x.Id == ItemID).FirstOrDefault();
            
            if (user == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            user.EgreetingUser.Draft = true;
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, "delete123456Aa@");
            await _userManager.UpdateAsync(user);
            var resultUpdate = await _userManager.UpdateAsync(user);
            if (resultUpdate.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => x.Draft != true).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersDetails,user.EgreetingUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EgreetingUserBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
