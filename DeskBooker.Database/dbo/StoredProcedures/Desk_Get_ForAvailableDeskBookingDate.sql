CREATE PROCEDURE [dbo].[Desk_Get_ForAvailableDeskBookingDate]
	@Date DATETIME
AS
BEGIN
	SELECT D.Id FROM [dbo].[Desk] D
	INNER JOIN [dbo].[DeskBooking] DB ON D.Id = DB.DeskId
	WHERE CAST(DB.Date AS Date) = CAST(@Date AS Date);
END
