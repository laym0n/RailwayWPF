namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPassenger : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passenger", "Birthday", c => c.DateTime(nullable: false));
            DropColumn("dbo.Passenger", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passenger", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Passenger", "Birthday");
        }
    }
}
