namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSeatVanTypeOfVan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seat", "NumberInVan", "dbo.Van");
            DropForeignKey("dbo.Ticket", "SeatId", "dbo.Seat");
            DropIndex("dbo.Ticket", new[] { "SeatId" });
            DropIndex("dbo.Seat", new[] { "NumberInVan" });
            DropPrimaryKey("dbo.Seat", "PK_Seat");
            AddColumn("dbo.Ticket", "TypeOfVanId", c => c.Int(nullable: false));
            AddColumn("dbo.Seat", "TypeOfVanId", c => c.Int(nullable: false));
            AddColumn("dbo.Seat", "TypeOfSeatId", c => c.Int(nullable: false));
            AlterColumn("dbo.Seat", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Seat", "NumberInVan", c => c.Int());
            AlterColumn("dbo.Seat", "CostPerStation", c => c.Int());
            AddPrimaryKey("dbo.Seat", new[] { "Id", "TypeOfVanId" });
            CreateIndex("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" });
            CreateIndex("dbo.Seat", "TypeOfVanId");
            AddForeignKey("dbo.Seat", "TypeOfVanId", "dbo.TypeOfVan", "Id");
            AddForeignKey("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" }, "dbo.Seat", new[] { "Id", "TypeOfVanId" });
            DropColumn("dbo.Seat", "VanId");
            DropColumn("dbo.Seat", "TicketId");
            DropColumn("dbo.Van", "NumberOfAvailableSeats");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Van", "NumberOfAvailableSeats", c => c.Int(nullable: false));
            AddColumn("dbo.Seat", "TicketId", c => c.Int());
            AddColumn("dbo.Seat", "VanId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" }, "dbo.Seat");
            DropForeignKey("dbo.Seat", "TypeOfVanId", "dbo.TypeOfVan");
            DropIndex("dbo.Seat", new[] { "TypeOfVanId" });
            DropIndex("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" });
            DropPrimaryKey("dbo.Seat");
            AlterColumn("dbo.Seat", "CostPerStation", c => c.Int(nullable: false));
            AlterColumn("dbo.Seat", "NumberInVan", c => c.Int(nullable: false));
            AlterColumn("dbo.Seat", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Seat", "TypeOfSeatId");
            DropColumn("dbo.Seat", "TypeOfVanId");
            DropColumn("dbo.Ticket", "TypeOfVanId");
            AddPrimaryKey("dbo.Seat", "Id");
            CreateIndex("dbo.Seat", "NumberInVan");
            CreateIndex("dbo.Ticket", "SeatId");
            AddForeignKey("dbo.Ticket", "SeatId", "dbo.Seat", "Id");
            AddForeignKey("dbo.Seat", "NumberInVan", "dbo.Van", "Id");
        }
    }
}
