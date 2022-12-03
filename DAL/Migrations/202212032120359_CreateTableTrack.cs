namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTrack : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimesForStation", "IdStationTrainSchedule", "dbo.StationTrainSchedule");
            DropIndex("dbo.TimesForStation", new[] { "IdStationTrainSchedule" });
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Train", t => t.TrainId)
                .Index(t => t.TrainId);
            
            AddColumn("dbo.TimesForStation", "TrackId", c => c.Int(nullable: false));
            CreateIndex("dbo.TimesForStation", "TrackId");
            AddForeignKey("dbo.TimesForStation", "TrackId", "dbo.Track", "Id");
            DropColumn("dbo.TimesForStation", "IdStationTrainSchedule");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimesForStation", "IdStationTrainSchedule", c => c.Int(nullable: false));
            DropForeignKey("dbo.Track", "TrainId", "dbo.Train");
            DropForeignKey("dbo.TimesForStation", "TrackId", "dbo.Track");
            DropIndex("dbo.TimesForStation", new[] { "TrackId" });
            DropIndex("dbo.Track", new[] { "TrainId" });
            DropColumn("dbo.TimesForStation", "TrackId");
            DropTable("dbo.Track");
            CreateIndex("dbo.TimesForStation", "IdStationTrainSchedule");
            AddForeignKey("dbo.TimesForStation", "IdStationTrainSchedule", "dbo.StationTrainSchedule", "Id");
        }
    }
}
