CREATE TABLE [dbo].[department_job_ref] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [code]               NVARCHAR (50) NULL,
    [department_Id]      INT           NOT NULL,
    [job_title_Id]       INT           NOT NULL,
    [valid]              BIT           NOT NULL,
    [is_manager]         BIT           NOT NULL,
    [multiple_available] BIT           NOT NULL,
    CONSTRAINT [PK_department_job_ref] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_department_job_ref_department] FOREIGN KEY ([department_Id]) REFERENCES [dbo].[department] ([Id]),
    CONSTRAINT [FK_department_job_ref_job_title] FOREIGN KEY ([job_title_Id]) REFERENCES [dbo].[job_title] ([Id])
);

