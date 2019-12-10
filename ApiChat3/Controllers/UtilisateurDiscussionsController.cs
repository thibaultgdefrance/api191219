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
    public class UtilisateurDiscussionsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/UtilisateurDiscussions
        public IQueryable<UtilisateurDiscussion> GetUtilisateurDiscussion()
        {
            return db.UtilisateurDiscussion;
        }

        // GET: api/UtilisateurDiscussions/5
        [ResponseType(typeof(UtilisateurDiscussion))]
        public async Task<IHttpActionResult> GetUtilisateurDiscussion(int id)
        {
            UtilisateurDiscussion utilisateurDiscussion = await db.UtilisateurDiscussion.FindAsync(id);
            if (utilisateurDiscussion == null)
            {
                return NotFound();
            }

            return Ok(utilisateurDiscussion);
        }

        // PUT: api/UtilisateurDiscussions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUtilisateurDiscussion(int id, UtilisateurDiscussion utilisateurDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateurDiscussion.IdUtilisateurDiscussion)
            {
                return BadRequest();
            }

            db.Entry(utilisateurDiscussion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurDiscussionExists(id))
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

        // POST: api/UtilisateurDiscussions
        [ResponseType(typeof(UtilisateurDiscussion))]
        public async Task<IHttpActionResult> PostUtilisateurDiscussion(UtilisateurDiscussion utilisateurDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UtilisateurDiscussion.Add(utilisateurDiscussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = utilisateurDiscussion.IdUtilisateurDiscussion }, utilisateurDiscussion);
        }


        [ResponseType(typeof(UtilisateurDiscussion))]
        public async Task<IHttpActionResult> PostUtilisateurDiscussionToken(string utilisateurToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UtilisateurDiscussion utilisateurDiscussion = new UtilisateurDiscussion();
            Utilisateur utilisateur = (from u in db.Utilisateur where u.TokenUtilisateur==utilisateurToken select u).First();
            Discussion discussion = (from d in db.Discussion join u in db.Utilisateur on d.IdCreateur equals u.IdUtilisateur where u.TokenUtilisateur==utilisateurToken orderby d.DateCreationDiscussion descending select d).First();
            utilisateurDiscussion.IdUtilisateur = utilisateur.IdUtilisateur;
            utilisateurDiscussion.IdDiscussion = discussion.IdDiscussion;
            utilisateurDiscussion.IdNiveau = 1;
            db.UtilisateurDiscussion.Add(utilisateurDiscussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = utilisateurDiscussion.IdUtilisateurDiscussion }, utilisateurDiscussion);
        }




        // DELETE: api/UtilisateurDiscussions/5
        [ResponseType(typeof(UtilisateurDiscussion))]
        public async Task<IHttpActionResult> DeleteUtilisateurDiscussion(int id)
        {
            UtilisateurDiscussion utilisateurDiscussion = await db.UtilisateurDiscussion.FindAsync(id);
            if (utilisateurDiscussion == null)
            {
                return NotFound();
            }

            db.UtilisateurDiscussion.Remove(utilisateurDiscussion);
            await db.SaveChangesAsync();

            return Ok(utilisateurDiscussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UtilisateurDiscussionExists(int id)
        {
            return db.UtilisateurDiscussion.Count(e => e.IdUtilisateurDiscussion == id) > 0;
        }
    }
}