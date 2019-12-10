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
    public class UtilisateursController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/Utilisateurs
        public IQueryable<Utilisateur> GetUtilisateur()
        {
            return db.Utilisateur;
        }

        // GET: api/Utilisateurs/5
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> GetUtilisateur(int id)
        {
            Utilisateur utilisateur = await db.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(utilisateur);
        }
         
        // PUT: api/Utilisateurs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateur.IdUtilisateur)
            {
                return BadRequest();
            }

            db.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        // POST: api/Utilisateurs
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Utilisateur.Add(utilisateur);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = utilisateur.IdUtilisateur }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> DeleteUtilisateur(int id)
        {
            Utilisateur utilisateur = await db.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            db.Utilisateur.Remove(utilisateur);
            await db.SaveChangesAsync();

            return Ok(utilisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UtilisateurExists(int id)
        {
            return db.Utilisateur.Count(e => e.IdUtilisateur == id) > 0;
        }


        public List<Participant> getDiscussionParticipant(string tokenDiscussion, string tokenUtilisateur)
        {
            Discussion discussion = (from d in db.Discussion where d.TokenDiscussion == tokenDiscussion select d).First();
            List<Utilisateur> utilisateurs = (from u in db.Utilisateur join ud in db.UtilisateurDiscussion on u.IdUtilisateur equals ud.IdUtilisateur where ud.IdDiscussion == discussion.IdDiscussion select u).ToList(); ;
            List<Participant> participants = new List<Participant>();
            foreach (var item in utilisateurs)
            {
                Participant participant = new Participant();
                participant.NomUtilisateur = item.NomUtilisateur;
                participant.PrenomUtilisateur = item.PrenomUtilisateur;
                participant.PseudoUtilisateur = item.PseudoUtilisateur;
                participant.EmailUtilisateur = item.EmailUtilisateur;
                participant.IdAcces = item.IdAcces;
                participant.IdAvatar = item.IdAvatar;
                participants.Add(participant);
                
            }
            return participants;
        }
    }
}