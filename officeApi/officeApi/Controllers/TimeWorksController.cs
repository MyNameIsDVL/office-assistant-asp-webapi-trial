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
    public class TimeWorksController : ApiController
    {
        private VacModel db = new VacModel();

        // GET: api/TimeWorks
        public IQueryable<TimeWork> GetTimeWork()
        {
            return db.TimeWork;
        }

        // GET: api/TimeWorks/5
        [ResponseType(typeof(TimeWork))]
        public IHttpActionResult GetTimeWork(int id)
        {
            TimeWork timeWork = db.TimeWork.Find(id);
            if (timeWork == null)
            {
                return NotFound();
            }

            return Ok(timeWork);
        }

        // PUT: api/TimeWorks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimeWork(int id, TimeWork timeWork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeWork.Id)
            {
                return BadRequest();
            }

            db.Entry(timeWork).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeWorkExists(id))
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

        // POST: api/TimeWorks
        [ResponseType(typeof(TimeWork))]
        public IHttpActionResult PostTimeWork(TimeWork timeWork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeWork.Add(timeWork);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = timeWork.Id }, timeWork);
        }

        // DELETE: api/TimeWorks/5
        [ResponseType(typeof(TimeWork))]
        public IHttpActionResult DeleteTimeWork(int id)
        {
            TimeWork timeWork = db.TimeWork.Find(id);
            if (timeWork == null)
            {
                return NotFound();
            }

            db.TimeWork.Remove(timeWork);
            db.SaveChanges();

            return Ok(timeWork);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeWorkExists(int id)
        {
            return db.TimeWork.Count(e => e.Id == id) > 0;
        }
    }
}