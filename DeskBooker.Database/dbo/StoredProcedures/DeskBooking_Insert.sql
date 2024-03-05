CREATE PROCEDURE [dbo].[DeskBooking_Insert]
	@FirstName NVARCHAR(35),
	@LastName NVARCHAR(35),
	@Email NVARCHAR(254),
	@Date DATETIME,
	@DeskId INT
AS
BEGIN
	INSERT INTO [dbo].[DeskBooking] (FirstName, LastName, Email, Date, DeskId)
	VALUES (@FirstName, @LastName, @Email, @Date, @DeskId)
END