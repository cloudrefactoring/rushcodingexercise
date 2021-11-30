CREATE PROCEDURE [dbo].[spOrder_UpdateTotal]
	@Id int,
	@total decimal(10,2)
AS
BEGIN
	UPDATE dbo.[Order]
	SET dbo.[Order].Total = @total, dbo.[Order].UpdatedDate = SYSDATETIMEOFFSET()
	WHERE Id = @Id
END