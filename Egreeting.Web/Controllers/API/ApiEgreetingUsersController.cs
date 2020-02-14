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
    public class ApiEgreetingUsersController : ApiController
    {
        private EgreetingContext db = new EgreetingContext();

        // GET: api/EgreetingUsers
        public IQueryable<EgreetingUser> GetEgreetingUsers()
        {
            return db.EgreetingUsers;
        }

        // GET: api/EgreetingUsers/5
        [ResponseType(typeof(EgreetingUser))]
        public IHttpActionResult GetEgreetingUser(int id)
        {
            EgreetingUser egreetingUser = db.EgreetingUsers.Find(id);
            if (egreetingUser == null)
            {
                return NotFound();
            }

            return Ok(egreetingUser);
        }

        // PUT: api/EgreetingUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEgreetingUser(int id, EgreetingUser egreetingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != egreetingUser.EgreetingUserID)
            {
                return BadRequest();
            }

            db.Entry(egreetingUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EgreetingUserExists(id))
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

        // POST: api/EgreetingUsers
        [ResponseType(typeof(EgreetingUser))]
        public IHttpActionResult PostEgreetingUser(EgreetingUser egreetingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EgreetingUsers.Add(egreetingUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = egreetingUser.EgreetingUserID }, egreetingUser);
        }

        // DELETE: api/EgreetingUsers/5
        [ResponseType(typeof(EgreetingUser))]
        public IHttpActionResult DeleteEgreetingUser(int id)
        {
            EgreetingUser egreetingUser = db.EgreetingUsers.Find(id);
            if (egreetingUser == null)
            {
                return NotFound();
            }

            db.EgreetingUsers.Remove(egreetingUser);
            db.SaveChanges();

            return Ok(egreetingUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EgreetingUserExists(int id)
        {
            return db.EgreetingUsers.Count(e => e.EgreetingUserID == id) > 0;
        }
    }
}