-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Employee_Documents]
	@Employee_ID int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	documents_ref.Id as document_ref_Id,
	documents_ref.document_type_title,
	documents_ref.icon,
	employee_documents.copy_path,
	employee_documents.employee_Id,
	employee_documents.Id as employee_document_Id,
	employee_documents.notes,
	employee_documents.signed_at
	--employee_documents.signed_end
	FROM 
	documents_ref INNER JOIN
	employee_documents ON documents_ref.Id=employee_documents.document_ref_Id
	Where 
	(@Employee_ID IS NULL) OR (employee_documents.employee_Id = @Employee_ID)
	Order By employee_documents.signed_at
END