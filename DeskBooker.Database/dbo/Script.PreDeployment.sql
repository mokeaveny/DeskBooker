IF OBJECT_ID(N'dbo.DeskBooking', N'U') IS NOT NULL
BEGIN
	DELETE [dbo].[DeskBooking]
END

IF OBJECT_ID(N'dbo.Desk', N'U') IS NOT NULL
BEGIN
	DELETE [dbo].[Desk]
END

DBCC CHECKIDENT('dbo.DeskBooking', RESEED, 0)
DBCC CHECKIDENT('dbo.Desk', RESEED, 0)