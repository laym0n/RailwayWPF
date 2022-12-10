namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNULLAbleToTrainForUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Train", new[] { "IdUserCreator" });
            AlterColumn("dbo.Train", "IdUserCreator", c => c.Int());
            CreateIndex("dbo.Train", "IdUserCreator");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Train", new[] { "IdUserCreator" });
            AlterColumn("dbo.Train", "IdUserCreator", c => c.Int(nullable: false));
            CreateIndex("dbo.Train", "IdUserCreator");
        }
    }
}
