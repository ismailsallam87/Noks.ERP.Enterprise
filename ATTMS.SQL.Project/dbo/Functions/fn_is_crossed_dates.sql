-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_is_crossed_dates
(
	@dt_1 datetime,
	@dt_2 datetime,
	@dt_3 datetime,
	@dt_4 datetime
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	 declare @corssed bit = 0;
	 declare @cross_days int =0;
	  
	 SET @cross_days =(SELECT COUNT(IndividualDate) FROM dbo.fn_dateRange('d',@dt_1,@dt_2)
	 Where IndividualDate IN(
	 SELECT * FROM dbo.fn_dateRange('d',@dt_3,@dt_4)))



	-- Return the result of the function
	RETURN @cross_days

END