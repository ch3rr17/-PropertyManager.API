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
        public IDbSet<Interest> Interests { get; set; }

        //ON MODEL CREATING - RELATIONSHIPS
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                        .HasMany(i => i.Interests)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Property>()
                        .HasMany(i => i.Interests)
                        .WithRequired(p => p.Property)
                        .HasForeignKey(p => p.PropertyId)
                        .WillCascadeOnDelete(false); //allows an interest to be deleted without deleting a property

            modelBuilder.Entity<User>()
                        .HasMany(p => p.Properties)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                        .HasMany(ps => ps.PropertySearches)
                        .WithRequired(u => u.User)
                        .HasForeignKey(u => u.UserId);

            //Compund Key for Interest table
            modelBuilder.Entity<Interest>()
                        .HasKey(i => new { i.UserId, i.PropertyId });

        
            base.OnModelCreating(modelBuilder);
        }

    }
}