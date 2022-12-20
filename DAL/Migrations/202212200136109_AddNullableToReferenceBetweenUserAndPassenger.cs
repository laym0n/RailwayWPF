namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableToReferenceBetweenUserAndPassenger : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Passenger", new[] { "UserId" });
            AlterColumn("dbo.Passenger", "UserId", c => c.Int());
            CreateIndex("dbo.Passenger", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Passenger", new[] { "UserId" });
            AlterColumn("dbo.Passenger", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Passenger", "UserId");
        }
    }
}
