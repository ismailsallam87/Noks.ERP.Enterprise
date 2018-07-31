-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[agent_SP_update_proceed_log]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
DECLARE @logdate datetime
DECLARE @logID nvarchar(256)
DECLARE @employee_ID int
DECLARE @employee_shift_Id int

DECLARE logcursor cursor 
for
SELECT LogID,Log_Date,employee_shift.employee_Id,employee_shift.Id as employee_shift_Id
from
device_logs inner join employee_device_log on device_logs.Enroll_No=employee_device_log.enroll_no
inner join employee_shift on employee_device_log.employee_Id = employee_shift.employee_Id
where Processed <> 1 AND Log_Date BETWEEN (SELECT TOP(1) start_from from monthly_master_ref where is_current=1) AND (SELECT TOP(1) end_at from monthly_master_ref where is_current=1)

open logcursor

fetch from logcursor into @logID,@logdate,@employee_ID,@employee_shift_Id

WHILE @@FETCH_STATUS=0
BEGIN

DECLARE @shift_Id int = (SELECT TOP(1) employee_shift.shift_Id from shift inner join employee_shift on employee_shift.shift_Id = shift.Id where employee_shift.Id= @employee_shift_Id)
DECLARE @working_hours time(7) ='00:00:00'
DECLARE @deduction_hours time(7) ='00:00:00'
DECLARE @overtime time(7) = '00:00:00'
DECLARE @target_hours time(7) = (select convert(char(8), dateadd(HOUR, (SELECT TOP(1) work_hours from shift inner join employee_shift on employee_shift.shift_Id = shift.Id where employee_shift.Id= @employee_shift_Id), ''), 114))
DECLARE @Is_Absence bit = 0;
DECLARE @rated_DeductionHours time(7)  = '00:00:00'
DECLARE @rate int =0;

DECLARE @attendance_sheet_Id bigint = ISNULL((SELECT TOP(1) Id from attendance_sheet where attendance_sheet.employee_shift_Id=@employee_shift_Id and CONVERT(date, log_day) = CONVERT(date, @logdate)),0);
DECLARE @check_in time(7) =cast((SELECT MIN(Log_Date) from device_logs inner join employee_device_log on device_logs.Enroll_No=employee_device_log.enroll_no where CAST(Log_Date as date) = CAST(@logdate as date) and employee_device_log.employee_Id=@employee_ID) as time(7));
DECLARE @check_out time(7) =cast((SELECT MAX(Log_Date) from device_logs inner join employee_device_log on device_logs.Enroll_No=employee_device_log.enroll_no where CAST(Log_Date as date) = CAST(@logdate as date) and employee_device_log.employee_Id=@employee_ID) as time(7));
DECLARE @check_in_rule int
DECLARE @check_in_title nvarchar(50)

select @check_in_rule=shift_rule.Id,@check_in_title=shift_rule.title from [dbo].[shift_rule] 
inner join employee_shift on employee_shift.shift_Id=shift_rule.shift_Id
where employee_shift.shift_Id=@employee_shift_Id and @check_in BETWEEN shift_rule.range_from and shift_rule.range_to

DECLARE @check_out_rule int
DECLARE @check_out_title nvarchar(50)

select @check_out_rule=shift_rule.Id,@check_out_title=shift_rule.title from [dbo].[shift_rule] 
inner join employee_shift on employee_shift.shift_Id=shift_rule.shift_Id
where employee_shift.shift_Id=@employee_shift_Id and @check_out BETWEEN shift_rule.range_from and shift_rule.range_to



if @check_in IS NULL AND @check_out IS NULL --Absence
BEGIN
	SET @working_hours= '00:00:00';
	SET @check_in_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=7);
	SET @check_out_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=7);
	SET @deduction_hours = @target_hours;
	SET @rate= 1--(SELECT TOP(1) rate from shift_rule where Id=@check_in_rule)
	SET @rated_DeductionHours =cast(dateadd(hour,(CONVERT(INT, REPLACE(CONVERT(VARCHAR(8), @target_hours, 108),':','')) *@rate),'00:00:00') as time) 
	SET @Is_Absence = 1;
END

if @check_in IS NULL AND @check_out IS NOT NULL --MISSING CHECK-IN
BEGIN
	SET @working_hours= '00:00:00';
	SET @check_in_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=3);
	SET @check_out_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=6 and @check_out BETWEEN shift_rule.range_from and shift_rule.range_to);
	SET @deduction_hours = @target_hours;
	SET @rate= ISNULL((SELECT TOP(1) rate from shift_rule where Id=@check_in_rule),0)
	SET @rate= @rate+ISNULL((SELECT TOP(1) rate from shift_rule where Id=@check_out_rule),0)
	SET @rated_DeductionHours =cast(dateadd(hour,CONVERT(INT, REPLACE(CONVERT(VARCHAR(8), @target_hours, 108),':',''))*@rate,'00:00:00') as time) 
	SET @Is_Absence = 0;
END

if @check_out IS NULL AND @check_in IS NOT NULL --MISSING CHECK-OUT
BEGIN
	SET @working_hours= '00:00:00';
	SET @check_in_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=5 and @check_in BETWEEN shift_rule.range_from and shift_rule.range_to);
	SET @check_out_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=4);
	SET @deduction_hours = @target_hours;
	SET @rate= ISNULL((SELECT TOP(1) rate from shift_rule where Id=@check_in_rule),0)
	SET @rate= @rate+ISNULL((SELECT TOP(1) rate from shift_rule where Id=@check_out_rule),0)
	SET @rated_DeductionHours =cast(dateadd(hour,CONVERT(INT, REPLACE(CONVERT(VARCHAR(8), @target_hours, 108),':',''))*@rate,'00:00:00') as time) 
	SET @Is_Absence = 0;
END

if @check_out IS NOT NULL AND @check_in IS NOT NULL --Regular
BEGIN
	SET @working_hours=(select dbo.fn_time_substract(@check_in,@check_out));
	SET @check_in_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=5 and @check_in BETWEEN shift_rule.range_from and shift_rule.range_to);
	SET @check_out_rule =(SELECT TOP(1) Id from shift_rule where shift_Id=@shift_Id and shift_rule.type_Id=6 and @check_out BETWEEN shift_rule.range_from and shift_rule.range_to);
	SET @deduction_hours = @target_hours;
	SET @rate= ISNULL((SELECT TOP(1) rate from shift_rule where Id=@check_in_rule),0)
	SET @rate= @rate+ISNULL((SELECT TOP(1) rate from shift_rule where Id=@check_out_rule),0)
	SET @rated_DeductionHours =cast(dateadd(hour,CONVERT(INT, REPLACE(CONVERT(VARCHAR(8), @target_hours, 108),':',''))*@rate,'00:00:00') as time) 
	SET @Is_Absence = 0;
END

UPDATE  device_logs 
SET Processed = 1 
Where 
Enroll_No IN(SELECT enroll_no from employee_device_log where employee_Id=employee_Id) AND CAST(Log_Date as date) = CAST(@logdate as date)

UPDATE attendance_sheet
SET 
log_In=@check_in,
log_out=@check_out,
rated_deduction_hours=@rated_DeductionHours,
log_in_applied_shift_rule=@check_in_rule,
log_out_applied_shift_rule=@check_out_rule,
worked_hours=@working_hours,
is_absent = @Is_Absence
Where Id=@attendance_sheet_Id


fetch from logcursor into @logID,@logdate,@employee_ID,@employee_shift_Id
END



CLOSE logcursor
DEALLOCATE logcursor
END