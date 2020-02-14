using System.Linq;
using System.Net;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    //[LogAction]
    public class EgreetingRolesController : BaseController
    {
        private IEgreetingRoleBusiness EgreetingRoleBusiness;
        public EgreetingRolesController(IEgreetingRoleBusiness EgreetingRoleBusiness)
        {
            this.EgreetingRoleBusiness = EgreetingRoleBusiness;
        }

        // GET: EgreetingRoles
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendEgreetingRolesIndex, EgreetingRoleBusiness.All.ToList());
        }

        // GET: EgreetingRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            if (egreetingRole == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.FrontendEgreetingRolesDetails, egreetingRole);
        }

        // GET: EgreetingRoles/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.FrontendEgreetingRolesCreate);
        }

        // POST: EgreetingRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EgreetingRole egreetingRole)
        {
            if (ModelState.IsValid)
            {
                EgreetingRoleBusiness.Insert(egreetingRole);
                EgreetingRoleBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.FrontendEgreetingRolesCreate, egreetingRole);
        }

        // GET: EgreetingRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            if (egreetingRole == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.FrontendEgreetingRolesEdit, egreetingRole);
        }

        // POST: EgreetingRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EgreetingRole egreetingRole)
        {
            if (ModelState.IsValid)
            {
                EgreetingRoleBusiness.Update(egreetingRole);
                EgreetingRoleBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.FrontendEgreetingRolesEdit, egreetingRole);
        }

        // GET: EgreetingRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            if (egreetingRole == null)
            {
                return View(ViewNamesConstant.FrontendHomeError);
            }
            return View(ViewNamesConstant.FrontendEgreetingRolesDelete, egreetingRole);
        }

        // POST: EgreetingRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            EgreetingRoleBusiness.Delete(egreetingRole);
            EgreetingRoleBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EgreetingRoleBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
