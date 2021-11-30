CREATE PROCEDURE [dbo].[spOrder_UpdateStatus]
	@Id int,
	@status varchar(16)
AS
BEGIN
	UPDATE dbo.[Order]
	SET dbo.[Order].[Status] = @status, dbo.[Order].UpdatedDate = SYSDATETIMEOFFSET()
	WHERE Id = @Id
END