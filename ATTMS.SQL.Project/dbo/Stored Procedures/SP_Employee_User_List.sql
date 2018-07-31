-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE dbo.SP_Employee_User_List
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT Id,UserName
	FROM AspNetUsers Where Id NOT IN(SELECT ISNULL(user_Id,'no code found') from employee)
END