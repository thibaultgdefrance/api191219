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
    public class StatutUtilisateursController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/StatutUtilisateurs
        public IQueryable<StatutUtilisateur> GetStatutUtilisateur()
        {
            return db.StatutUtilisateur;
        }

        // GET: api/StatutUtilisateurs/5
        [ResponseType(typeof(StatutUtilisateur))]
        public async Task<IHttpActionResult> GetStatutUtilisateur(int id)
        {
            StatutUtilisateur statutUtilisateur = await db.StatutUtilisateur.FindAsync(id);
            if (statutUtilisateur == null)
            {
                return NotFound();
            }

            return Ok(statutUtilisateur);
        }

        // PUT: api/StatutUtilisateurs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStatutUtilisateur(int id, StatutUtilisateur statutUtilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statutUtilisateur.IdStatutUtilisateur)
            {
                return BadRequest();
            }

            db.Entry(statutUtilisateur).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatutUtilisateurExists(id))
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

        // POST: api/StatutUtilisateurs
        [ResponseType(typeof(StatutUtilisateur))]
        public async Task<IHttpActionResult> PostStatutUtilisateur(StatutUtilisateur statutUtilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StatutUtilisateur.Add(statutUtilisateur);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = statutUtilisateur.IdStatutUtilisateur }, statutUtilisateur);
        }

        // DELETE: api/StatutUtilisateurs/5
        [ResponseType(typeof(StatutUtilisateur))]
        public async Task<IHttpActionResult> DeleteStatutUtilisateur(int id)
        {
            StatutUtilisateur statutUtilisateur = await db.StatutUtilisateur.FindAsync(id);
            if (statutUtilisateur == null)
            {
                return NotFound();
            }

            db.StatutUtilisateur.Remove(statutUtilisateur);
            await db.SaveChangesAsync();

            return Ok(statutUtilisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatutUtilisateurExists(int id)
        {
            return db.StatutUtilisateur.Count(e => e.IdStatutUtilisateur == id) > 0;
        }
    }
}