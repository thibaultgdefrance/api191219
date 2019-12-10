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
    public class AvatarsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/Avatars
        public IQueryable<Avatar> GetAvatar()
        {
            return db.Avatar;
        }

        // GET: api/Avatars/5
        [ResponseType(typeof(Avatar))]
        public async Task<IHttpActionResult> GetAvatar(int id)
        {
            Avatar avatar = await db.Avatar.FindAsync(id);
            if (avatar == null)
            {
                return NotFound();
            }

            return Ok(avatar);
        }

        // PUT: api/Avatars/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAvatar(int id, Avatar avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != avatar.IdAvatar)
            {
                return BadRequest();
            }

            db.Entry(avatar).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvatarExists(id))
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

        // POST: api/Avatars
        [ResponseType(typeof(Avatar))]
        public async Task<IHttpActionResult> PostAvatar(Avatar avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Avatar.Add(avatar);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = avatar.IdAvatar }, avatar);
        }

        // DELETE: api/Avatars/5
        [ResponseType(typeof(Avatar))]
        public async Task<IHttpActionResult> DeleteAvatar(int id)
        {
            Avatar avatar = await db.Avatar.FindAsync(id);
            if (avatar == null)
            {
                return NotFound();
            }

            db.Avatar.Remove(avatar);
            await db.SaveChangesAsync();

            return Ok(avatar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvatarExists(int id)
        {
            return db.Avatar.Count(e => e.IdAvatar == id) > 0;
        }
    }
}