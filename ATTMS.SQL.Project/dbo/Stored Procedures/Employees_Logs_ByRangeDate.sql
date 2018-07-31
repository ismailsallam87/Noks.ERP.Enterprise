
CREATE procedure [dbo].[Employees_Logs_ByRangeDate](@from datetime, @to datetime,@in_name nvarchar(50),@in_id int,@in_job_title_id int,@in_dep_id int,@in_code nvarchar(50))
as
begin
declare @emp_id int
DECLARE @code nvarchar(50) 
DECLARE @name nvarchar(50)

declare @emp_logs Table(log_in time(7),log_out time(7),	Log_day date,code nvarchar(50),	emp_name nvarchar(50),
log_in_delay time(7), log_out_early time(7), log_in_rule nvarchar(50),log_out_rule nvarchar(50),deduction int,work_hours time(7))

DECLARE emp_cursor cursor 
For 
SELECT id,name,code from employee where  
(@in_name IS NULL OR employee.name like '%'+@in_name+'%')
AND
((@in_id IS NULL or @in_id <=0) OR (employee.Id =@in_id))
AND 
((@in_job_title_id IS NULL or @in_job_title_id <=0) OR 
(employee.ID IN 
(SELECT employee_Id from 
department_job_employee_trns inner join department_job_ref on department_job_employee_trns.department_job_Id_ref =department_job_ref.Id   where department_job_ref.job_title_Id=@in_job_title_id AND department_job_employee_trns.left_at IS NULL )))
AND 
((@in_dep_id IS NULL or @in_dep_id <=0) OR 
(employee.ID IN 
(SELECT employee_Id from 
department_job_employee_trns inner join department_job_ref on department_job_employee_trns.department_job_Id_ref =department_job_ref.Id   where department_job_ref.department_Id=@in_dep_id AND department_job_employee_trns.left_at IS NULL )))

AND
(@in_code IS NULL OR employee.code like '%'+@in_code+'%')

OPEN emp_cursor

FETCH FROM emp_cursor
Into @emp_id,@name,@code
WHILE @@FETCH_STATUS =0
BEGIN
declare @emp_tbl_rows Table(Enroll_No int, Device_Id int)

insert into @emp_tbl_rows select distinct(d.Enroll_No), d.Device_ID from Device_Logs as d
inner join employee_device_log as dl 
on d.Enroll_No = dl.enroll_no 
where dl.employee_Id =@emp_id
order by d.Enroll_No

DECLARE @day_curs date = @from;
declare @time_in time(7) = '09:00:00'
declare @time_out time(7) = '17:00:00'
declare @log_in time(7) 
declare @log_out time(7)
declare @login_rule nvarchar(50)
declare @logout_rule nvarchar(50)
declare @rate1 int
declare @rate2 int
declare @working_hours time(7) 
declare @deduction int
declare @shift_Id int 

DECLARE @myIndex int= 0
DECLARE @max_id int =0
SET @myIndex =(SELECT TOP(1) Enroll_No from @emp_tbl_rows order by Enroll_No )
SET @max_id =(SELECT TOP(1) Enroll_No from @emp_tbl_rows order by Enroll_No  desc)
	
WHILE @myIndex <= @max_id
BEGIN	
	While @day_curs <= @to
	BEGIN 

		set @shift_Id = (select s.id  from [dbo].[shift] as s inner join employee_shift as es on s.Id = es.shift_Id
		where ((@emp_Id IS NULL or @emp_Id <=0) OR (es.employee_Id = @emp_Id))
		AND ((@day_curs IS NULL or @day_curs = '') OR (es.join_at <= @day_curs) and (es.left_at >= @day_curs)))

		
		set @log_in = (SELECT min(CONVERT(time,Log_Date)) from Device_Logs where Enroll_No =@myIndex and CONVERT(date,Log_Date) = @day_curs)		
		set @log_out = (SELECT max(CONVERT(time,Log_Date)) from Device_Logs where Enroll_No =@myIndex and CONVERT(date,Log_Date) = @day_curs )
		
		set @login_rule =(select top(1)title from shift_rule where shift_Id = 1 and  @log_in between range_from and range_to and [type_Id] =5 order by rate)
	    set @rate1 = (select top(1)rate from shift_rule where title=@login_rule and  @log_in between range_from and range_to and [type_Id] =5 order by rate)
		if @rate1 is null
		begin
		 set @rate1 = 0
		end
		set @logout_rule =(select top(1)title from shift_rule where shift_Id = @shift_Id and @log_out between range_from and range_to and [type_Id] =6 order by rate)
        set @rate2 = (select top(1)rate from shift_rule where title = @logout_rule and  @log_out between range_from and range_to and [type_Id] =6 order by rate)
		if @rate2 is null
		begin
		 set @rate2 = 0
		end	
		if @log_in is null and @log_out is null
		begin 
		  set @login_rule = (select title from shift_rule where shift_Id = @shift_Id and [type_Id] =7)
		  set @logout_rule = (select title from shift_rule where shift_Id = @shift_Id  and [type_Id] =7)
		  set @rate1 = 50
		  set @rate2 = 50
		end 
		else
		begin
			if @log_in is null
			begin 
			  set @login_rule = (select top(1)title from shift_rule where shift_Id = @shift_Id and range_from = '00:00:00' and range_to = '00:00:00' and [type_Id] =5)
			  set @rate1 = (select rate from shift_rule where title='Missing_CheckIn')
			end 
			else if @log_out is null
			begin 
			  set @logout_rule = (select top(1)title from shift_rule where shift_Id = @shift_Id and range_from = '00:00:00' and range_to = '00:00:00' and [type_Id] =6)
			  set @rate1 = (select rate from shift_rule where title='Missing_CheckOut' )
			end 
		end 
		set @working_hours =(SELECT dbo.fn_time_substract(@log_in, @log_out))
		set @deduction = ((@rate1 + @rate2)*8)/100
		declare @delaying time(7) = (SELECT dbo.fn_time_substract(@time_in, @log_in))

		INSERT INTO @emp_logs values(@log_in, @log_out, @day_curs, @code, @name,ISNULL(@delaying,'00:00:00'),
		(SELECT dbo.fn_time_substract(@log_out,@time_out)),isnull(@login_rule,'undefined'), isnull(@logout_rule,'undefined'),
		isnull(@deduction,0),isnull(@working_hours,'00:00:00'))	
		
		SET @day_curs=DATEADD(day,1,@day_curs);
	END
	SET @myIndex = @myIndex + 1;
END
FETCH FROM emp_cursor
Into @emp_id,@name,@code
END
CLOSE emp_cursor
DEALLOCATE emp_cursor

declare @delay_table table(delay_time time(7),code nvarchar(50))
 insert into @delay_table 
 select convert(time(7),DATEADD(s, SUM(( DATEPART(hh, log_in_delay) * 3600 ) + ( DATEPART(mi, log_in_delay) * 60 ) + DATEPART(ss, log_in_delay)), 0)) 
 ,code from @emp_logs group by code

SELECT  count(log_in_rule) As Absence, (DATEDiff(DAY, @from,@to ) - count(log_in_rule)) as Attendance,
(select delay_time from @delay_table where code =  emp_logs.code) as Delay_Time, 
code,emp_name as Name from @emp_logs as emp_logs
where log_in_rule = 'Absence'
group by code,emp_name
--SELECT * from @emp_logs
--ORDER BY code,Log_day
end