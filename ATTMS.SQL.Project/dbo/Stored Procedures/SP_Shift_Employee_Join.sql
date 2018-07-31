-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Shift_Employee_Join 
	@employee_Id int,
	@shift_Id int,
	@Join_Date date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	--leave Current Shift-Id
	UPDATE employee_shift SET left_at = @Join_Date Where employee_Id=@employee_Id and left_at IS NULL

	INSERT INTO employee_shift VALUES(@employee_Id,@shift_Id,@Join_Date,null)

	DECLARE @NewID int = SCOPE_IDENTITY()
	IF @NewID IS NOT NULL AND @NewID > 0 BEGIN
		INSERT INTO @Result_Tbl VALUES (@NewID,1,1,'new employee is joined the shift successfully.',null)
	END ELSE BEGIN
		INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to join the shift ',null)
	END

	SELECT * FROM @Result_Tbl
END