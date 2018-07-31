-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Employee_Create
	@name nvarchar(50),
	@photo nvarchar(250)=null,
	@code nvarchar(50)=null,
	@user_Id nvarchar(128)=null,
	@ssn nvarchar(50)=null,
	@mobile nvarchar(50)=null,
	@social_insurance_code nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	INSERT INTO dbo.employee VALUES (@name,@photo,@code,@user_Id,@ssn,@mobile,@social_insurance_code)

	DECLARE @NewID int = SCOPE_IDENTITY()
	IF @NewID IS NOT NULL AND @NewID > 0 BEGIN
		INSERT INTO @Result_Tbl VALUES (@NewID,1,1,@name+' is inserted successfully.',null)
	END ELSE BEGIN
		INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to insert '+@name,null)
	END

	SELECT * FROM @Result_Tbl
END