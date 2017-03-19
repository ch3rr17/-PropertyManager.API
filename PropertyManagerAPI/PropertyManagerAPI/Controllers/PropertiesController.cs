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
    public class PropertiesController : ApiController
    {
        private PropertiesDataContext db = new PropertiesDataContext();

        // GET: api/Properties
        public IHttpActionResult GetProperties()
        {
            var resultSet = db.Properties.Select(p => new
            {
                p.UserId,
                p.PropertyName,
                p.City,
                p.State,
                p.ZipCode,
                p.Rent,
                p.SquareFootage,
                p.IsPetFriendly,
                p.LeaseTerm,
                p.PropertyImage,
                p.Bedroom,
                p.Bathroom
            });

            return Ok(resultSet);
        }

        // GET: api/Properties/5
        [ResponseType(typeof(Property))]
        public IHttpActionResult GetProperty(int id)
        {
            // Get the property from SQL
            var dbProperty = db.Properties.Find(id);

            // Map it to an anonymous object (To filter the columns)
            var mappedProperty = new
            {
                dbProperty.UserId,
                dbProperty.PropertyName,
                dbProperty.Address1,
                dbProperty.Address2,
                dbProperty.City,
                dbProperty.State,
                dbProperty.ZipCode,
                dbProperty.ContactPhone,
                dbProperty.Rent,
                dbProperty.SquareFootage,
                dbProperty.IsPetFriendly,
                dbProperty.LeaseTerm,
                dbProperty.PropertyImage,
                dbProperty.Bedroom,
                dbProperty.Bathroom
            };

            // Return the _mapped_ property (the one with filtered columns)
            return Ok(mappedProperty);

        }

        [Route("api/Properties/GetSearchPropertiesByUser")]
        public IHttpActionResult GetSearchPropertiesByUser(string username)
        {
            var searchedUser = db.Users.FirstOrDefault(u => u.UserName == username);

            IQueryable<Property> resultSet = db.Properties.Where(p => p.UserId == searchedUser.UserId);

            return Ok(resultSet.Select(p => new
            {
                p.UserId,
                p.PropertyName,
                p.Address1,
                p.Address2,
                p.City,
                p.State,
                p.ZipCode,
                p.ContactPhone,
                p.Rent,
                p.SquareFootage,
                p.IsPetFriendly,
                p.LeaseTerm,
                p.PropertyImage,
                p.Bedroom,
                p.Bathroom
            }));
        }

        //Users(tenants) can search for a property
        [Route("api/Properties/GetSearchProperties")]
        public IHttpActionResult GetSearchProperties([FromUri] PropertySearch search)
        {
            IQueryable<Property> resultSet = db.Properties;

            if(!string.IsNullOrEmpty(search.City))
            {
                resultSet = resultSet.Where(p => p.City == search.City);
            }

            if(!string.IsNullOrEmpty(search.ZipCode))
            {
                resultSet = resultSet.Where(p => p.ZipCode == search.ZipCode);
            }

            if(search.MinimumRent > 0 || search.MaximumRent > 0)
            {
                resultSet = resultSet.Where(p => p.Rent >= search.MinimumRent || p.Rent <= search.MaximumRent);
            }

            if(search.IsPetFriendly)
            {
                resultSet = resultSet.Where(p => p.IsPetFriendly == true);
            }

            return Ok(resultSet.Select(p => new
            {
                p.UserId,
                p.PropertyName,
                p.Address1,
                p.Address2,
                p.City,
                p.State,
                p.ZipCode,
                p.ContactPhone,
                p.Rent,
                p.SquareFootage,
                p.IsPetFriendly,
                p.LeaseTerm,
                p.PropertyImage,
                p.Bedroom,
                p.Bathroom
            }));

        }



        // PUT: api/Properties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                return BadRequest();
            }

            db.Entry(property).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        [ResponseType(typeof(Property))]
        public IHttpActionResult PostProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Properties.Add(property);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
        }

        // DELETE: api/Properties/5
        [ResponseType(typeof(Property))]
        public IHttpActionResult DeleteProperty(int id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(property);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}