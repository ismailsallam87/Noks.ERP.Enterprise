CREATE PROCEDURE [dbo].[SP_Job_Title_Create]
	@Title nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	INSERT INTO dbo.job_title VALUES (@Title)

	DECLARE @NewID int = SCOPE_IDENTITY()
	IF @NewID IS NOT NULL AND @NewID > 0 BEGIN
		INSERT INTO @Result_Tbl VALUES (@NewID,1,1,@Title+' is inserted successfully.',null)
	END ELSE BEGIN
		INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to insert '+@Title,null)
	END

	SELECT * FROM @Result_Tbl
END