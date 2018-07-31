-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_shift_workedhours_total]
(
	@shift_Id int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @workedhours_total int =0

	-- Add the T-SQL statements to compute the return value here
	SET @workedhours_total =(
	
	SELECT      

	SUM( DATEPART(SECOND, dbo.attendance_sheet.worked_hours) * 0 + 
              DATEPART(MINUTE, dbo.attendance_sheet.worked_hours) * 0 +
              DATEPART(HOUR, dbo.attendance_sheet.worked_hours ) 
            ) as Total_Worked_Hours

	FROM            dbo.monthly_master_ref INNER JOIN
                         dbo.attendance_sheet ON dbo.monthly_master_ref.Id = dbo.attendance_sheet.monthly_master_ref_Id INNER JOIN
                         dbo.employee_shift ON dbo.attendance_sheet.employee_shift_Id = dbo.employee_shift.Id INNER JOIN
                         dbo.employee ON dbo.employee_shift.employee_Id = dbo.employee.Id
	WHERE        (dbo.monthly_master_ref.is_current = 1) AND (dbo.attendance_sheet.is_absent <> 1) AND (dbo.employee_shift.shift_Id = @shift_Id)
)
	-- Return the result of the function
	RETURN ISNULL(@workedhours_total,0)

END