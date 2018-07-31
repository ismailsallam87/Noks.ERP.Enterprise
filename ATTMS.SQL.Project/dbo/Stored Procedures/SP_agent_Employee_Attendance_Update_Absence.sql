-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_agent_Employee_Attendance_Update_Absence]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		declare @start_date date = (SELECT TOP(1) start_from from monthly_master_ref where is_current=1)
		declare @end_date date = GETDATE()

		UPDATE attendance_sheet
		SET is_absent=1
		,worked_hours ='00:00:00'
		,rated_deduction_hours=cast(dateadd(hour,(CONVERT(INT, REPLACE(CONVERT(VARCHAR(8), attendance_sheet.target_working_hours, 108),':','')) *cast(ISNULL((select top(1) rate from shift_rule inner join shift on shift_rule.shift_Id = shift.Id inner join employee_shift on employee_shift.shift_Id = shift.Id where employee_shift.Id = attendance_sheet.employee_shift_Id and shift_rule.type_Id=7) , (select top(1) value from setting where key_name='deduction rate')) as decimal)),'00:00:00') as time)  
		,rated_overtime ='00:00:00'
		where  (
		DATENAME(dw, 
		CAST(DATEPART(m,  attendance_sheet.log_day) AS VARCHAR) 
		+ '/' 
		+ CAST(DATEPART(d, attendance_sheet.log_day) AS VARCHAR) 
		+ '/' 
		+ CAST(DATEPART(yy,  attendance_sheet.log_day) AS VARCHAR))
		) 
		IN(select name from shift_work_days where shift_work_days.day_on=1 and shift_Id=(select top(1) shift_Id from employee_shift where id=attendance_sheet.employee_shift_Id))
		AND attendance_sheet.log_In IS NULL AND attendance_sheet.log_out IS NULL
		AND cast(log_day as date)  BETWEEN cast(@start_date as date) AND cast(@end_date as date)

		UPDATE attendance_sheet
		SET 
		is_absent=0
		,day_off = 1
		,worked_hours ='00:00:00'
		,rated_deduction_hours= '00:00:00'
		,rated_overtime ='00:00:00'
		where  (
		DATENAME(dw, 
		CAST(DATEPART(m,  attendance_sheet.log_day) AS VARCHAR) 
		+ '/' 
		+ CAST(DATEPART(d, attendance_sheet.log_day) AS VARCHAR) 
		+ '/' 
		+ CAST(DATEPART(yy,  attendance_sheet.log_day) AS VARCHAR))
		) IN(select name from shift_work_days where shift_work_days.day_on=0 and shift_Id=(select top(1) shift_Id from employee_shift where id=attendance_sheet.employee_shift_Id))
		AND attendance_sheet.log_In IS NULL AND attendance_sheet.log_out IS NULL
		AND cast(log_day as date)  BETWEEN cast(@start_date as date) AND cast(@end_date as date)
END