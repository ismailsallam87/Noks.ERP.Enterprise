
CREATE procedure  [dbo].SP_Get_Job_Title_By_Id(@Id int )
as
begin
select * from job_title 
where Id = @Id
end