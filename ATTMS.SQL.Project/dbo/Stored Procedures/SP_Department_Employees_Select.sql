-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Department_Employees_Select]
	@department_Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT        dbo.job_title.title AS job_title, dbo.department_job_employee_trns.Id AS department_job_employee_trns_Id, dbo.department_job_employee_trns.employee_Id, 
                         dbo.department_job_employee_trns.department_job_Id_ref, dbo.department_job_employee_trns.joined_at, dbo.department_job_employee_trns.left_at, dbo.department_job_employee_trns.notes, 
                         dbo.department_job_employee_trns.employee_coach_Id, employee_coach.name AS employee_coach_name, dbo.employee.Id AS trns_employee_Id, dbo.employee.name AS trns_employee_name, 
                         employee_coach.Id AS trns_employee_coach_Id,dbo.department_job_ref.is_manager
	FROM            dbo.department_job_employee_trns INNER JOIN
                         dbo.department_job_ref ON dbo.department_job_employee_trns.department_job_Id_ref = dbo.department_job_ref.Id INNER JOIN
                         dbo.employee ON dbo.department_job_employee_trns.employee_Id = dbo.employee.Id INNER JOIN
                         dbo.job_title ON dbo.department_job_ref.job_title_Id = dbo.job_title.Id LEFT OUTER JOIN
                         dbo.employee AS employee_coach ON dbo.department_job_employee_trns.employee_coach_Id = employee_coach.Id
	WHERE        (dbo.department_job_ref.department_Id = @department_Id)
	Order By dbo.department_job_employee_trns.joined_at DESC
END