namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveWorkerIdFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "WorkerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "WorkerId", c => c.Int());
        }
    }
}
