--declare @from datetime = '1-jan-2007'
--declare @to datetime = '30-aug-2007'

CREATE procedure [dbo].[Employee_Log_ByEmpID_AndRangeDate](@empId int, @from datetime, @to datetime)
as
begin

declare @emp_tbl_rows Table(Enroll_No int, Device_Id int)

insert into @emp_tbl_rows select distinct(d.Enroll_No), d.Device_ID from Device_Logs as d
inner join employee_device_log as dl 
on d.Enroll_No = dl.enroll_no 
where dl.employee_Id = @empId
order by d.Enroll_No

DECLARE @myIndex int= 0
DECLARE @max_id int =0
SET @myIndex =(SELECT TOP(1) Enroll_No from @emp_tbl_rows order by Enroll_No )
SET @max_id =(SELECT TOP(1) Enroll_No from @emp_tbl_rows order by Enroll_No  desc)

DECLARE @day_curs date = @from;
WHILE @myIndex <= @max_id
BEGIN	
	While @day_curs <= @to
	BEGIN	
		declare @emp_logs Table(log_in time(7),log_out time(7),Log_day date)
		INSERT INTO @emp_logs 
		values(
		(SELECT min(CONVERT(time,Log_Date)) from Device_Logs where Enroll_No =@myIndex and CONVERT(date,Log_Date) = @day_curs ),
		(SELECT max(CONVERT(time,Log_Date)) from Device_Logs where Enroll_No =@myIndex and CONVERT(date,Log_Date) = @day_curs ),
		@day_curs)
		SET @day_curs=DATEADD(day,1,@day_curs);
	END
	SET @myIndex = @myIndex + 1;
END
SELECT * from @emp_logs
end