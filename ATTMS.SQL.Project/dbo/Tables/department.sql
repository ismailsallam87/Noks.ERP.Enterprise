CREATE TABLE [dbo].[department] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [title] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED ([Id] ASC)
);

