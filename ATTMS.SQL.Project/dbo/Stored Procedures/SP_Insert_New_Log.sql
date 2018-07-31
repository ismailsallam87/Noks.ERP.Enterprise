-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Insert_New_Log]
	@Device_ID int,
	@Enroll_No int,
	@Log_Mode nvarchar(150),
	@Log_Date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Status bit = 0;
	IF (SELECT TOP(1) LogID From Device_Logs Where Enroll_No=@Enroll_No AND Device_ID=@Device_ID AND Log_Date=@Log_Date) IS NOT NULL
	BEGIN
		SET @Status = 0;
	END ELSE BEGIN
	INSERT INTO Device_Logs
	VALUES (NEWID(),@Device_ID,@Enroll_No,@Log_Mode,@Log_Date,GETDATE(),0)
	UPDATE Device SET Device_Last_Status=0,Device_Last_Sync=GETDATE()
	SET @Status = 1;
	END
	SELECT @Status as Status 
END