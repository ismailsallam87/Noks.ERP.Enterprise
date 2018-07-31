CREATE TABLE [dbo].[shift] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [title]      NVARCHAR (50) NOT NULL,
    [status]     BIT           NOT NULL,
    [check_in]   TIME (7)      NOT NULL,
    [check_out]  TIME (7)      NOT NULL,
    [work_hours] INT           NOT NULL,
    [Active]     BIT           NULL,
    CONSTRAINT [PK_shift] PRIMARY KEY CLUSTERED ([Id] ASC)
);

