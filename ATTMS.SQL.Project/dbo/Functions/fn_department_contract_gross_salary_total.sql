-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION dbo.fn_department_contract_gross_salary_total
(
	@department_Id int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @contract_gross_salary money =0

	-- Add the T-SQL statements to compute the return value here
	SET @contract_gross_salary =(SELECT TOP(1) SUM(contracts.contract_gross_salary) 
	FROM 
	contracts
	INNER JOIN department_job_ref ON dbo.contracts.department_job_Id_ref = dbo.department_job_ref.Id
	Where contracts.terminated =0 AND department_job_ref.department_Id=@department_Id)
	-- Return the result of the function
	RETURN ISNULL(@contract_gross_salary,0)

END