-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Edit_Employee

@Emp_id int

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select Id,[name] ,[code],[ssn],[mobile],[social_insurance_code],[photo] from employee where Id=@Emp_id;
    -- Insert statements for procedure here
	
END