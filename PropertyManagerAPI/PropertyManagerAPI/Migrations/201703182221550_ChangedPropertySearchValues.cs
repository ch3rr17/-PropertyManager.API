namespace PropertyManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropertySearchValues : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropertySearches", "MinimumRent", c => c.Int());
            AlterColumn("dbo.PropertySearches", "MaximumRent", c => c.Int());
            AlterColumn("dbo.PropertySearches", "SquareFootage", c => c.Int());
            AlterColumn("dbo.PropertySearches", "Bedroom", c => c.Int());
            AlterColumn("dbo.PropertySearches", "Bathroom", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropertySearches", "Bathroom", c => c.Int(nullable: false));
            AlterColumn("dbo.PropertySearches", "Bedroom", c => c.Int(nullable: false));
            AlterColumn("dbo.PropertySearches", "SquareFootage", c => c.Int(nullable: false));
            AlterColumn("dbo.PropertySearches", "MaximumRent", c => c.Int(nullable: false));
            AlterColumn("dbo.PropertySearches", "MinimumRent", c => c.Int(nullable: false));
        }
    }
}
