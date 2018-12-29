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
using ValaisBookingAPI.Models;

namespace ValaisBookingAPI.Controllers
{
    public class ReservationsController : ApiController
    {
        private ValaisBookingEntities db = new ValaisBookingEntities();

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public async System.Threading.Tasks.Task<IHttpActionResult> PostReservationAsync(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reservation.IdReservation }, reservation);
        }

        // GET: api/reservations/19/rooms/
        [Route("api/reservations/{idReservation}/rooms")]
        public IQueryable<Room> GetRoomsReservations(int idReservation)
        {
            return db.Reservations.Where(re => re.IdReservation == idReservation).SelectMany(re=>db.Rooms);
        }

        /*
        // POST: api/reservations/{idReservation}/rooms/{idRoom}
        [Route("api/reservations/{idReservation}/rooms")]
        public IQueryable<Room> PostRoomsReservations(int idReservation, )
        {
            return db.Reservations.Where(re => re.IdReservation == idReservation).SelectMany(re => db.Rooms);
        }
        */

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.IdReservation == id) > 0;
        }
    }
}