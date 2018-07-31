-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Shifts_Select]
	@shift_id int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
	shift.Id,shift.title,shift.status,shift.check_in,shift.check_out,shift.work_hours,
	dbo.fn_shfit_employees_count(shift.id) as employees_count
	FROM shift
	Where (@shift_id IS NULL OR shift.Id=@shift_id)
	order by shift.title
END