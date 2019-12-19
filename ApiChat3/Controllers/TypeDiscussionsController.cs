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
    public class TypeDiscussionsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();
        // GET: api/TypeDiscussions
        public IQueryable<TypeDiscussion> GetTypeDiscussion()
        {
            return db.TypeDiscussion;
        }

        // GET: api/TypeDiscussions/5
        [ResponseType(typeof(TypeDiscussion))]
        public async Task<IHttpActionResult> GetTypeDiscussion(int id)
        {
            TypeDiscussion typeDiscussion = await db.TypeDiscussion.FindAsync(id);
            if (typeDiscussion == null)
            {
                return NotFound();
            }

            return Ok(typeDiscussion);
        }

        // PUT: api/TypeDiscussions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypeDiscussion(int id, TypeDiscussion typeDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeDiscussion.IdTypeDiscussion)
            {
                return BadRequest();
            }

            db.Entry(typeDiscussion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeDiscussionExists(id))
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

        // POST: api/TypeDiscussions
        [ResponseType(typeof(TypeDiscussion))]
        public async Task<IHttpActionResult> PostTypeDiscussion(TypeDiscussion typeDiscussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeDiscussion.Add(typeDiscussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typeDiscussion.IdTypeDiscussion }, typeDiscussion);
        }

        // DELETE: api/TypeDiscussions/5
        [ResponseType(typeof(TypeDiscussion))]
        public async Task<IHttpActionResult> DeleteTypeDiscussion(int id)
        {
            TypeDiscussion typeDiscussion = await db.TypeDiscussion.FindAsync(id);
            if (typeDiscussion == null)
            {
                return NotFound();
            }

            db.TypeDiscussion.Remove(typeDiscussion);
            await db.SaveChangesAsync();

            return Ok(typeDiscussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeDiscussionExists(int id)
        {
            return db.TypeDiscussion.Count(e => e.IdTypeDiscussion == id) > 0;
        }
    }
}