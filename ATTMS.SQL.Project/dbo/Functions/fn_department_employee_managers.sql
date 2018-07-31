-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_department_employee_managers]
(
	-- Add the parameters for the function here
	@department_Id int
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @managers nvarchar(max) ='';
	DECLARE @manager_name nvarchar(250) = '';
	DECLARE @manager_Id nvarchar(250) = '';
	DECLARE @manager_JobTitle nvarchar(250) = '';
	-- Add the T-SQL statements to compute the return value here

	DECLARE employee_managers_cursor cursor for
	SELECT        dbo.job_title.title, dbo.employee.name, CAST(dbo.employee.Id as nvarchar(50))
	FROM            dbo.department_job_employee_trns INNER JOIN
                         dbo.employee ON dbo.department_job_employee_trns.employee_Id = dbo.employee.Id INNER JOIN
                         dbo.department_job_ref ON dbo.department_job_employee_trns.department_job_Id_ref = dbo.department_job_ref.Id INNER JOIN
                         dbo.job_title ON dbo.department_job_ref.job_title_Id = dbo.job_title.Id
	WHERE        (dbo.department_job_employee_trns.left_at IS NULL) AND (dbo.department_job_ref.department_Id = @department_Id) AND (dbo.department_job_ref.is_manager = 1)
	GROUP BY dbo.job_title.title, dbo.employee.name, dbo.employee.Id

	OPEN employee_managers_cursor
	FETCH NEXT FROM employee_managers_cursor INTO @manager_JobTitle,@manager_name,@manager_Id
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @managers = @managers+@manager_Id+';'+@manager_name+';'+@manager_JobTitle+','

	FETCH NEXT FROM employee_managers_cursor INTO @manager_JobTitle,@manager_name,@manager_Id
	END
	CLOSE employee_managers_cursor
	DEALLOCATE employee_managers_cursor
	-- Return the result of the function
	RETURN @managers

END