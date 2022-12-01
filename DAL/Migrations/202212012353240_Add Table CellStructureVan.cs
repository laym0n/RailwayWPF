namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCellStructureVan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" }, "dbo.Seat");
            DropIndex("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" });
            DropPrimaryKey("dbo.Seat");
            CreateTable(
                "dbo.CellStructureVan",
                c => new
                    {
                        NumberOfCell = c.Int(nullable: false),
                        NumberOfRow = c.Int(nullable: false),
                        TypeOfVanId = c.Int(nullable: false),
                        NumberOfSeatInVan = c.Int(),
                    })
                .PrimaryKey(t => new { t.NumberOfCell, t.NumberOfRow, t.TypeOfVanId })
                .ForeignKey("dbo.TypeOfVan", t => t.TypeOfVanId)
                .Index(t => t.TypeOfVanId);
            
            AddColumn("dbo.Seat", "NumberOfSeatInVan", c => c.Int(nullable: false));
            AlterColumn("dbo.Seat", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Seat", "CostPerStation", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Seat", "Id");
            CreateIndex("dbo.Ticket", "SeatId");
            AddForeignKey("dbo.Ticket", "SeatId", "dbo.Seat", "Id");
            DropColumn("dbo.Ticket", "TypeOfVanId");
            DropColumn("dbo.Seat", "NumberInVan");
            DropColumn("dbo.Seat", "TypeOfSeatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seat", "TypeOfSeatId", c => c.Int(nullable: false));
            AddColumn("dbo.Seat", "NumberInVan", c => c.Int());
            AddColumn("dbo.Ticket", "TypeOfVanId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ticket", "SeatId", "dbo.Seat");
            DropForeignKey("dbo.CellStructureVan", "TypeOfVanId", "dbo.TypeOfVan");
            DropIndex("dbo.Ticket", new[] { "SeatId" });
            DropIndex("dbo.CellStructureVan", new[] { "TypeOfVanId" });
            DropPrimaryKey("dbo.Seat");
            AlterColumn("dbo.Seat", "CostPerStation", c => c.Int());
            AlterColumn("dbo.Seat", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Seat", "NumberOfSeatInVan");
            DropTable("dbo.CellStructureVan");
            AddPrimaryKey("dbo.Seat", new[] { "Id", "TypeOfVanId" });
            CreateIndex("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" });
            AddForeignKey("dbo.Ticket", new[] { "SeatId", "TypeOfVanId" }, "dbo.Seat", new[] { "Id", "TypeOfVanId" });
        }
    }
}
