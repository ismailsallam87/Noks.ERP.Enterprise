CREATE PROCEDURE [dbo].[Reset_DB]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DELETE [dbo].[contract_salary_structure]
DELETE [dbo].[contracts]
DELETE [dbo].attendance_sheet
DELETE [dbo].department_job_employee_trns
DELETE [dbo].department_job_ref
DELETE [dbo].department
DELETE [dbo].device_logs
DELETE [dbo].employee_device_log
DELETE [dbo].employee_documents
DELETE [dbo].employee_shift
DELETE [dbo].leave_request_trns
DELETE [dbo].shift_rule
DELETE [dbo].shift
DELETE [dbo].monthly_master_ref
DELETE [dbo].employee


dbcc checkident('contracts',RESEED,1);
dbcc checkident('attendance_sheet',RESEED,1);
dbcc checkident('contracts',RESEED,1);
dbcc checkident('department_job_employee_trns',RESEED,1);
dbcc checkident('dbo.[department_job_ref]',RESEED,1);
dbcc checkident('department',RESEED,1);
dbcc checkident('employee_documents',RESEED,1);
dbcc checkident('employee_shift',RESEED,1);
dbcc checkident('leave_request_trns',RESEED,1);
dbcc checkident('shift_rule',RESEED,1);
dbcc checkident('shift',RESEED,1);
dbcc checkident('monthly_master_ref',RESEED,1);
dbcc checkident('employee',RESEED,1);
	
END