-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_department_job_employee_count 
(
	@department_job_ref_Id int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @department_job_employees_count int

	-- Add the T-SQL statements to compute the return value here
	SET @department_job_employees_count=(SELECT        COUNT(department_job_Id_ref) AS Employee_Count
	FROM            dbo.department_job_employee_trns
	WHERE        (department_job_Id_ref = @department_job_ref_Id) AND (left_at IS NULL))

	-- Return the result of the function
	RETURN @department_job_employees_count

END