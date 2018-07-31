CREATE procedure [dbo].[SP_Select_Requests]
as
begin
select l.Id,r.leave_request_title as Reason,l.req_from,l.req_to,e.[name] as EmployeeName,
(select [name] from employee where Id = l.coach_Id)as CoachName,(select [name] from employee where Id = l.manager_Id)as ManagerName,
u.UserName as PostedBy from leave_request_trns as l
inner join leave_request_ref as r
on l.leave_request_ref_Id = r.Id
inner join employee as e
on l.requested_for_employee_Id = e.Id
inner join AspNetUsers as u
on l.posted_by = u.Id 
where l.coach_approval is null and l.manager_approval is null 
end