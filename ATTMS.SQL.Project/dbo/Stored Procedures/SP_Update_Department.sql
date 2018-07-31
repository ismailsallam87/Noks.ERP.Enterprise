
Create procedure [dbo].[SP_Update_Department](@Id int, @Title nvarchar(150) = null  )
as
begin
update [dbo].department 
set title = @Title
where Id = @Id 
 

end