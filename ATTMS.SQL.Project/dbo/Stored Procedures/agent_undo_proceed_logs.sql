-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.agent_undo_proceed_logs
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE device_logs set Processed=0



	UPDATE attendance_sheet set log_In=null,log_in_applied_shift_rule=null,log_out=null,log_out_applied_shift_rule=null,worked_hours=null,rated_deduction_hours=null,rated_overtime=null,rated_approved_overtime=null,is_absent=0
END