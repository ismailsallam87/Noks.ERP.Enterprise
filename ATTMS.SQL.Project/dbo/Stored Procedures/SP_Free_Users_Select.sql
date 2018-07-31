-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Free_Users_Select

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [Id],[UserName] as Name from [dbo].[AspNetUsers] Where Id NOT IN (SELECT [user_Id] from [dbo].[employee])
	ORDER BY [UserName]
END