CREATE TABLE [dbo].[employee_documents] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [document_ref_Id] INT            NOT NULL,
    [signed_at]       DATETIME       NOT NULL,
    [expires_at]      DATETIME       NULL,
    [copy_path]       NVARCHAR (500) NOT NULL,
    [employee_Id]     INT            NOT NULL,
    [notes]           NVARCHAR (500) NOT NULL,
    [expired]         BIT            NULL,
    [purged]          BIT            NULL,
    [renewed]         BIT            NULL,
    CONSTRAINT [PK_employee_documents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_employee_documents_documents_ref] FOREIGN KEY ([document_ref_Id]) REFERENCES [dbo].[documents_ref] ([Id]),
    CONSTRAINT [FK_employee_documents_employee] FOREIGN KEY ([employee_Id]) REFERENCES [dbo].[employee] ([Id])
);

