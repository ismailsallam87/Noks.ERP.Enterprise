-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION fn_device_log_count
(
	@device_id int,
	@Processed bit=null
)
RETURNS bigint
AS
BEGIN
	-- Declare the return variable here
	DECLARE @log_count bigint

	-- Add the T-SQL statements to compute the return value here
	SET @log_count=(SELECT TOP(1) Count(Device_ID) from device_logs where device_id = @device_id and  (@Processed IS NULL OR Processed = @Processed))

	-- Return the result of the function
	RETURN @log_count

END