-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Employee_Edit]
	@id int,
	@name nvarchar(50),
	@photo nvarchar(250)=null,
	@code nvarchar(50)=null,
	@ssn nvarchar(50)=null,
	@mobile nvarchar(50)=null,
	@social_insurance_code nvarchar(50)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	update [dbo].[employee] set [name]=@name ,[photo]=@photo ,[code]=@code,[ssn]=@ssn,[mobile]=	@mobile,[social_insurance_code]=@social_insurance_code where [Id]=@id;
	
	
	IF @id IS NOT NULL AND @id > 0 BEGIN
		INSERT INTO @Result_Tbl VALUES (@id,1,1,@name+' is Updated successfully.',null)
	END ELSE BEGIN
		INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to Update '+@name,null)
	END

	SELECT * FROM @Result_Tbl
END