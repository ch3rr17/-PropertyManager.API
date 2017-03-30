namespace PropertyManagerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPhoneTypeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "ContactPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "ContactPhone", c => c.Int(nullable: false));
        }
    }
}
