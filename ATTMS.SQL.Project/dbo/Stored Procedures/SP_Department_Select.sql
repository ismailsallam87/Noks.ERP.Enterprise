-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Department_Select]
	@title nvarchar(50) = NULL,
	@Id int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
	department.Id,
	department.title,
	dbo.fn_department_employee_count(department.Id) employee_count,
	dbo.fn_department_contract_count(department.Id) contarct_count,
	dbo.fn_department_contract_gross_salary_total(department.Id) gross_salary_total,
	dbo.fn_department_absense_total(department.Id) total_absence,
	dbo.fn_department_workedhours_total(department.Id) total_workedhours,
	dbo.fn_department_deducted_total(department.Id) total_deductedhours,
	dbo.fn_department_overtime_total(department.Id) total_overtimehours,
	dbo.fn_department_approved_overtime_total(department.Id) total_approved_overtimehours,
	dbo.fn_department_employee_managers(department.Id) managers,
	dbo.fn_department_employee_photos(department.Id) employees_photos,
	dbo.fn_Department_Fresh(department.Id) as is_fresh
	from 
	department		
	Where (@title IS NULL OR department.title LIKE '%'+@title+'%')
	AND (@Id IS NULL OR department.Id = @Id)
	ORDER BY department.title
END