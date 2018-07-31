-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Department_employee_Create] 
	@department_Id int,
	@department_job_ref int,
	@employee_Id int,
	@joined_at datetime,
	@notes nvarchar(500),
	@employee_coach_Id int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Result_Tbl TABLE(Id nvarchar(128),updated_rows int,success bit,user_message nvarchar(500),advanced_message nvarchar(500))

	--IS Multiple 
	declare @is_multiple bit = (SELECT TOP(1) multiple_available From department_job_ref Where Id=@department_job_ref)
	declare @is_exist bit = 0
	IF (SELECT TOP(1) Id From department_job_employee_trns Where department_job_Id_ref=@department_job_ref) IS NOT NULL
		BEGIN
			SET @is_exist = 1;
		END
	declare @exist_employee_id int = (SELECT TOP(1) employee_Id From department_job_employee_trns Where department_job_Id_ref=@department_job_ref)
	
	IF (@is_multiple = 1 AND @is_exist = 0) OR (@is_multiple = 0) 
	BEGIN
			INSERT INTO department_job_employee_trns VALUES
			(@employee_Id,@department_job_ref,@joined_at,null,@notes,@employee_coach_Id)
			
			DECLARE @NewID int = SCOPE_IDENTITY()
			IF @NewID IS NOT NULL AND @NewID > 0 
			BEGIN
				INSERT INTO @Result_Tbl VALUES (@NewID,1,1,ISNULL((SELECT TOP(1) employee.name from employee where Id=@employee_id),'no employee name found')+' is addedd successfully.',null)
		    END ELSE BEGIN
				INSERT INTO @Result_Tbl VALUES (-1,0,0,'an error occurred while trying to add '+ISNULL((SELECT TOP(1) name from employee where id=@exist_employee_id),'no job title found')+'.',null)
			END
	END ELSE BEGIN
				INSERT INTO @Result_Tbl VALUES (-1,0,0,'this job-title is already assigned to '+ISNULL((SELECT TOP(1) name from employee where id=@exist_employee_id),'no employee name found.')+'.',null)
	END
	SELECT * FROM @Result_Tbl
END