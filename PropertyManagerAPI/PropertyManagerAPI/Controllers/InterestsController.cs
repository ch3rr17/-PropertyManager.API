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
using PropertyManagerAPI.Data;
using PropertyManagerAPI.Models;

namespace PropertyManagerAPI.Controllers
{
    public class InterestsController : ApiController
    {
        private PropertiesDataContext db = new PropertiesDataContext();

        // GET: api/Interests
        public IHttpActionResult GetInterests()
        {
            var resultSet = db.Interests.Select(u => new
            {
                u.UserId,
                u.PropertyId
            });

            return Ok(resultSet);
        }

        // GET: api/Interests/5
        [ResponseType(typeof(Interest))]
        public IHttpActionResult GetInterest(int id)
        {
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return NotFound();
            }

            return Ok(interest);
        }

        //GET: api/Properties/SearchInterests
        [Route("api/Properties/SearchInterests/{userId}")]
        public IHttpActionResult GetSearchInterests(int userId)
        {
            var resultSet = db.Interests.Where(i => i.UserId == userId);

            return Ok(resultSet.Select(i => new
            {
                i.Property.PropertyName,
                i.Property.Address1,
                i.Property.Address2,
                i.Property.City,
                i.Property.State,
                i.Property.ZipCode,
                i.Property.ContactPhone,
                i.Property.Rent,
                i.Property.SquareFootage,
                i.Property.IsPetFriendly,
                i.Property.LeaseTerm,
                i.Property.PropertyImage,
                i.Property.Bedroom,
                i.Property.Bathroom
            }));
        }

        //GET: PROPERTIES WITH INTERESTED USERS
        [Route("api/Properties/{propertyId}/InterestedProperties/{userId}")]
        public IHttpActionResult GetInterestedProperties(int userId, int propertyId)
        {
            var resultSet = db.Interests.Where(i => i.UserId == userId && i.PropertyId == propertyId);

            return Ok(resultSet.Select(i => new
            {
                i.Property.PropertyName,
                i.User.FirstName,
                i.User.LastName
            }));
        }


        // PUT: api/Interests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInterest(int id, Interest interest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interest.UserId)
            {
                return BadRequest();
            }

            db.Entry(interest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestExists(id))
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

        // POST: api/Interests
        [ResponseType(typeof(Interest))]
        public IHttpActionResult PostInterest(Interest interest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interests.Add(interest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InterestExists(interest.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = interest.UserId }, interest);
        }


        [HttpDelete]
        [Route("api/Interests/{propertyId}/Interests/{userId}")]
        public IHttpActionResult DeleteInterestToProperty(int propertyId, int userId)
        {
            //Interest interest = db.Interests.Find(propertyId, userId);

            Interest interest = db.Interests.FirstOrDefault(i => i.PropertyId == propertyId && i.UserId == userId);



            if (interest == null)
            {
                return NotFound();
            }

            db.Interests.Remove(interest);
            db.SaveChanges();

            return Ok(interest);
        }

        // DELETE: api/Interests/5
        [ResponseType(typeof(Interest))]
        public IHttpActionResult DeleteInterest(int id)
        {
            Interest interest = db.Interests.Find(id);
            if (interest == null)
            {
                return NotFound();
            }

            db.Interests.Remove(interest);
            db.SaveChanges();

            return Ok(interest);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InterestExists(int id)
        {
            return db.Interests.Count(e => e.UserId == id) > 0;
        }
    }
}