-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Emplyee_Fresh]
(
	@Employee_id int
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @is_related bit

	-- Add the T-SQL statements to compute the return value here
	IF
	( (SELECT TOP(1) Id from department_job_employee_trns where employee_Id=@Employee_id) IS NULL
	and
 
 (SELECT TOP(1) Id from employee_shift where employee_Id=@Employee_id) IS NULL)

	 BEGIN
		SET @is_related = 1
	END ELSE BEGIN
		SET @is_related = 0
	END

	-- Return the result of the function
	RETURN @is_related

END