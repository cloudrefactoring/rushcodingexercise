CREATE PROCEDURE [dbo].[spOrder_GetByOrderNumber]
	@orderNumber varchar(16)
AS
BEGIN
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
