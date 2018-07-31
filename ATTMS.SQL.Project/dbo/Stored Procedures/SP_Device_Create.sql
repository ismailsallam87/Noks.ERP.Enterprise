-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Device_Create
	@Device_Name nvarchar(50),
	@Device_No int,
	@Device_ID int,
	@Device_IP nvarchar(150),
	@Device_Port nvarchar(150),
	@Device_Password nvarchar(150)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	

	IF (SELECT TOP(1) ID from device where Device_IP = @Device_IP) IS NOT NULL BEGIN
		INSERT INTO @Result_Tbl VALUES(null,0,0,(@Device_IP+' is already exist.'),null)
	END
	ELSE BEGIN
		INSERT INTO device values(@Device_Name,@Device_No,@Device_ID,@Device_IP,@Device_Port,@Device_Password,null,null)
		DECLARE @newId int = SCOPE_IDENTITY()
		IF @newId IS NOT NULL BEGIN
			INSERT INTO @Result_Tbl VALUES(@newId,1,1,'The New Device is added successfully.',null)
		END ELSE BEGIN
			INSERT INTO @Result_Tbl VALUES(-1,0,0,'an error occurred while trying to add new device.',null)
		END
	END
	SELECT * from @Result_Tbl
END