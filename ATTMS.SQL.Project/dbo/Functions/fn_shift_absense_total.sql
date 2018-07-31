-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[fn_shift_absense_total]
(
	@shift_Id int,
	@day date
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @absense_total int =0

	-- Add the T-SQL statements to compute the return value here
	SET @absense_total =(
	
	SELECT   TOP(1)     COUNT(dbo.attendance_sheet.Id) AS absence_count
	FROM            dbo.monthly_master_ref INNER JOIN
                         dbo.attendance_sheet ON dbo.monthly_master_ref.Id = dbo.attendance_sheet.monthly_master_ref_Id INNER JOIN
                         dbo.employee_shift ON dbo.attendance_sheet.employee_shift_Id = dbo.employee_shift.Id INNER JOIN
                         dbo.employee ON dbo.employee_shift.employee_Id = dbo.employee.Id 
	WHERE        (dbo.attendance_sheet.log_day = @day) AND (dbo.attendance_sheet.is_absent = 1) AND (dbo.employee_shift.shift_Id = @shift_Id))
	-- Return the result of the function
	RETURN ISNULL(@absense_total,0)

END