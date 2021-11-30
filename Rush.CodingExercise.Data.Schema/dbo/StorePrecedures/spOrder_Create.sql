CREATE PROCEDURE [dbo].[spOrder_Create]
	@customerId bigint,
	@orderNumber varchar(16),
	@status varchar(16)
AS
BEGIN
	INSERT INTO dbo.[Order] (CustomerId,OrderNumber,[Status])
	VALUES (@customerId,@orderNumber,@status)

	DECLARE @newId AS bigint
	SELECT @newId = SCOPE_IDENTITY()

	SELECT 
	dbo.[Order].[Id],
	dbo.[Order].CustomerId,
	dbo.[Order].OrderNumber,
	dbo.[Order].[Status],
	dbo.[Order].Total,
	dbo.[Order].CreateDate,
	dbo.[Order].UpdatedDate 
	FROM dbo.[Order] 
	WHERE dbo.[Order].Id = @newId

END