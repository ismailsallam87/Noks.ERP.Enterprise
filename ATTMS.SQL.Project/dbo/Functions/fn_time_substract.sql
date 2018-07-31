-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_time_substract] 
(
	@time_start time(7),
	@time_end time(7)
)
RETURNS time(7)
AS
BEGIN
	-- Declare the return variable here
	Declare @time_dif time(7)
	if @time_end > @time_start
	begin
		SET @time_dif = (SELECT CONVERT(TIME,DATEADD(MS,DATEDIFF(SS,@time_start, @time_end)*1000,0),114))
	end
	if @time_end < @time_start
	begin
		set @time_dif ='00:00:00.0000000'
	end
	return @time_dif

END