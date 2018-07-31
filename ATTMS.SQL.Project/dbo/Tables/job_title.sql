CREATE TABLE [dbo].[job_title] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [title] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_job_title] PRIMARY KEY CLUSTERED ([Id] ASC)
);

