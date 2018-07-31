-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SP_Devices_Select2]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * ,
	dbo.fn_device_log_count(device.Id,1) as Processed_log_count,
	dbo.fn_device_log_count(device.Id,0) as Raw_log_count
	FROM device
END