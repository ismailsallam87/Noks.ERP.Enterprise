CREATE TABLE [dbo].[shift_rule_type] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [title] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_type] PRIMARY KEY CLUSTERED ([Id] ASC)
);

