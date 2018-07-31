-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Shift_Stat_Details 
	@shift_Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Stats TABLE (Total_Worked_Hours int,ys_Absence int,ys_Deductions int,ys_workedhours int,ys_Overtime int)

	DECLARE @yesterday datetime=DATEADD(day,-1,GETDATE())

	INSERT INTO @Stats VALUES
	(
	[dbo].[fn_shift_workedhours_total](@shift_Id),
	[dbo].[fn_shift_absense_total](@shift_Id,@yesterday),
	[dbo].[fn_shift_deductions_total](@shift_Id,@yesterday),
	[dbo].[fn_shift_workedhours_day_total](@shift_Id,@yesterday),
	[dbo].[fn_shift_overtime_day_total](@shift_Id,@yesterday)
	)

	SELECT * FROM @Stats
END