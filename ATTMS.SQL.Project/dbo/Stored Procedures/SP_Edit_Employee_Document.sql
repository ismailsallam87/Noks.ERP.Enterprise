-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[SP_Edit_Employee_Document]
	@employee_documents_Id int 
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
		employee_documents.Id=@employee_documents_Id

END
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON