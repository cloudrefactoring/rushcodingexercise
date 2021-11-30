CREATE PROCEDURE [dbo].[spOrder_GetByCustomerAndOrderStatus]
	@customerId int,
	@status varchar(16)
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
	WHERE dbo.[Order].CustomerId = @customerId AND dbo.[Order].Status = @status
END
