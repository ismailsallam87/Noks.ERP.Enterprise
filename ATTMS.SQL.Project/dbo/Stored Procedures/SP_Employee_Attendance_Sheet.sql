-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Employee_Attendance_Sheet]
	@day_from date =null,
	@day_to date =null,
	@employee_Id int =null,
	@monthly_master_ref_Id int = null,
	@monthly_master_ref_current bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT 
		attendance_sheet.Id as attendance_sheet_Id,
		attendance_sheet.employee_shift_Id,
		attendance_sheet.log_day,
		attendance_sheet.log_In,
		attendance_sheet.log_in_applied_shift_rule,
		attendance_sheet.log_out,
		attendance_sheet.log_out_applied_shift_rule,
		attendance_sheet.worked_hours,
		attendance_sheet.target_working_hours,
		attendance_sheet.rated_deduction_hours,
		attendance_sheet.rated_overtime,
		attendance_sheet.rated_approved_overtime,
		attendance_sheet.is_absent,
		attendance_sheet.monthly_master_ref_Id,
		attendance_sheet.day_off,
		employee_shift.Id as shift_Id,
		shift.title as shift_title,
		shift.check_in as shift_checkIn,
		shift.check_out as shift_checkOut,
		employee_shift.employee_Id,
		login_rules.title as checkIn_rule_title,
		login_rules.color as checkIn_rule_color,
		logout_rules.title as checkOut_rule_title,
		logout_rules.color as checkout_rule_color,
		employee.name as employee_name
		FROM 
		attendance_sheet
		INNER JOIN employee_shift ON attendance_sheet.employee_shift_Id = employee_shift.Id
		INNER JOIN shift ON employee_shift.shift_Id = shift.Id
		LEFT OUTER JOIN shift_rule as login_rules ON attendance_sheet.log_in_applied_shift_rule = login_rules.Id
		LEFT OUTER JOIN shift_rule as logout_rules ON attendance_sheet.log_out_applied_shift_rule = logout_rules.Id
		INNER JOIN monthly_master_ref ON attendance_sheet.monthly_master_ref_Id = monthly_master_ref.Id
		INNER JOIN employee ON employee_shift.employee_Id = employee.Id

		WHERE (
		((@day_from IS NULL OR @day_to IS NULL) OR (CAST(attendance_sheet.log_day as date) BETWEEN CAST(@day_from as date) AND CAST(@day_to as date)))
		AND
		(@employee_Id IS NULL OR employee_shift.employee_Id = @employee_Id)
		AND
		(@monthly_master_ref_Id IS NULL OR attendance_sheet.monthly_master_ref_Id=@monthly_master_ref_Id)
		)
		AND
		(@monthly_master_ref_current IS NULL OR monthly_master_ref.is_current=@monthly_master_ref_current)

		ORDER BY employee_shift.employee_Id,attendance_sheet.log_day
END