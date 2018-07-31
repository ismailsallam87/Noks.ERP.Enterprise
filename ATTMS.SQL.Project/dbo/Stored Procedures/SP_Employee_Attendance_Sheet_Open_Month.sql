-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Employee_Attendance_Sheet_Open_Month]
	@date_from date,
	@date_to date,
	@employee_Id int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @monthly_ref int = (SELECT TOP(1) Id from monthly_master_ref where is_current=1)
	DECLARE @sheet_day DATE;


	--Get all employees who works for a shift.
	DECLARE @shift_Id int
	DECLARE @employee_shift_Id int
	DECLARE @work_hours int

			DECLARE employees_cursor cursor for
			SELECT 
			employee.Id as employee_Id,employee_shift.shift_Id,employee_shift.Id as employee_shift_Id,
			work_hours
			FROM employee inner join employee_shift ON employee.Id = employee_shift.employee_Id
			inner join shift on employee_shift.shift_Id = shift.Id
			Where (@employee_Id IS NULL OR employee.Id = @employee_Id) AND (employee_shift.left_at IS NULL) --AND (CAST(@sheet_day as date) BETWEEN CAST(employee_shift.join_at as date) AND CAST(ISNULL(employee_shift.left_at,GETDATE()) as date))

			OPEN employees_cursor 

			FETCH FROM employees_cursor into @employee_Id,@shift_Id,@employee_shift_Id,@work_hours

			WHILE @@FETCH_STATUS = 0
			BEGIN
			DECLARE month_days CURSOR FOR SELECT * FROM [dbo].[fn_dateRange]('d',@date_from,@date_to)
			OPEN month_days
			
			FETCH FROM month_days INTO @sheet_day
			WHILE @@FETCH_STATUS = 0
			BEGIN
			DECLARE @exist_Id bigint =0 
				SET @exist_Id=(SELECT TOP(1) Id from attendance_sheet where employee_shift_Id=@employee_shift_Id AND log_day=@sheet_day)
				IF @exist_Id IS NULL BEGIN
					--Insert a new attendance log for employee on specific day when employee is a member of shift
					INSERT INTO attendance_sheet 
					       (employee_shift_Id,log_day,target_working_hours,is_absent,day_off,monthly_master_ref_Id) 
					VALUES (@employee_shift_Id,@sheet_day,(select convert(char(8), dateadd(HOUR, @work_hours, ''), 114)),0,0,@monthly_ref)
				END
			FETCH FROM month_days INTO @sheet_day		
			END
			CLOSE month_days
			DEALLOCATE month_days

			FETCH FROM employees_cursor into @employee_Id,@shift_Id,@employee_shift_Id,@work_hours
			END
			CLOSE employees_cursor
			DEALLOCATE employees_cursor
END