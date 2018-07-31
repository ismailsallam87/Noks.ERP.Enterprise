-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[SP_Pending_Requests]
	@UserID nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--Checking User Roles (Admin,HR,Manager,Coach or Employee)
	DECLARE @Requests_Tbl TABLE(id int,name nvarchar(50) ,username nvarchar(256),posted_at datetime,req_from datetime,req_to datetime,approved bit,rejected bit,Manaer_name nvarchar(50),Coach_name nvarchar(50),leave_request_title nvarchar(50))

	DECLARE @IsAdmin bit = 1
	DECLARE @IsManager bit = 1
	DECLARE @IsCoach bit = 1


	--Check If admin 
	IF (SELECT TOP(1) UserId from AspNetUserRoles where UserId=@UserID AND RoleId=N'74762200-30f4-45b4-b564-fb958eb1a5cc') IS NOT NULL BEGIN
		SET @IsAdmin = 1
	END ELSE BEGIN
		SET @IsAdmin = 0
	END
	--Get Manager ID (Employee ID) , SELECT FROM Employee by UserId
	DECLARE @Manager_ID int = (SELECT TOP(1) Id from employee where user_Id = @UserID)
	DECLARE @Coach_ID int = (SELECT TOP(1) Id from employee where user_Id = @UserID)

	
	INSERT INTO @Requests_Tbl  SELECT  [dbo].[leave_request_trns].Id  , emp1.name,UserName,posted_at,
 req_from,req_to,approved,rejected,emp2.name as Manager_Name,emp3.name as Coach_Name, leave_request_ref.leave_request_title
 
 	 FROM [dbo].[leave_request_trns]
	INNER JOIN [dbo].[leave_request_ref] on [dbo].[leave_request_ref].Id = [dbo].[leave_request_trns].leave_request_ref_Id
	INNER JOIN [dbo].[employee] as emp1 ON emp1.Id = [dbo].[leave_request_trns].requested_for_employee_Id
	INNER JOIN [dbo].[AspNetUsers] ON  [dbo].[AspNetUsers].Id = [dbo].[leave_request_trns].[posted_by]
	INNER JOIN [dbo].[employee] as emp2 ON emp2.Id = [dbo].[leave_request_trns].manager_Id
	INNER JOIN [dbo].[employee] as emp3 ON emp3.Id = [dbo].[leave_request_trns].coach_Id
 	Where rejected IS NULL AND approved IS NULL AND ((@IsAdmin = 1 or manager_Id=@Manager_ID) or (@IsAdmin = 1 or coach_Id=@Coach_ID))
	ORDER BY [dbo].[leave_request_trns].posted_at DESC
	
	SELECT * FROM @Requests_Tbl
END