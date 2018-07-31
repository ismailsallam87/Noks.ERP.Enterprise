-- =============================================
 
-- =============================================
Create PROCEDURE [dbo].[SP_Insert_Document_Details]
 @Employee_id int,@document_ref_id int,@signed_at datetime,@expires_at datetime
 ,@notes nvarchar(500),@expired bit,@copy_Path nvarchar(500),@purged bit,@renwed bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra re	sult sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--insert into [dbo].documents_ref([document_type_title],[icon],[valid],[expiration_days],[required]) values 
	--(@document_type_title,@icon,@Valid,@expiration_days,@required
	--);
    -- Insert statements for procedure here 
	insert into employee_documents([document_ref_Id],[signed_at],[expires_at],[copy_path],[employee_Id],[notes],[expired],[purged],[renewed])
values( @document_ref_id,@signed_at,@expires_at,@copy_Path,@Employee_id,@notes,@expired,@purged,@renwed)
END