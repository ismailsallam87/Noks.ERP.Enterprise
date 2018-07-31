-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE enroll_employee_device
	@employee_code nvarchar(50),
	@employee_no int,
	@device_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Employee_Id int = (SELECT TOP(1) Id from employee where code= RTRIM(LTRIM(@employee_code)))
	IF @employee_Id IS NOT NULL BEGIN
		INSERT INTO employee_device_log VALUES(@Employee_Id,@employee_no,@device_id)
	END
END