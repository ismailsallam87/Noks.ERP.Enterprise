-- =============================================

-- =============================================
CREATE PROCEDURE Sp_Change_Status
@status bit, @id int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
update [dbo].[shift] set status= @status where Id = @id;

END