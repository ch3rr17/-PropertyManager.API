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
    public class UsersController : ApiController
    {
        private PropertiesDataContext db = new PropertiesDataContext();

        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            //return db.Users;

            var resultSet = db.Users.Select(u => new
            {
                u.UserId,
                u.FirstName,
                u.LastName,
                u.EmailAddress,
                u.IsLandlord,
                u.UserName
            });

            return Ok(resultSet);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            var dbUsers = db.Users.Find(id);

            // Map it to an anonymous object (To filter the columns)
            var mappedProperty = new
            {
                dbUsers.UserId,
                dbUsers.FirstName,
                dbUsers.LastName,
                dbUsers.EmailAddress,
                dbUsers.IsLandlord,
                dbUsers.UserName
            };

            // Return the _mapped_ property (the one with filtered columns)
            return Ok(mappedProperty);

        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}