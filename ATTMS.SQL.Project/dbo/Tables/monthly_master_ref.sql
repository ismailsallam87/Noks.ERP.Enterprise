CREATE TABLE [dbo].[monthly_master_ref] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [code]       NVARCHAR (50)  NOT NULL,
    [start_from] DATETIME       NOT NULL,
    [end_at]     DATETIME       NOT NULL,
    [created_at] DATETIME       NOT NULL,
    [created_by] NVARCHAR (128) NOT NULL,
    [deleted]    BIT            NOT NULL,
    [is_current] BIT            NOT NULL,
    CONSTRAINT [PK_monthly_master_ref] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER dbo.tr_open_new_month
   ON  monthly_master_ref
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @start_from date = (select inserted.start_from FROM inserted)
	DECLARE @end_at date = (select inserted.end_at from inserted)

	EXEC dbo.SP_Employee_Attendance_Sheet_Open_Month @start_from,@end_at
END