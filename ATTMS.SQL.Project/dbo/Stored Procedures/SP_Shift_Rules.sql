-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.SP_Shift_Rules
	@shift_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT shift_rule.Id,shift_rule.title,shift_rule.range_from,shift_rule.range_to,shift_rule.rate,shift_rule.color,shift_rule.type_Id,shift_rule_type.title as rule_type
	FROM shift_rule INNER JOIN shift_rule_type ON shift_rule.type_Id = shift_rule_type.Id
	where shift_rule.shift_Id=@shift_id
	Order By shift_rule_type.Id,shift_rule.range_from
END