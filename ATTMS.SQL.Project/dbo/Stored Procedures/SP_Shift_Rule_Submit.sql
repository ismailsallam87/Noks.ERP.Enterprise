-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Shift_Rule_Submit
	@title nvarchar(50),
	@range_from time(7),
	@range_to time(7),
	@rate int,
	@color nvarchar(15),
	@type_Id int,
	@shift_Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))
	
	INSERT INTO dbo.shift_rule VALUES (@title,@range_from,@range_to,@rate,@color,@type_Id,@shift_Id)
	
	DECLARE @NewID int = SCOPE_IDENTITY()
	IF @NewID IS NOT NULL AND @NewID > 0 BEGIN
		INSERT INTO @Result_Tbl VALUES (@NewID,1,1,@title+' is inserted successfully.',null)
	END ELSE BEGIN
		INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to insert '+@title,null)
	END
	
	SELECT * FROM @Result_Tbl
END