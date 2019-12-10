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
    public class MaintenancesController : ApiController
    {
        private Chat2Entities1 db = new Chat2Entities1();

        // GET: api/Maintenances
        public IQueryable<Maintenance> GetMaintenance()
        {
            return db.Maintenance;
        }

        // GET: api/Maintenances/5
        [ResponseType(typeof(Maintenance))]
        public async Task<IHttpActionResult> GetMaintenance(int id)
        {
            Maintenance maintenance = await db.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return Ok(maintenance);
        }

        // PUT: api/Maintenances/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMaintenance(int id, Maintenance maintenance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maintenance.IdMaintenance)
            {
                return BadRequest();
            }

            db.Entry(maintenance).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceExists(id))
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

        // POST: api/Maintenances
        [ResponseType(typeof(Maintenance))]
        public async Task<IHttpActionResult> PostMaintenance(Maintenance maintenance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Maintenance.Add(maintenance);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = maintenance.IdMaintenance }, maintenance);
        }

        // DELETE: api/Maintenances/5
        [ResponseType(typeof(Maintenance))]
        public async Task<IHttpActionResult> DeleteMaintenance(int id)
        {
            Maintenance maintenance = await db.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            db.Maintenance.Remove(maintenance);
            await db.SaveChangesAsync();

            return Ok(maintenance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaintenanceExists(int id)
        {
            return db.Maintenance.Count(e => e.IdMaintenance == id) > 0;
        }
    }
}