CREATE Procedure [dbo].[Emloyee_Info](@name nvarchar(50), @departId int, @jobTitle_Id int)
as
begin
select Id,name from employee 
where 
LOWER(name) like '%'+@name+'%' 
AND 
((@jobTitle_Id IS NULL or @jobTitle_Id <=0) OR 
(employee.ID IN 
(SELECT employee_Id from 
department_job_employee_trns inner join department_job_ref on department_job_employee_trns.department_job_Id_ref =department_job_ref.Id   where department_job_ref.job_title_Id=@jobTitle_Id AND department_job_employee_trns.left_at IS NULL )))
AND 
((@departId IS NULL or @departId <=0) OR 
(employee.ID IN 
(SELECT employee_Id from 
department_job_employee_trns inner join department_job_ref on department_job_employee_trns.department_job_Id_ref =department_job_ref.Id   where department_job_ref.department_Id=@departId AND department_job_employee_trns.left_at IS NULL )))


end