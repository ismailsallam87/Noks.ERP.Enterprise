-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Insert_Contract_Details]
 @Employee_id int,@Job_Title_id int,  @Employee_Name nvarchar(150)=null,  @Employee_Mid_Name nvarchar(150)=null,  @Employee_last_Name nvarchar(150)=null, @aspuser nvarchar(150), @datapicker2 datetime,@datapicker3_Exp_Start datetime,
              @datapicker4 DateTime,  @datapicker5 DateTime, @datapicker6_Exp_End DateTime,  @contract_net_salary Decimal =null,
          @contract_basic_salary Decimal =null,  @contract_gross_salary Decimal, @chk_terminated bit, @chk_renewable bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra re	sult sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	insert into [dbo].[contracts](department_job_Id_ref,exp_start_at,exp_end_at,contract_employee_first_name,contract_employee_mid_name,
	contract_employee_last_name,created_at,created_by
		
	,start_at,end_at,trial_period_duration_start,renewable,contract_net_salary
	,contract_gross_salary,contract_basic_salary,
terminated,employee_Id
	) values ( @Job_Title_id ,@datapicker3_Exp_Start, @datapicker6_Exp_End, @Employee_Name ,  @Employee_Mid_Name ,
	  @Employee_last_Name ,GETDATE(),@aspuser 
	   ,@datapicker2  ,  @datapicker4 ,  @datapicker5  ,@chk_renewable ,  @contract_net_salary  ,
      @contract_gross_salary,    @contract_basic_salary  ,   @chk_terminated ,  @Employee_id 
		
);
    -- Insert statements for procedure here
END