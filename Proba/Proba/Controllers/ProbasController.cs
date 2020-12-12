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
using Proba.Models;

namespace Proba.Controllers
{
    [RoutePrefix("api")]
    public class ProbasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Probas
        public IQueryable<Proba> GetProbas()
        {
            return db.Probas;
        }

        // GET: api/Probas/5
        [ResponseType(typeof(Proba))]
        public IHttpActionResult GetProba(int id)
        {
            Proba proba = db.Probas.Find(id);
            if (proba == null)
            {
                return NotFound();
            }

            return Ok(proba);
        }

        // PUT: api/Probas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProba(int id, Proba proba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proba.numero)
            {
                return BadRequest();
            }

            db.Entry(proba).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProbaExists(id))
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

        // POST: api/Probas
        [ResponseType(typeof(Proba))]
        public IHttpActionResult PostProba(Proba proba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Probas.Add(proba);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = proba.numero }, proba);
        }
        [HttpGet]
        [Route("{id:int}")]
        public string Operacion(int id)
        {
            if (id < 0)
                return "ERROR";
            if (id == 0)
                return "Realizado Por Migizito";
            return "Usted ingreso el numero" + id;
        }

        // DELETE: api/Probas/5
        [ResponseType(typeof(Proba))]
        public IHttpActionResult DeleteProba(int id)
        {
            Proba proba = db.Probas.Find(id);
            if (proba == null)
            {
                return NotFound();
            }

            db.Probas.Remove(proba);
            db.SaveChanges();

            return Ok(proba);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProbaExists(int id)
        {
            return db.Probas.Count(e => e.numero == id) > 0;
        }
    }
}