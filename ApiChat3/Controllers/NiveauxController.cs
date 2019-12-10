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
    public class NiveauxController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/Niveaux
        public IQueryable<Niveau> GetNiveau()
        {
            return db.Niveau;
        }

        // GET: api/Niveaux/5
        [ResponseType(typeof(Niveau))]
        public async Task<IHttpActionResult> GetNiveau(int id)
        {
            Niveau niveau = await db.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return NotFound();
            }

            return Ok(niveau);
        }

        // PUT: api/Niveaux/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNiveau(int id, Niveau niveau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != niveau.IdNiveau)
            {
                return BadRequest();
            }

            db.Entry(niveau).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NiveauExists(id))
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

        // POST: api/Niveaux
        [ResponseType(typeof(Niveau))]
        public async Task<IHttpActionResult> PostNiveau(Niveau niveau)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Niveau.Add(niveau);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = niveau.IdNiveau }, niveau);
        }

        // DELETE: api/Niveaux/5
        [ResponseType(typeof(Niveau))]
        public async Task<IHttpActionResult> DeleteNiveau(int id)
        {
            Niveau niveau = await db.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return NotFound();
            }

            db.Niveau.Remove(niveau);
            await db.SaveChangesAsync();

            return Ok(niveau);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NiveauExists(int id)
        {
            return db.Niveau.Count(e => e.IdNiveau == id) > 0;
        }
    }
}