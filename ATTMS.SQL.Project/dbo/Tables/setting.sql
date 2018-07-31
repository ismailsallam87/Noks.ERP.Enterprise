CREATE TABLE [dbo].[setting] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [group_name] NVARCHAR (50) NOT NULL,
    [key_name]   NVARCHAR (50) NOT NULL,
    [value]      NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_setting] PRIMARY KEY CLUSTERED ([Id] ASC)
);

