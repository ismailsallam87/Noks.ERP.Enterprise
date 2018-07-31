CREATE TABLE [dbo].[documents_ref] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [document_type_title] NVARCHAR (50) NOT NULL,
    [icon]                NVARCHAR (50) NULL,
    [valid]               BIT           NOT NULL,
    [expiration_days]     INT           NOT NULL,
    [required]            BIT           NOT NULL,
    CONSTRAINT [PK_documents_ref] PRIMARY KEY CLUSTERED ([Id] ASC)
);

