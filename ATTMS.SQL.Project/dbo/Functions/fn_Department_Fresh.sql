-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION fn_Department_Fresh
(
	@department_id int
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @is_fresh bit

	-- Add the T-SQL statements to compute the return value here
	IF (SELECT TOP(1) Id from department_job_ref where department_Id=@department_id) IS NULL BEGIN
		SET @is_fresh = 1
	END ELSE BEGIN
		SET @is_fresh = 0
	END

	-- Return the result of the function
	RETURN @is_fresh

END