-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_userName_by_Employee
(
	@employee_Id int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @userName nvarchar(50)

	-- Add the T-SQL statements to compute the return value here
	SET @userName=(SELECT TOP(1) UserName
	From AspNetUsers Where (Id = (SELECT TOP(1) user_Id from employee where Id=@employee_Id)))
	-- Return the result of the function
	RETURN @userName

END