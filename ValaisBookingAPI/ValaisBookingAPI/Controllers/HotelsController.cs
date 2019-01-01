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
    }
}