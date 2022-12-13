delete Seat
where Seat.TypeOfVanId = 1
go
delete CellStructureVan
where CellStructureVan.TypeOfVanId = 1
go
declare @countblocks int, @i int;
set @countblocks = 8;
set @i = 0;
declare @numberOfSeat int;
set @numberOfSeat = 1;
while @i < @countblocks
begin
	declare @startRow int;
	Set @startRow = @i * 5;
	exec AddZeroRowsStructureVan 1, @startRow, 2, 4;
	insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(0, (@i * 5) + 2, 1, null);
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(1, (@i * 5) + 2, 1, null);
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(2, (@i * 5) + 2, 1, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(3, (@i * 5) + 2, 1, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
	Set @startRow = @i * 5 + 3;
	exec AddZeroRowsStructureVan 1, @startRow , 1, 4;
	insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(0, (@i * 5) + 4, 1, null);
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(1, (@i * 5) + 4, 1, null);
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(2, (@i * 5) + 4, 1, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
			insert CellStructureVan(NumberOfCell, NumberOfRow, TypeOfVanId, NumberOfSeatInVan)
			values
			(3, (@i * 5) + 4, 1, @numberOfSeat);
			set @numberOfSeat = @numberOfSeat + 1
			set @i = @i  + 1
end