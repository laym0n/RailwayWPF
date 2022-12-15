namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteTrack : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimesForStation", "TrackId", "dbo.Track");
            AddForeignKey("dbo.TimesForStation", "TrackId", "dbo.Track", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimesForStation", "TrackId", "dbo.Track");
            AddForeignKey("dbo.TimesForStation", "TrackId", "dbo.Track", "Id");
        }
    }
}
