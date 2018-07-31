-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Import_FingerList
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

DECLARE @employee_name nvarchar(50)
DECLARE @employee_code nvarchar(50)
DECLARE curs CURSOR
FOR
SELECT Employee_Name,Emp_No From Finger_Print_List

OPEN curs
FETCH NEXT FROM curs into @employee_name,@employee_code
WHILE @@FETCH_STATUS = 0
BEGIN
DECLARE @Exist int = (SELECT TOP(1) id from employee where code=LTRIM(Rtrim(@employee_code)))
IF @Exist IS NULL BEGIN
INSERT INTO employee (name,code) values (@employee_name,LTRIM(Rtrim(@employee_code))) END

FETCH NEXT FROM curs into @employee_name,@employee_code
END
CLOSE curs
DEALLOCATE curs

END