namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenderToPassenger : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passenger", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passenger", "Gender");
        }
    }
}
