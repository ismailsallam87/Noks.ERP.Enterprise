
CREATE procedure [dbo].[SP_Update_Job_Title](@Id int, @Title nvarchar(150) = null)
as
begin
update [dbo].[job_title] 
set [title] = @Title
where Id = @Id
end