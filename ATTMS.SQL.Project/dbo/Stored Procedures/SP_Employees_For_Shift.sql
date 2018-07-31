-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Employees_For_Shift
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
	Id,code,name,
	(SELECT TOP(1) shift.title from employee_shift inner join shift on employee_shift.shift_Id = shift.Id where employee_shift.employee_Id=employee.Id AND left_at IS NULL) as current_shift_title,
	(SELECT TOP(1) shift.Id from employee_shift inner join shift on employee_shift.shift_Id = shift.Id where employee_shift.employee_Id=employee.Id AND left_at IS NULL) as current_shift_Id,
	(SELECT TOP(1) employee_shift.join_at from employee_shift inner join shift on employee_shift.shift_Id = shift.Id where employee_shift.employee_Id=employee.Id AND left_at IS NULL) as current_shift_joining_date,
	(SELECT TOP(1) employee_shift.Id from employee_shift inner join shift on employee_shift.shift_Id = shift.Id where employee_shift.employee_Id=employee.Id AND left_at IS NULL) as current_shift_trns_Id
	FROM employee
END