namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StationTrainSchedule", "IdTrain", "dbo.Train");
            DropForeignKey("dbo.Track", "TrainId", "dbo.Train");
            DropForeignKey("dbo.Van", "TrainId", "dbo.Train");
            AddForeignKey("dbo.StationTrainSchedule", "IdTrain", "dbo.Train", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Track", "TrainId", "dbo.Train", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Van", "TrainId", "dbo.Train", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Van", "TrainId", "dbo.Train");
            DropForeignKey("dbo.Track", "TrainId", "dbo.Train");
            DropForeignKey("dbo.StationTrainSchedule", "IdTrain", "dbo.Train");
            AddForeignKey("dbo.Van", "TrainId", "dbo.Train", "Id");
            AddForeignKey("dbo.Track", "TrainId", "dbo.Train", "Id");
            AddForeignKey("dbo.StationTrainSchedule", "IdTrain", "dbo.Train", "Id");
        }
    }
}
