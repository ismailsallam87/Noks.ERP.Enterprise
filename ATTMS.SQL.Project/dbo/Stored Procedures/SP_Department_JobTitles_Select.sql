-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Department_JobTitles_Select]
	@department_Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT   
	dbo.department_job_ref.Id,     
	dbo.department_job_ref.code, 
	dbo.department_job_ref.valid, 
	dbo.department_job_ref.is_manager, 
	dbo.department_job_ref.multiple_available, 
	dbo.job_title.title,
	dbo.fn_department_job_employee_count(dbo.department_job_ref.Id) employee_count
	FROM            dbo.department_job_ref INNER JOIN
                         dbo.job_title ON dbo.department_job_ref.job_title_Id = dbo.job_title.Id
	WHERE        (dbo.department_job_ref.department_Id = @department_Id)
	Order By dbo.job_title.title
END