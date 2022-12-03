namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTypeOfTrain : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Train", new[] { "TypeOfTrainId" });
            DropTable("dbo.TypeOfTrain");
            DropColumn("dbo.Train", "TypeOfTrainId");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TypeOfTrain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            AddColumn("dbo.Train", "TypeOfTrainId", c => c.Int(nullable: false));
            CreateIndex("dbo.Train", "TypeOfTrainId");
            AddForeignKey("dbo.Train", "TypeOfTrainId", "dbo.TypeOfTrain", "Id");
        }
    }
}
