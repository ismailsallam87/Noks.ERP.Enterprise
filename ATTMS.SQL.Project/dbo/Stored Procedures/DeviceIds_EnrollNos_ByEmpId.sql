create Procedure [dbo].[DeviceIds_EnrollNos_ByEmpId](@empId int)
as
begin
select distinct(d.Enroll_No), d.Device_ID from Device_Logs as d
inner join employee_device_log as dl 
on d.Enroll_No = dl.enroll_no 
where dl.employee_Id = @empId
order by d.Enroll_No
end