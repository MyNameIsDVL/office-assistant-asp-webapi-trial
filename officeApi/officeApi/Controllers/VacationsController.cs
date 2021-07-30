using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using officeApi.Models;

namespace officeApi.Controllers
{
    [AllowAnonymous]
    public class VacationsController : ApiController
    {
        private VacModel db = new VacModel();

        // GET: api/Vacations
        public IQueryable<Vacation> GetVacation()
        {
            return db.Vacation;
        }

        // GET: api/Vacations/5
        [ResponseType(typeof(Vacation))]
        public IHttpActionResult GetVacation(int id)
        {
            Vacation vacation = db.Vacation.Find(id);
            if (vacation == null)
            {
                return NotFound();
            }

            return Ok(vacation);
        }

        // PUT: api/Vacations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVacation(int id, Vacation vacation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacation.Id)
            {
                return BadRequest();
            }

            db.Entry(vacation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacationExists(id))
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

        // POST: api/Vacations
        [ResponseType(typeof(Vacation))]
        public IHttpActionResult PostVacation(Vacation vacation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vacation.Add(vacation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vacation.Id }, vacation);
        }

        // DELETE: api/Vacations/5
        [ResponseType(typeof(Vacation))]
        public IHttpActionResult DeleteVacation(int id)
        {
            Vacation vacation = db.Vacation.Find(id);
            if (vacation == null)
            {
                return NotFound();
            }

            db.Vacation.Remove(vacation);
            db.SaveChanges();

            return Ok(vacation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VacationExists(int id)
        {
            return db.Vacation.Count(e => e.Id == id) > 0;
        }
    }
}