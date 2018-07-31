-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Department_JobTitle_Create 
	@department_Id int,
	@code nvarchar(50) = null,
	@job_title_Id int,
	@is_manager bit,
	@multiple_available bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	INSERT INTO department_job_ref VALUES
	(@code,@department_Id,@job_title_Id,1,@is_manager,@multiple_available)

	DECLARE @NewID int = SCOPE_IDENTITY()
	IF @NewID IS NOT NULL AND @NewID > 0 BEGIN
		INSERT INTO @Result_Tbl VALUES (@NewID,1,1,ISNULL((SELECT TOP(1) title from job_title where id= @job_title_Id),'no job title found')+' is addedd successfully to '+ISNULL((SELECT TOP(1) title from department where id= @department_Id),'no department found')+'.',null)
	END ELSE BEGIN
		INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to add '+ISNULL((SELECT TOP(1) title from job_title where id= @job_title_Id),'no job title found')+' to'+ISNULL((SELECT TOP(1) title from department where id= @department_Id),'no department found')+'.',null)
	END

	SELECT * FROM @Result_Tbl
END