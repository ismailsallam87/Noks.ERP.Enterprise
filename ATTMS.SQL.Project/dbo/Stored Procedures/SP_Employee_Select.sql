-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Employee_Select]
	@Id int = null,
	@name nvarchar(50) = null,@photo  nvarchar(250) = null
,@code  nvarchar(50) = null,@ssn  nvarchar(50) = null,@mobile  nvarchar(50) = null,
@social_insurance_code nvarchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
	employee.Id,employee.name,employee.photo,employee.code,employee.user_Id,employee.ssn,employee.mobile,employee.social_insurance_code,
	dbo.fn_userName_by_Employee(employee.Id) as user_name,
	dbo.fn_employee_current_department_title(employee.Id) as department_title,
	dbo.fn_employee_current_job_title(employee.Id) as job_title,
	[dbo].[fn_employee_absense_total](employee.Id) as absence_total,
	[dbo].[fn_employee_approved_overtime_total](employee.Id) as total_approved_overtime,
	[dbo].[fn_employee_deducted_total](employee.Id) as total_deductions,
	[dbo].[fn_employee_workedhours_total](employee.Id) as worked_hours,
	[dbo].[fn_Emplyee_Fresh](employee.Id) as Is_Related


	FROM employee
	Where 
	(@Id IS NULL OR employee.Id = @Id)
	and
	((@name is null or @name = '')OR([Name] like '%'+@name+'%'))
	and
	((@photo is null or @photo = '')OR([photo] like '%'+@photo+'%'))
	and
	((@code is null or @code = '')OR([code] like '%'+@code+'%'))
	and
	((@ssn is null or @ssn = '')OR( [ssn] like '%'+@ssn+'%'))
	and
	((@mobile is null or @mobile='')OR ([mobile] like '%'+@mobile+'%'))
	and
	((@social_insurance_code is NULL or  @social_insurance_code='')OR ([social_insurance_code] like '%'+@social_insurance_code+'%'))
END