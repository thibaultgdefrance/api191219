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
    public class AccesController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/Acces
        public IQueryable<Acces> GetAcces()
        {
            return db.Acces;
        }

        // GET: api/Acces/5
        [ResponseType(typeof(Acces))]
        public async Task<IHttpActionResult> GetAcces(int id)
        {
            Acces acces = await db.Acces.FindAsync(id);
            if (acces == null)
            {
                return NotFound();
            }

            return Ok(acces);
        }

        // PUT: api/Acces/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAcces(int id, Acces acces)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != acces.IdAcces)
            {
                return BadRequest();
            }

            db.Entry(acces).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccesExists(id))
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

        // POST: api/Acces
        [ResponseType(typeof(Acces))]
        public async Task<IHttpActionResult> PostAcces(Acces acces)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acces.Add(acces);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = acces.IdAcces }, acces);
        }

        // DELETE: api/Acces/5
        [ResponseType(typeof(Acces))]
        public async Task<IHttpActionResult> DeleteAcces(int id)
        {
            Acces acces = await db.Acces.FindAsync(id);
            if (acces == null)
            {
                return NotFound();
            }

            db.Acces.Remove(acces);
            await db.SaveChangesAsync();

            return Ok(acces);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccesExists(int id)
        {
            return db.Acces.Count(e => e.IdAcces == id) > 0;
        }
    }
}