using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;

namespace Egreeting.Web.Controllers.API
{
    public class ApiEgreetingRolesController : ApiController
    {
        private EgreetingContext db = new EgreetingContext();

        // GET: api/EgreetingRoles
        public IQueryable<EgreetingRole> GetEgreetingRoles()
        {
            return db.EgreetingRoles;
        }

        // GET: api/EgreetingRoles/5
        [ResponseType(typeof(EgreetingRole))]
        public IHttpActionResult GetEgreetingRole(int id)
        {
            EgreetingRole egreetingRole = db.EgreetingRoles.Find(id);
            if (egreetingRole == null)
            {
                return NotFound();
            }

            return Ok(egreetingRole);
        }

        // PUT: api/EgreetingRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEgreetingRole(int id, EgreetingRole egreetingRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != egreetingRole.EgreetingRoleID)
            {
                return BadRequest();
            }

            db.Entry(egreetingRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EgreetingRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EgreetingRoles
        [ResponseType(typeof(EgreetingRole))]
        public IHttpActionResult PostEgreetingRole(EgreetingRole egreetingRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EgreetingRoles.Add(egreetingRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = egreetingRole.EgreetingRoleID }, egreetingRole);
        }

        // DELETE: api/EgreetingRoles/5
        [ResponseType(typeof(EgreetingRole))]
        public IHttpActionResult DeleteEgreetingRole(int id)
        {
            EgreetingRole egreetingRole = db.EgreetingRoles.Find(id);
            if (egreetingRole == null)
            {
                return NotFound();
            }

            db.EgreetingRoles.Remove(egreetingRole);
            db.SaveChanges();

            return Ok(egreetingRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EgreetingRoleExists(int id)
        {
            return db.EgreetingRoles.Count(e => e.EgreetingRoleID == id) > 0;
        }
    }
}