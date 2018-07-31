-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Truncate_DB
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Truncate Table emplyee;
	Truncate Table Department;
	truncate table Shift;
	dbcc checkident('emplyee',RESEED);
	dbcc checkident('Department',RESEED);

END