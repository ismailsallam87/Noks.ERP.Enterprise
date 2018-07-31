-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_shfit_employees_count
(
	@shift_id int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @employee_count int

	-- Add the T-SQL statements to compute the return value here
	SET @employee_count= (SELECT COUNT(shift_id) from employee_shift where shift_id = @shift_id
	AND left_at IS NULL
	)

	-- Return the result of the function
	RETURN @employee_count

END