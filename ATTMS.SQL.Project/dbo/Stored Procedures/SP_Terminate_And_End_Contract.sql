Create procedure [dbo].[SP_Terminate_And_End_Contract](@Id int, @endAt datetime, @isTerminate bit)
as
begin
update contracts 
set end_at = @endAt,
terminated = @isTerminate
where Id = @Id
end