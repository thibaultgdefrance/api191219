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
    public class NotificationsController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();
        Worflow worflow = new Worflow();
        // GET: api/Notifications
        public IQueryable<Notification> GetNotification()
        {
            return db.Notification;
        }

        // GET: api/Notifications/5
        [ResponseType(typeof(Notification))]
        public async Task<IHttpActionResult> GetNotification(int id)
        {
            Notification notification = await db.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpGet]
        public List<NotificationDiscussion> afficherNotification(string tokenUtilisateur)
        {
            int idDestinataire = (from u in db.Utilisateur where u.TokenUtilisateur == tokenUtilisateur select u.IdUtilisateur).First();
            List<Notification> notifications = (from n in db.Notification where n.IdDestinataire==idDestinataire select n).ToList();
            List<NotificationDiscussion> notificationDiscussions = new List<NotificationDiscussion>();
            foreach (var item in notifications)
            {
                if (item.IdTypeNotification==1)
                {
                    NotificationDiscussion notificationDiscussion = new NotificationDiscussion();
                    notificationDiscussion.EmailCreateur = (from u in db.Utilisateur where u.IdUtilisateur == item.IdCreateur select u.EmailUtilisateur).First();
                    notificationDiscussion.TitreDiscussion = null;
                    notificationDiscussion.IdNotification = item.IdNotification;
                    notificationDiscussion.TokenNotification = item.TokenNotification;
                    notificationDiscussions.Add(notificationDiscussion);
                }
                else
                {

                    NotificationDiscussion notificationDiscussion = new NotificationDiscussion();
                    notificationDiscussion.EmailCreateur = (from u in db.Utilisateur where u.IdUtilisateur == item.IdCreateur select u.EmailUtilisateur).First();
                    notificationDiscussion.TitreDiscussion = (from d in db.Discussion where d.IdDiscussion == item.IdDiscussion select d.TitreDiscussion).First();
                    notificationDiscussion.IdNotification = item.IdNotification;
                    notificationDiscussion.TokenNotification = item.TokenNotification;
                    notificationDiscussions.Add(notificationDiscussion);
                }
               
            }


            return notificationDiscussions;
        }

        // PUT: api/Notifications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNotification(int id, Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notification.IdNotification)
            {
                return BadRequest();
            }

            db.Entry(notification).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        [ResponseType(typeof(Notification))]
        public async Task<IHttpActionResult> PostNotification(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notification.Add(notification);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = notification.IdNotification }, notification);
        }
        [ResponseType(typeof(Notification))]
        public async Task<IHttpActionResult> PostNotificationDiscussion(string tokenUtilisateur,string emailDestinataire,string tokenDiscussion)
        {
            Notification notification = new Notification();
            Utilisateur utilisateur= (from u in db.Utilisateur where u.TokenUtilisateur == tokenUtilisateur select u).First();
            
            Discussion discussion= (from d in db.Discussion where d.TokenDiscussion == tokenDiscussion select d).First();
            int idDestinataire = (from u in db.Utilisateur where u.EmailUtilisateur == emailDestinataire select u.IdUtilisateur).First();
            notification.IdDiscussion = discussion.IdDiscussion;
            notification.IdCreateur = utilisateur.IdUtilisateur;
            notification.IdDestinataire = idDestinataire;
            notification.TexteNotification = utilisateur.EmailUtilisateur + " vous invite à participer à la discussion " +discussion.TitreDiscussion;
            notification.StatutNotification = 1;
            notification.IdTypeNotification = 2;
            notification.TokenNotification = worflow.createToken();
            int tokenExist = (from n in db.Notification where n.TokenNotification==notification.TokenNotification select n).Count();
            if (tokenExist > 0)
            {
                int test = tokenExist;
                while (test > 0)
                {
                    notification.TokenNotification = worflow.createToken();
                    test--;
                }
            }
            int existNotification = (from n in db.Notification where n.IdDiscussion == notification.IdDiscussion && n.IdCreateur == notification.IdCreateur && n.IdDestinataire == notification.IdDestinataire select n).Count();
            if (existNotification>0)
            {
                return null;
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }else if(existNotification>0){
                return null;
            }
            else if (discussion.IdTypeDiscussion==1)
            {
                return null;
            }

            db.Notification.Add(notification);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = notification.IdNotification }, notification);
        }


        [ResponseType(typeof(Notification))]
        public async Task<string> PostNotificationContact(string tokenUtilisateur, string emailDestinataire)
        {
            try
            {
                Notification notification = new Notification();
                notification.IdTypeNotification = 1;
                Utilisateur utilisateur = (from u in db.Utilisateur where u.TokenUtilisateur == tokenUtilisateur select u).First();
                notification.IdCreateur = (from u in db.Utilisateur where u.TokenUtilisateur == tokenUtilisateur select u.IdUtilisateur).First();
                notification.IdDestinataire = (from u in db.Utilisateur where u.EmailUtilisateur == emailDestinataire select u.IdUtilisateur).First();
                notification.TexteNotification = "vous avez reçu une demande de contact de la part de" + utilisateur.EmailUtilisateur;
                notification.StatutNotification = 1;
                notification.TokenNotification = worflow.createToken();
                int tokenExist = (from n in db.Notification where n.TokenNotification == notification.TokenNotification select n).Count();
                if (tokenExist > 0)
                {
                    int test = tokenExist;
                    while (test > 0)
                    {
                        notification.TokenNotification = worflow.createToken();
                        test--;
                    }
                }
                int existNotification = (from n in db.Notification where n.IdDiscussion == notification.IdDiscussion && n.IdCreateur == notification.IdCreateur && n.IdDestinataire == notification.IdDestinataire select n).Count();
                if (existNotification > 0)
                {
                    return null;
                }
                if (!ModelState.IsValid)
                {
                    return "ko";
                }
                else if (existNotification > 0)
                {
                    return null;
                }

                db.Notification.Add(notification);
                await db.SaveChangesAsync();
                return "ok";
            }
            catch (Exception ex)
            {

                return ex.Message +ex.InnerException;
            }  

            
        }

        // DELETE: api/Notifications/5
        [ResponseType(typeof(Notification))]
        public async Task<IHttpActionResult> DeleteNotification(int id)
        {
            Notification notification = await db.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            db.Notification.Remove(notification);
            await db.SaveChangesAsync();

            return Ok(notification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificationExists(int id)
        {
            return db.Notification.Count(e => e.IdNotification == id) > 0;
        }





        [ResponseType(typeof(Notification))]
        public  async Task<IHttpActionResult> SupprimerNotification(string tokenNotification)
        {
            
            Notification notification = (from n in db.Notification where n.TokenNotification==tokenNotification select n).First();
            if (notification == null)
            {
                return null;
            }

            db.Notification.Remove(notification);
            await db.SaveChangesAsync();

            return Ok(notification);
        }


    }
}