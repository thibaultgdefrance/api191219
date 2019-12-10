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
    public class TonsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/Tons
        public IQueryable<Ton> GetTon()
        {
            return db.Ton;
        }

        // GET: api/Tons/5
        [ResponseType(typeof(Ton))]
        public async Task<IHttpActionResult> GetTon(int id)
        {
            Ton ton = await db.Ton.FindAsync(id);
            if (ton == null)
            {
                return NotFound();
            }

            return Ok(ton);
        }

        // PUT: api/Tons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTon(int id, Ton ton)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ton.IdTon)
            {
                return BadRequest();
            }

            db.Entry(ton).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TonExists(id))
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

        // POST: api/Tons
        [ResponseType(typeof(Ton))]
        public async Task<IHttpActionResult> PostTon(Ton ton)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ton.Add(ton);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ton.IdTon }, ton);
        }

        // DELETE: api/Tons/5
        [ResponseType(typeof(Ton))]
        public async Task<IHttpActionResult> DeleteTon(int id)
        {
            Ton ton = await db.Ton.FindAsync(id);
            if (ton == null)
            {
                return NotFound();
            }

            db.Ton.Remove(ton);
            await db.SaveChangesAsync();

            return Ok(ton);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TonExists(int id)
        {
            return db.Ton.Count(e => e.IdTon == id) > 0;
        }
    }
}