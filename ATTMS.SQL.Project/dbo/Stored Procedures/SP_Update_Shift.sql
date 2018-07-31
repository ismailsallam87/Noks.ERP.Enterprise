
CREATE procedure [dbo].[SP_Update_Shift](@Id int, @Title nvarchar(150) = null,@check_in time ,@check_out time  )
as
begin
update [dbo].shift 
set title = @Title,
 
check_in=@check_in ,
check_out= @check_out
--work_hours=@work_hours
where Id = @Id 
 

end