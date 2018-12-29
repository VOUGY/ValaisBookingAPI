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
    public class PicturesController : ApiController
    {
        private ValaisBookingEntities db = new ValaisBookingEntities();

        // GET: api/Hotel/{id}/Pictures
        [Route("api/Hotels/{id}/Pictures")]
        [HttpGet]
        public IQueryable<Picture> GetPicturesForHotel(int id)
        {
            IQueryable<Picture> pictures = db.Rooms.Where(r => r.IdHotel == id).SelectMany(r=>db.Pictures);

            return pictures;
        }

        // GET: api/rooms/{id}/Pictures
        [Route("api/rooms/{id}/Pictures")]
        [HttpGet]
        public IQueryable<Picture> GetPicturesForRoom(int id)
        {
            IQueryable<Picture> pictures = db.Rooms.Where(r => r.IdRoom == id).SelectMany(r => db.Pictures);

            return pictures;
        }

    }
}