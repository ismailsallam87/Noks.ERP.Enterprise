-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Select_Job_Title_By_Dept_id
	 @department_Id int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT        dbo.department_job_ref.Id, dbo.job_title.title AS Job_Title
FROM            dbo.department_job_ref INNER JOIN
                         dbo.job_title ON dbo.department_job_ref.job_title_Id = dbo.job_title.Id
WHERE        (dbo.department_job_ref.department_Id = @Department_ID)


END