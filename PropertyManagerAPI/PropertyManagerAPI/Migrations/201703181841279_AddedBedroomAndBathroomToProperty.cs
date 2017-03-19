namespace PropertyManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBedroomAndBathroomToProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "Bedroom", c => c.Int(nullable: false));
            AddColumn("dbo.Properties", "Bathroom", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "Bathroom");
            DropColumn("dbo.Properties", "Bedroom");
        }
    }
}
