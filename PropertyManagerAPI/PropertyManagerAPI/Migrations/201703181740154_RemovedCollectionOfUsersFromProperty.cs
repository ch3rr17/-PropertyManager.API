namespace PropertyManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCollectionOfUsersFromProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Property_PropertyId", "dbo.Properties");
            DropIndex("dbo.Users", new[] { "Property_PropertyId" });
            DropColumn("dbo.Users", "Property_PropertyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Property_PropertyId", c => c.Int());
            CreateIndex("dbo.Users", "Property_PropertyId");
            AddForeignKey("dbo.Users", "Property_PropertyId", "dbo.Properties", "PropertyId");
        }
    }
}
