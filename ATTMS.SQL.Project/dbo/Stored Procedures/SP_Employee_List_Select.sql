-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Employee_List_Select]
	@Only_free bit = null,
	@department_Id int = null,
	@shift_Id int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
	Id,name,code,photo,
	dbo.fn_employee_current_department_title(Id) as department_title,
	dbo.fn_employee_current_job_title(Id) as job_title
	From 
	[dbo].[employee]
	Where 
	((@Only_free IS NULL OR @Only_free = 0) OR (employee.Id not in (SELECT employee_Id from department_job_employee_trns Where left_at IS NULL)))
	AND
	((@department_Id IS NULL OR Id IN (SELECT employee_Id FROM department_job_employee_trns INNER JOIN department_job_ref ON department_job_employee_trns.department_job_Id_ref = department_job_ref.Id Where department_job_employee_trns.left_at IS NULL AND department_job_ref.department_Id =@department_Id)))
	AND
	((@shift_Id IS NULL) OR (employee.Id in (SELECT employee_Id from employee_shift where shift_Id=@shift_Id AND left_at IS NULL)))
	Order by Name
END