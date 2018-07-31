CREATE TABLE [dbo].[allowance_ref] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [allowance_title] NVARCHAR (150)  NOT NULL,
    [valid]           BIT             NOT NULL,
    [default_amount]  DECIMAL (18, 2) NOT NULL,
    [fixed]           BIT             NOT NULL,
    CONSTRAINT [PK_allowance_ref] PRIMARY KEY CLUSTERED ([Id] ASC)
);

