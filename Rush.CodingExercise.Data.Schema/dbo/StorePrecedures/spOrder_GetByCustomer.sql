CREATE PROCEDURE [dbo].[spOrder_GetByCustomer]
	@customerId int,
	@dateRangeFrom datetime,
	@dateRangeTo datetime
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
	WHERE dbo.[Order].CustomerId = @customerId AND (dbo.[Order].CreateDate >=@dateRangeFrom and dbo.[Order].CreateDate <= @dateRangeTo)
END
