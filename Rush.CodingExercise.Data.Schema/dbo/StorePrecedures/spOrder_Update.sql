CREATE PROCEDURE [dbo].[spOrder_Update]
	@orderNumber varchar(16),
	@total decimal(10,2),
	@status varchar(16)
AS
BEGIN
	UPDATE dbo.[Order]
	SET dbo.[Order].Total = @total, dbo.[Order].[Status] = @status, dbo.[Order].UpdatedDate = SYSDATETIMEOFFSET()
	WHERE dbo.[Order].OrderNumber = @orderNumber

	SELECT 
	dbo.[Order].[Id],
	dbo.[Order].CustomerId,
	dbo.[Order].OrderNumber,
	dbo.[Order].[Status],
	dbo.[Order].Total,
	dbo.[Order].CreateDate,
	dbo.[Order].UpdatedDate 
	FROM dbo.[Order] 
	WHERE dbo.[Order].OrderNumber = @orderNumber

END