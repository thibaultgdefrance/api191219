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
using Newtonsoft.Json;

namespace ApiChat3.Controllers
{
    public class DiscussionsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();
        Worflow worflow = new Worflow();
        // GET: api/Discussions
        public IQueryable<Discussion> GetDiscussion()
        {
            return db.Discussion;
        }

        // GET: api/Discussions/5
        [ResponseType(typeof(Discussion))]
        public async Task<IHttpActionResult> GetDiscussion(int id)
        {
            Discussion discussion = await db.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            return Ok(discussion);
        }

        // PUT: api/Discussions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiscussion(int id, Discussion discussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discussion.IdDiscussion)
            {
                return BadRequest();
            }

            db.Entry(discussion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscussionExists(id))
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

        // POST: api/Discussions
        [ResponseType(typeof(Discussion))]
        public async Task<IHttpActionResult> PostDiscussion(Discussion discussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discussion.Add(discussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = discussion.IdDiscussion }, discussion);
        }

        public async Task<IHttpActionResult> CreerDiscussion(string titre,string description,string tokenUtilisateur)
        {
            Discussion discussion = new Discussion();
            discussion.DateCreationDiscussion = DateTime.Now;
            discussion.DescriptionDiscussion = description;
            discussion.IdStatutDiscussion = 1;
            discussion.IdTypeDiscussion = 2;
            discussion.TitreDiscussion = titre;
            discussion.StatutDiscussion = 1;
            discussion.IdCreateur = (from u in db.Utilisateur where u.TokenUtilisateur == tokenUtilisateur select u.IdUtilisateur).First();
            discussion.TokenDiscussion = worflow.createToken();
            int tokenExist = (from d in db.Discussion where d.TokenDiscussion==discussion.TokenDiscussion select d).Count();
            
            if (tokenExist > 0)
            {
                int test = tokenExist;
                while (test > 0)
                {
                    discussion.TokenDiscussion = worflow.createToken();
                    test--;
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discussion.Add(discussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = discussion.IdDiscussion }, discussion);
        }
        public async Task<string> CreerDiscussionContact(string tokenNotification)
        {
            try
            {
                Notification notification = (from n in db.Notification where n.TokenNotification == tokenNotification select n).First();
                Utilisateur utilisateur1 = (from u in db.Utilisateur where u.IdUtilisateur == notification.IdCreateur select u).First();
                Utilisateur utilisateur2 = (from u in db.Utilisateur where u.IdUtilisateur == notification.IdDestinataire select u).First();
                Discussion discussion = new Discussion();
                discussion.DateCreationDiscussion = DateTime.Now;
                discussion.DescriptionDiscussion = "";
                discussion.IdStatutDiscussion = 1;
                discussion.IdTypeDiscussion = 1;
                discussion.TitreDiscussion = utilisateur1.EmailUtilisateur + "/" + utilisateur2.EmailUtilisateur;
                discussion.StatutDiscussion = 1;
                discussion.IdCreateur = utilisateur1.IdUtilisateur;
                discussion.TokenDiscussion = worflow.createToken();
                int tokenExist = (from d in db.Discussion where d.TokenDiscussion == discussion.TokenDiscussion select d).Count();

                if (tokenExist > 0)
                {
                    int test = tokenExist;
                    while (test > 0)
                    {
                        discussion.TokenDiscussion = worflow.createToken();
                        test--;
                    }
                }
                if (!ModelState.IsValid)
                {
                    return "ko";
                }

                db.Discussion.Add(discussion);
                await db.SaveChangesAsync();

                return "ok";
            }
            catch (Exception ex)
            {

                return ex.Message + ex.InnerException;
            } 
        }




        // DELETE: api/Discussions/5
        [ResponseType(typeof(Discussion))]
        public async Task<IHttpActionResult> DeleteDiscussion(int id)
        {
            Discussion discussion = await db.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            db.Discussion.Remove(discussion);
            await db.SaveChangesAsync();

            return Ok(discussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscussionExists(int id)
        {
            return db.Discussion.Count(e => e.IdDiscussion == id) > 0;
        }



        public List<Discussion> GetDiscussionUtilisateur(string token)
        {
            try
            {
                Utilisateur utilisateur = (from u in db.Utilisateur where u.TokenUtilisateur == token select u).First();
                //int idUtilisateur = Convert.ToInt32(token);
                List<Discussion> discussionsUtilisateur = (from d in db.Discussion join u in db.UtilisateurDiscussion on d.IdDiscussion equals u.IdDiscussion where u.IdUtilisateur == utilisateur.IdUtilisateur && d.IdTypeDiscussion==2 select d).ToList();
                //List<Discussion> discussionsUtilisateur = (from d in db.Discussion select d).ToList();

                //var result = JsonConvert.SerializeObject(discussionsUtilisateur);
                return discussionsUtilisateur;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        [HttpGet]

        public List<Contact> GetContact(string token,int dif)
        {
            try
            {
                Utilisateur utilisateur = (from u in db.Utilisateur where u.TokenUtilisateur == token select u).First();
                List<Discussion> discussions = (from d in db.Discussion join ud in db.UtilisateurDiscussion on d.IdDiscussion equals ud.IdDiscussion where d.IdTypeDiscussion==1 && ud.IdUtilisateur==utilisateur.IdUtilisateur select d).ToList();
                List<Contact> contacts = new List<Contact>();
                foreach (var item in discussions)
                {
                    Contact contact = new Contact();
                    contact.TokenDiscussion = item.TokenDiscussion;
                    contact.DateCreationDiscussion = item.DateCreationDiscussion;
                    contact.IdStatutDiscussion = item.IdStatutDiscussion;
                    contact.StatutDiscussion = item.StatutDiscussion;
                    //select u.PseudoUtilisateur from Utilisateur as u Where u.IdUtilisateur in (select IdUtilisateur from dbo.UtilisateurDiscussion where IdDiscussion=1)
                    contact.NomContact = (from u in db.Utilisateur join ud in db.UtilisateurDiscussion on u.IdUtilisateur equals ud.IdUtilisateur where ud.IdDiscussion == item.IdDiscussion && u.TokenUtilisateur != token select u.PseudoUtilisateur).First();
                    
                    contacts.Add(contact);
                }
                return contacts;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}