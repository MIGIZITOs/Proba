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
using Intento.Models;

namespace Intento.Controllers
{
    [RoutePrefix("api")]
    public class operacionsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/operacions
        public IQueryable<operacion> Getoperacions()
        {
            return db.operacions;
        }

        // GET: api/operacions/5
        [ResponseType(typeof(operacion))]
        public IHttpActionResult Getoperacion(int id)
        {
            operacion operacion = db.operacions.Find(id);
            if (operacion == null)
            {
                return NotFound();
            }

            return Ok(operacion);
        }

        // PUT: api/operacions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putoperacion(int id, operacion operacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operacion.numero)
            {
                return BadRequest();
            }

            db.Entry(operacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!operacionExists(id))
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

        // POST: api/operacions
        [ResponseType(typeof(operacion))]
        public IHttpActionResult Postoperacion(operacion operacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.operacions.Add(operacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = operacion.numero }, operacion);
        }
        [HttpGet]
        [Route("{id:int}")]
        public string Operacion(int id)
        {
            if (id < 0)
                return "ERROR";
            if (id == 0)
                return "Realizado por Migizito";
            return "Usted ha ingresado el numero " + id;
        }

        // DELETE: api/operacions/5
        [ResponseType(typeof(operacion))]
        public IHttpActionResult Deleteoperacion(int id)
        {
            operacion operacion = db.operacions.Find(id);
            if (operacion == null)
            {
                return NotFound();
            }

            db.operacions.Remove(operacion);
            db.SaveChanges();

            return Ok(operacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool operacionExists(int id)
        {
            return db.operacions.Count(e => e.numero == id) > 0;
        }
    }
}