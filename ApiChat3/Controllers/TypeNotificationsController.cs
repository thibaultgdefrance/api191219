using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiChat3.Models;

namespace ApiChat3.Controllers
{
    public class TypeNotificationsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/TypeNotifications
        public IQueryable<TypeNotification> GetTypeNotification()
        {
            return db.TypeNotification;
        }

        // GET: api/TypeNotifications/5
        [ResponseType(typeof(TypeNotification))]
        public async Task<IHttpActionResult> GetTypeNotification(int id)
        {
            TypeNotification typeNotification = await db.TypeNotification.FindAsync(id);
            if (typeNotification == null)
            {
                return NotFound();
            }

            return Ok(typeNotification);
        }

        // PUT: api/TypeNotifications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypeNotification(int id, TypeNotification typeNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeNotification.IdTypeNotification)
            {
                return BadRequest();
            }

            db.Entry(typeNotification).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeNotificationExists(id))
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

        // POST: api/TypeNotifications
        [ResponseType(typeof(TypeNotification))]
        public async Task<IHttpActionResult> PostTypeNotification(TypeNotification typeNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeNotification.Add(typeNotification);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typeNotification.IdTypeNotification }, typeNotification);
        }

        // DELETE: api/TypeNotifications/5
        [ResponseType(typeof(TypeNotification))]
        public async Task<IHttpActionResult> DeleteTypeNotification(int id)
        {
            TypeNotification typeNotification = await db.TypeNotification.FindAsync(id);
            if (typeNotification == null)
            {
                return NotFound();
            }

            db.TypeNotification.Remove(typeNotification);
            await db.SaveChangesAsync();

            return Ok(typeNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeNotificationExists(int id)
        {
            return db.TypeNotification.Count(e => e.IdTypeNotification == id) > 0;
        }
    }
}