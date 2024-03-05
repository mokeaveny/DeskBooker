CREATE PROCEDURE [dbo].[DeskBooking_Delete]
	@Id INT
AS
BEGIN
	DELETE FROM [dbo].[DeskBooking]  
	WHERE Id = @Id;
END