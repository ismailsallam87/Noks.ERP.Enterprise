-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_department_employee_count
(
	@department_Id int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @employee_count int =0

	-- Add the T-SQL statements to compute the return value here
	SET @employee_count =(SELECT TOP(1) COUNT(department_job_ref.department_Id) 
	FROM 
	department_job_employee_trns
	INNER JOIN department_job_ref ON dbo.department_job_employee_trns.department_job_Id_ref = dbo.department_job_ref.Id
	Where left_at IS NULL AND department_job_ref.department_Id=@department_Id)
	-- Return the result of the function
	RETURN @employee_count

END