delete Seat
where Seat.TypeOfVanId = 3
go
delete CellStructureVan
where CellStructureVan.TypeOfVanId = 3
go
declare @countblocks int, @i int;
set @countblocks = 10;
set @i = 0;
declare @numberOfSeat int;
set @numberOfSeat = 1;
while @i < @countblocks
begin
	declare @startRow int;
	Set @startRow = @i * 2;
	exec AddZeroRowsStructureVan 3, @startRow, 1, 5;
	Set @startRow = @i * 2 + 1;
	insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(0, @startRow, 3, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(1, @startRow, 3, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(2, @startRow, 3, null);
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(3, @startRow, 3, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(4, @startRow, 3, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
		set @i = @i + 1;
end