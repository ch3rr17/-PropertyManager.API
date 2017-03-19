namespace PropertyManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.PropertyId })
                .ForeignKey("dbo.Properties", t => t.PropertyId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PropertyId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PropertyName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        ContactPhone = c.Int(nullable: false),
                        Rent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SquareFootage = c.Int(nullable: false),
                        IsPetFriendly = c.Boolean(nullable: false),
                        LeaseTerm = c.Int(nullable: false),
                        PropertyImage = c.String(),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        IsLandlord = c.Boolean(nullable: false),
                        UserName = c.String(nullable: false),
                        Property_PropertyId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Properties", t => t.Property_PropertyId)
                .Index(t => t.Property_PropertyId);
            
            CreateTable(
                "dbo.PropertySearches",
                c => new
                    {
                        PropertySearchId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        City = c.String(),
                        ZipCode = c.String(),
                        MinimumRent = c.Int(nullable: false),
                        MaximumRent = c.Int(nullable: false),
                        SquareFootage = c.Int(nullable: false),
                        IsPetFriendly = c.Boolean(nullable: false),
                        Bedroom = c.Int(nullable: false),
                        Bathroom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertySearchId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Property_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertySearches", "UserId", "dbo.Users");
            DropForeignKey("dbo.Properties", "UserId", "dbo.Users");
            DropForeignKey("dbo.Interests", "UserId", "dbo.Users");
            DropForeignKey("dbo.Interests", "PropertyId", "dbo.Properties");
            DropIndex("dbo.PropertySearches", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Property_PropertyId" });
            DropIndex("dbo.Properties", new[] { "UserId" });
            DropIndex("dbo.Interests", new[] { "PropertyId" });
            DropIndex("dbo.Interests", new[] { "UserId" });
            DropTable("dbo.PropertySearches");
            DropTable("dbo.Users");
            DropTable("dbo.Properties");
            DropTable("dbo.Interests");
        }
    }
}
