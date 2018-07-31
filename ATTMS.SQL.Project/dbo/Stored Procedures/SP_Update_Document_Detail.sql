Create PROCEDURE [dbo].[SP_Update_Document_Detail]
 @employee_document_Id int,@document_ref_id int,@signed_at datetime,@expires_at datetime
 ,@notes nvarchar(500),@copy_Path nvarchar(500)

 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra re	sult sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--insert into [dbo].documents_ref([document_type_title],[icon],[valid],[expiration_days],[required]) values 
	--(@document_type_title,@icon,@Valid,@expiration_days,@required
	--);
    -- Insert statements for procedure here 
	update employee_documents
	set document_ref_Id= @document_ref_id,signed_at=@signed_at,expires_at=@expires_at,copy_path=@copy_Path,notes=@notes 
	where
Id=@employee_document_Id;
END