-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_employee_current_department_title]
(
	@employee_Id int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @department_title nvarchar(50)

	-- Add the T-SQL statements to compute the return value here
	SET @department_title=(SELECT TOP(1) department.title
	FROM department_job_employee_trns 
	INNER JOIN department_job_ref ON department_job_employee_trns.department_job_Id_ref=department_job_ref.Id
	INNER JOIN department ON department_job_ref.department_Id = department.Id
	Where department_job_employee_trns.employee_Id = @employee_Id
	ORDER BY joined_at DESC)

	-- Return the result of the function
	RETURN @department_title

END