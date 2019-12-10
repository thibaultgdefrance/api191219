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
    public class StatutDiscussionsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/StatutDiscussions
        public IQueryable<StatutDiscussion> GetStatutDiscussion()
        {
            return db.StatutDiscussion;
        }

        // GET: api/StatutDiscussions/5
        [ResponseType(typeof(StatutDiscussion))]
        public async Task<IHttpActionResult> GetStatutDiscussion(int id)
        {
            StatutDiscussion statutDiscussion = await db.StatutDiscussion.FindAsync(id);
            if (statutDiscussion == null)
            {
                return NotFound();
            }

            return Ok(statutDiscussion);
        }

        // PUT: api/StatutDiscussions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStatutDiscussion(int id, StatutDiscussion statutDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statutDiscussion.IdStatutDiscussion)
            {
                return BadRequest();
            }

            db.Entry(statutDiscussion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatutDiscussionExists(id))
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

        // POST: api/StatutDiscussions
        [ResponseType(typeof(StatutDiscussion))]
        public async Task<IHttpActionResult> PostStatutDiscussion(StatutDiscussion statutDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StatutDiscussion.Add(statutDiscussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = statutDiscussion.IdStatutDiscussion }, statutDiscussion);
        }

        // DELETE: api/StatutDiscussions/5
        [ResponseType(typeof(StatutDiscussion))]
        public async Task<IHttpActionResult> DeleteStatutDiscussion(int id)
        {
            StatutDiscussion statutDiscussion = await db.StatutDiscussion.FindAsync(id);
            if (statutDiscussion == null)
            {
                return NotFound();
            }

            db.StatutDiscussion.Remove(statutDiscussion);
            await db.SaveChangesAsync();

            return Ok(statutDiscussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatutDiscussionExists(int id)
        {
            return db.StatutDiscussion.Count(e => e.IdStatutDiscussion == id) > 0;
        }
    }
}