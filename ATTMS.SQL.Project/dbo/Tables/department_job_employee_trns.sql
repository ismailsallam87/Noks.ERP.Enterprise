CREATE TABLE [dbo].[department_job_employee_trns] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [employee_Id]           INT            NOT NULL,
    [department_job_Id_ref] INT            NOT NULL,
    [joined_at]             DATETIME       NOT NULL,
    [left_at]               DATETIME       NULL,
    [notes]                 NVARCHAR (500) NULL,
    [employee_coach_Id]     INT            NULL,
    CONSTRAINT [PK_department_job_employee_trns] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_department_job_employee_trns_department_job_ref] FOREIGN KEY ([department_job_Id_ref]) REFERENCES [dbo].[department_job_ref] ([Id]),
    CONSTRAINT [FK_department_job_employee_trns_employee] FOREIGN KEY ([employee_Id]) REFERENCES [dbo].[employee] ([Id]),
    CONSTRAINT [FK_department_job_employee_trns_employee_coach] FOREIGN KEY ([employee_coach_Id]) REFERENCES [dbo].[employee] ([Id])
);


GO
ALTER TABLE [dbo].[department_job_employee_trns] NOCHECK CONSTRAINT [FK_department_job_employee_trns_employee_coach];

