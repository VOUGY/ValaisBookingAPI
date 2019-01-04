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
using ValaisBookingAPI.Models;

namespace ValaisBookingAPI.Controllers
{
    public class HotelsController : ApiController
    {
        private ValaisBookingEntities db = new ValaisBookingEntities();

        // GET: api/Hotels
        public IQueryable<Hotel> GetHotels()
        {
            return db.Hotels;
        }

        // GET: api/Hotels/5
        [ResponseType(typeof(Hotel))]
        public async Task<IHttpActionResult> GetHotel(int id)
        {
            Hotel hotel = await db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // GET: api/Hotels/5/Rooms
        [Route("api/hotels/{idHotel}/rooms")]
        public IQueryable<Room> GetRoomsByHotel(int idHotel)
        {         

            return db.Rooms.Where(r => r.IdHotel == idHotel);
        }

        // GET: api/Hotels/5/Rooms
        [Route("api/hotels/{idHotel}/rooms")]
        public List<Room> GetRoomsByHotel(int idHotel, int withReservation, string arrival, string departure)
        {
          DateTime Darrival = DateTime.Parse(arrival);
          DateTime Ddeparture = DateTime.Parse(departure);
            //   var query = from Roo in db.Rooms
            //               join Res in db.Reservations on Roo.IdRoom equals Res.Rooms.Where(r => r.IdRoom = )
            string sql = "SELECT ro.*, re.* FROM Room ro " +
                                    "FULL JOIN RoomReservation rr ON ro.IdRoom = rr.IdRoom " +
                                    "LEFT JOIN Reservation re ON re.IdReservation = rr.IdReservation " +
                                    " WHERE " +
                                    "    ro.IdHotel = " + idHotel +
                                    "    AND(" +
                                    "        NOT(" +
                                    "            re.Arrival BETWEEN " + Darrival.ToString("yyyy-MM-dd HH:mm:ss.fff") + " AND " + Ddeparture.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " +
                                    "            OR re.Departure BETWEEN " + Darrival.ToString("yyyy-MM-dd HH:mm:ss.fff") + " AND " + Ddeparture.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " +
                                    "            OR(re.Arrival <= " + Darrival.ToString("yyyy-MM-dd HH:mm:ss.fff") + " AND re.Departure >= " + Ddeparture.ToString("yyyy-MM-dd HH:mm:ss.fff") + ") " +
                                    "        ) " +
                                    "        OR re.Arrival IS NULL " +
                                    "     )";

            return db.Rooms.SqlQuery("SELECT ro.*, re.* FROM Room ro" +
                                    " FULL JOIN RoomReservation rr ON ro.IdRoom = rr.IdRoom" +
                                    " LEFT JOIN Reservation re ON re.IdReservation = rr.IdReservation " +
                                    " WHERE " +
                                    "    ro.IdHotel = " + idHotel +
                                    "    AND(" +
                                    "        NOT(" +
                                    "            re.Arrival BETWEEN '" + Darrival.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + Ddeparture.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' " +
                                    "            OR re.Departure BETWEEN '" + Darrival.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + Ddeparture.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' " +
                                    "            OR(re.Arrival <= '" + Darrival.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND re.Departure >= '" + Ddeparture.ToString("yyyy-MM-dd HH:mm:ss.fff") + "') " +
                                    "        )" +
                                    "        OR re.Arrival IS NULL" +
                                    "     )").ToList();
        }
    }
}