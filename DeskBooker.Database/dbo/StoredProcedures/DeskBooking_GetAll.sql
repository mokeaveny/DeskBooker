CREATE PROCEDURE [dbo].[DeskBooking_GetAll]
AS
BEGIN
	SELECT Id, FirstName, LastName, Email, Date, DeskId 
	FROM [dbo].[DeskBooking]
END