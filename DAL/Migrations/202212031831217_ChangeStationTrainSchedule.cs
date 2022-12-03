namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStationTrainSchedule : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.TimesForStation", new[] { "IdStationTrainSchedule", "IdStation" }, "dbo.StationTrainSchedule");
            DropIndex("dbo.StationTrainSchedule", new[] { "IdStation" });
            DropIndex("dbo.StationTrainSchedule", new[] { "IdTrain" });
            DropIndex("dbo.TimesForStation", new[] { "IdStationTrainSchedule", "IdStation" });
            //DropPrimaryKey("dbo.StationTrainSchedule");
            AlterColumn("dbo.StationTrainSchedule", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StationTrainSchedule", "Id");
            CreateIndex("dbo.StationTrainSchedule", new[] { "IdStation", "IdTrain" }, unique: true, name: "UQ_FirstAndSecond");
            CreateIndex("dbo.TimesForStation", "IdStationTrainSchedule");
            AddForeignKey("dbo.TimesForStation", "IdStationTrainSchedule", "dbo.StationTrainSchedule", "Id");
            DropColumn("dbo.TimesForStation", "IdStation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimesForStation", "IdStation", c => c.Int(nullable: false));
            DropForeignKey("dbo.TimesForStation", "IdStationTrainSchedule", "dbo.StationTrainSchedule");
            DropIndex("dbo.TimesForStation", new[] { "IdStationTrainSchedule" });
            DropIndex("dbo.StationTrainSchedule", "UQ_FirstAndSecond");
            DropPrimaryKey("dbo.StationTrainSchedule");
            AlterColumn("dbo.StationTrainSchedule", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StationTrainSchedule", new[] { "Id", "IdStation" });
            CreateIndex("dbo.TimesForStation", new[] { "IdStationTrainSchedule", "IdStation" });
            CreateIndex("dbo.StationTrainSchedule", "IdTrain");
            CreateIndex("dbo.StationTrainSchedule", "IdStation");
            AddForeignKey("dbo.TimesForStation", new[] { "IdStationTrainSchedule", "IdStation" }, "dbo.StationTrainSchedule", new[] { "Id", "IdStation" });
        }
    }
}
