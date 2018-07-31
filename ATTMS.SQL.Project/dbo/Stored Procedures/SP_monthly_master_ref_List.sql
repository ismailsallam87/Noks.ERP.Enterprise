-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_monthly_master_ref_List
	@Is_Current bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM 
	monthly_master_ref
	Where (@Is_Current IS NULL OR monthly_master_ref.is_current =@Is_Current)
	AND (monthly_master_ref.deleted <> 1)
	ORDER BY  start_from DESC
END