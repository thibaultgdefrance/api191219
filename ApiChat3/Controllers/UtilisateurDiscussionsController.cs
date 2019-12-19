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
            int existUtilisateurDiscussion = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            if (existUtilisateurDiscussion > 0)
            {
                return null;
            }
            else
            {
                utilisateurDiscussion.IdUtilisateur = utilisateur.IdUtilisateur;
                utilisateurDiscussion.IdDiscussion = discussion.IdDiscussion;
                utilisateurDiscussion.IdNiveau = 1;

                db.UtilisateurDiscussion.Add(utilisateurDiscussion);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = utilisateurDiscussion.IdUtilisateurDiscussion }, utilisateurDiscussion);
            }
           
        }

        public async Task<IHttpActionResult> ajouterUtilisateurDiscussionToken(string utilisateurToken,string tokenNotif)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            UtilisateurDiscussion utilisateurDiscussion = new UtilisateurDiscussion();
            Utilisateur utilisateur = (from u in db.Utilisateur where u.TokenUtilisateur == utilisateurToken select u).First();
            Notification notification = (from n in db.Notification where n.TokenNotification == tokenNotif select n).First();
            Discussion discussion = (from d in db.Discussion where d.IdDiscussion==notification.IdDiscussion select d).First();
            int existUtilisateurDiscussion = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            if (existUtilisateurDiscussion>0)
            {
                return null;
            }
            else
            {
                utilisateurDiscussion.IdUtilisateur = utilisateur.IdUtilisateur;
                utilisateurDiscussion.IdDiscussion = discussion.IdDiscussion;
                utilisateurDiscussion.IdNiveau = 3;
                db.UtilisateurDiscussion.Add(utilisateurDiscussion);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = utilisateurDiscussion.IdUtilisateurDiscussion }, utilisateurDiscussion);
            }
            
        }
        public async Task<string> postDiscussionContact(string utilisateurToken, string tokenNotif,int contact)
        {
            

            UtilisateurDiscussion utilisateurDiscussion1 = new UtilisateurDiscussion();
            UtilisateurDiscussion utilisateurDiscussion2 = new UtilisateurDiscussion();
            Notification notification = (from n in db.Notification where n.TokenNotification == tokenNotif select n).First();
            Utilisateur utilisateur1 = (from u in db.Utilisateur where u.IdUtilisateur==notification.IdCreateur select u).First();
            Utilisateur utilisateur2 = (from u in db.Utilisateur where u.IdUtilisateur == notification.IdDestinataire select u).First();
            
            Discussion discussion = (from d in db.Discussion where d.TitreDiscussion==utilisateur1.EmailUtilisateur+"/"+utilisateur2.EmailUtilisateur select d).First();
            utilisateurDiscussion1.IdUtilisateur = utilisateur1.IdUtilisateur;
            utilisateurDiscussion1.IdDiscussion = discussion.IdDiscussion;
            utilisateurDiscussion1.IdNiveau = 3;
            utilisateurDiscussion2.IdUtilisateur = utilisateur2.IdUtilisateur;
            utilisateurDiscussion2.IdDiscussion = discussion.IdDiscussion;
            utilisateurDiscussion2.IdNiveau = 3;
            if (!ModelState.IsValid)
            {
                return "ko";
            }
            int existUtilisateurDiscussion1 = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur1.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            int existUtilisateurDiscussion2 = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur2.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            if (existUtilisateurDiscussion1>0 || existUtilisateurDiscussion2>0)
            {
                return null;
            }
            db.UtilisateurDiscussion.Add(utilisateurDiscussion1);
            db.UtilisateurDiscussion.Add(utilisateurDiscussion2);
            await db.SaveChangesAsync();

            return "ok";
        }
        public async Task<IHttpActionResult> postDiscussionContact1(string utilisateurToken, string tokenNotif, int contact1)
        {


            UtilisateurDiscussion utilisateurDiscussion = new UtilisateurDiscussion();
            
            Utilisateur utilisateur1 = (from u in db.Utilisateur join d in db.Notification on u.IdUtilisateur equals d.IdCreateur where d.TokenNotification == tokenNotif select u).First();
            Utilisateur utilisateur2 = (from u in db.Utilisateur join d in db.Notification on u.IdUtilisateur equals d.IdDestinataire where d.TokenNotification == tokenNotif select u).First();
            Notification notification = (from n in db.Notification where n.TokenNotification == tokenNotif select n).First();
            Discussion discussion = (from d in db.Discussion where d.TitreDiscussion == utilisateur1.PseudoUtilisateur + "/" + utilisateur2.PseudoUtilisateur select d).First();
            utilisateurDiscussion.IdUtilisateur = utilisateur1.IdUtilisateur;
            utilisateurDiscussion.IdDiscussion = discussion.IdDiscussion;
            utilisateurDiscussion.IdNiveau = 3;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int existUtilisateurDiscussion1 = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur1.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            int existUtilisateurDiscussion2 = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur2.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            if (existUtilisateurDiscussion1 > 0 || existUtilisateurDiscussion2 > 0)
            {
                return null;
            }
            db.UtilisateurDiscussion.Add(utilisateurDiscussion);
            
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = utilisateurDiscussion.IdUtilisateurDiscussion }, utilisateurDiscussion);
        }
        public async Task<IHttpActionResult> postDiscussionContact2(string utilisateurToken, string tokenNotif, int contact2)
        {


            UtilisateurDiscussion utilisateurDiscussion = new UtilisateurDiscussion();
            
            Utilisateur utilisateur1 = (from u in db.Utilisateur join d in db.Notification on u.IdUtilisateur equals d.IdCreateur where d.TokenNotification == tokenNotif select u).First();
            Utilisateur utilisateur2 = (from u in db.Utilisateur join d in db.Notification on u.IdUtilisateur equals d.IdDestinataire where d.TokenNotification == tokenNotif select u).First();
            Notification notification = (from n in db.Notification where n.TokenNotification == tokenNotif select n).First();
            string titre = utilisateur1.EmailUtilisateur + "/" + utilisateur2.EmailUtilisateur;
            Discussion discussion = (from d in db.Discussion where d.TitreDiscussion == titre select d).First();
            utilisateurDiscussion.IdUtilisateur = utilisateur2.IdUtilisateur;
            utilisateurDiscussion.IdDiscussion = discussion.IdDiscussion;
            utilisateurDiscussion.IdNiveau = 3;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int existUtilisateurDiscussion1 = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur1.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            int existUtilisateurDiscussion2 = (from ud in db.UtilisateurDiscussion where ud.IdUtilisateur == utilisateur2.IdUtilisateur && ud.IdDiscussion == discussion.IdDiscussion select ud).Count();
            if (existUtilisateurDiscussion1 > 0 || existUtilisateurDiscussion2 > 0)
            {
                return null;
            }
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