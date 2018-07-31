-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Employee_Contracts
	@Employee_Id int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	contracts.Id as contracts_Id,
	contracts.contract_employee_first_name,
	contracts.contract_employee_last_name,
	contracts.contract_employee_mid_name,
	contracts.contract_gross_salary,
	contracts.contract_net_salary,
	contracts.created_at,
	contracts.created_by,
	(SELECT TOP(1) UserName from AspNetUsers Where Id=contracts.created_by) as CreatedBy_UserName,
	contracts.department_job_Id_ref,
	contracts.employee_Id,
	contracts.start_at,
	contracts.end_at,
	contracts.renewable,
	contracts.exp_start_at,
	contracts.exp_end_at,
	contracts.terminated,
	contracts.trial_period_duration_start,
	contracts.trial_period_duration_end,
	department.title as department_title,
	job_title.title as job_title
	FROM contracts INNER JOIN
	department_job_ref ON department_job_ref.Id = contracts.department_job_Id_ref INNER JOIN
	job_title ON department_job_ref.job_title_Id = job_title.Id INNER JOIN
	department ON department_job_ref.department_Id = department.Id
	Where 
	(@Employee_Id IS NULL OR contracts.employee_Id=@Employee_Id)
	Order By contracts.start_at DESC
END