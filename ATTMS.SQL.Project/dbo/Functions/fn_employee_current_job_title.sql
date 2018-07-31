-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_employee_current_job_title
(
	@employee_Id int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @job_title nvarchar(50)

	-- Add the T-SQL statements to compute the return value here
	SET @job_title=(SELECT TOP(1) job_title.title
	FROM department_job_employee_trns 
	INNER JOIN department_job_ref ON department_job_employee_trns.department_job_Id_ref=department_job_ref.Id
	INNER JOIN job_title ON department_job_ref.job_title_Id = job_title.Id
	Where department_job_employee_trns.employee_Id = @employee_Id
	ORDER BY joined_at DESC)

	-- Return the result of the function
	RETURN @job_title

END