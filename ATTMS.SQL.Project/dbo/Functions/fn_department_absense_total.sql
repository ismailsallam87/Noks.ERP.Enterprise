-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_department_absense_total
(
	@department_Id int
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
                         dbo.employee ON dbo.employee_shift.employee_Id = dbo.employee.Id INNER JOIN
                         dbo.department_job_employee_trns ON dbo.employee.Id = dbo.department_job_employee_trns.employee_Id INNER JOIN
                         dbo.department_job_ref ON dbo.department_job_employee_trns.department_job_Id_ref = dbo.department_job_ref.Id
	WHERE        (dbo.monthly_master_ref.is_current = 1) AND (dbo.attendance_sheet.is_absent = 1) AND (dbo.department_job_employee_trns.left_at IS NULL) AND (dbo.department_job_ref.department_Id = @department_Id))
	-- Return the result of the function
	RETURN ISNULL(@absense_total,0)

END