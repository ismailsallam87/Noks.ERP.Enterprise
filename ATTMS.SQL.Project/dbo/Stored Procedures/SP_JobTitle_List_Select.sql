-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_JobTitle_List_Select
	@department_Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT * FROM [dbo].[job_title]
		WHERE Id NOT IN 
		(
		SELECT job_title_Id FROM department_job_ref Where department_Id=@department_Id
		)

		ORDER BY title
END