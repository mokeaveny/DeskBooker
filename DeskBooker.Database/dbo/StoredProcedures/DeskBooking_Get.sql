CREATE PROCEDURE [dbo].[DeskBooking_Get]
	@Id INT
AS
BEGIN
	SELECT Id, FirstName, LastName, Email, Date, DeskId 
	FROM [dbo].[DeskBooking]  
	WHERE Id = @Id
END