-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_department_employee_photos]
(
	-- Add the parameters for the function here
	@department_Id int
)
RETURNS nvarchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @photos nvarchar(max) ='';
	DECLARE @employee_photo nvarchar(250) = '';
	-- Add the T-SQL statements to compute the return value here

	DECLARE employee_photos_cursor cursor for
	SELECT        dbo.employee.photo
	FROM            dbo.department_job_employee_trns INNER JOIN
                         dbo.employee ON dbo.department_job_employee_trns.employee_Id = dbo.employee.Id INNER JOIN
                         dbo.department_job_ref ON dbo.department_job_employee_trns.department_job_Id_ref = dbo.department_job_ref.Id
	WHERE        (dbo.department_job_employee_trns.left_at IS NULL) AND (dbo.department_job_ref.department_Id = @department_Id)
	GROUP BY dbo.employee.name, dbo.employee.photo

	OPEN employee_photos_cursor
	FETCH NEXT FROM employee_photos_cursor INTO @employee_photo
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @photos = @photos+@employee_photo+','

	FETCH NEXT FROM employee_photos_cursor INTO @employee_photo
	END
	CLOSE employee_photos_cursor
	DEALLOCATE employee_photos_cursor
	-- Return the result of the function
	RETURN @photos

END