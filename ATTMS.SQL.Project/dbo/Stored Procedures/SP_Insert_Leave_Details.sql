-- =============================================
 
-- =============================================
CREATE PROCEDURE [dbo].[SP_Insert_Leave_Details]
 @leave_request_ref_Id int,@requested_for_employee_Id int ,@req_from datetime,@req_to datetime,
 @posted_by nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra re	sult sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	 
    -- Insert statements for procedure here 
	insert into leave_request_trns([leave_request_ref_Id],[requested_for_employee_Id],[req_from],[req_to],[posted_at],[posted_by],[approved],[rejected],[manager_approval],[manager_Id],[coach_approval],[coach_Id])
values( @leave_request_ref_Id,@requested_for_employee_Id,@req_from,@req_to,GETDATE(),@posted_by,NULL,NULL,NULL,NULL,NULL,NULL)
END