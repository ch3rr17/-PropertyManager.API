using PropertyManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PropertyManagerAPI.Data
{
    public class PropertiesDataContext : DbContext
    {
        public PropertiesDataContext() : base("PropertyManager")
        {

        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Property> Properties { get; set; }
        public IDbSet<PropertySearch> PropertySearches { get; set;  }

        //ON MODEL CREATING - RELATIONSHIPS
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(ps => ps.PropertySearches)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);
        
        
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Properties)
                        .WithMany(p => p.Users)
                        .Map(ts =>
                        {
                            ts.MapLeftKey("UserId");
                            ts.MapRightKey("PropertyId");
                            ts.ToTable("Interests");
                        });
            


            base.OnModelCreating(modelBuilder);
        }

    }
}