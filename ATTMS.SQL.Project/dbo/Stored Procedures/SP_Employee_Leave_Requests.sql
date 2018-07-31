-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Employee_Leave_Requests
	@All bit = null,
	@Employee_Id int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	leave_request_trns.Id as leave_request_trns_Id,
	leave_request_trns.leave_request_ref_Id,
	leave_request_ref.leave_request_title,
	leave_request_trns.req_from,
	leave_request_trns.req_to,
	leave_request_trns.posted_at,
	leave_request_trns.posted_by,
	leave_request_trns.requested_for_employee_Id,
	(SELECT TOP(1) name from employee where id=leave_request_trns.requested_for_employee_Id) as employee_name,
	(SELECT TOP(1) UserName from AspNetUsers where id=leave_request_trns.posted_by) as posted_by_userName,
	leave_request_trns.approved,
	leave_request_trns.rejected,
	leave_request_trns.manager_approval,
	leave_request_trns.manager_Id,
	(SELECT TOP(1) name from employee where id=leave_request_trns.manager_Id) as manager_name,
	leave_request_trns.coach_approval,
	leave_request_trns.coach_Id,
	(SELECT TOP(1) name from employee where id=leave_request_trns.coach_Id) as coach_name
	FROM leave_request_trns
	INNER JOIN leave_request_ref ON leave_request_ref.Id = leave_request_trns.leave_request_ref_Id
	Where 
	((@All IS NULL OR @All=1) OR (@All = 0 AND dbo.fn_is_crossed_dates(leave_request_trns.req_from,leave_request_trns.req_to,(SELECT TOP(1) start_from FROM monthly_master_ref where is_current =1),(SELECT TOP(1) end_at FROM monthly_master_ref where is_current =1)) >0))
	AND 
	(@Employee_Id IS NULL OR leave_request_trns.requested_for_employee_Id = @Employee_Id)
	Order By req_from
END