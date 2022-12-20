namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferenceToUserFromTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "UserId", c => c.Int(nullable: true));
            CreateIndex("dbo.Ticket", "UserId");
            AddForeignKey("dbo.Ticket", "UserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "UserId", "dbo.User");
            DropIndex("dbo.Ticket", new[] { "UserId" });
            DropColumn("dbo.Ticket", "UserId");
        }
    }
}
