﻿CREATE TABLE [dbo].[contracts] (
    [Id]                           INT            IDENTITY (1, 1) NOT NULL,
    [contract_employee_first_name] NVARCHAR (150) NOT NULL,
    [contract_employee_last_name]  NVARCHAR (150) NOT NULL,
    [contract_employee_mid_name]   NVARCHAR (150) NULL,
    [created_at]                   DATETIME       NOT NULL,
    [created_by]                   NVARCHAR (128) NOT NULL,
    [department_job_Id_ref]        INT            NOT NULL,
    [exp_start_at]                 DATE           NULL,
    [exp_end_at]                   DATE           NULL,
    [start_at]                     DATE           NULL,
    [end_at]                       DATE           NULL,
    [trial_period_duration_start]  DATE           NULL,
    [trial_period_duration_end]    DATE           NULL,
    [renewable]                    BIT            NOT NULL,
    [contract_net_salary]          MONEY          NOT NULL,
    [contract_gross_salary]        MONEY          NOT NULL,
    [contract_basic_salary]        MONEY          NOT NULL,
    [terminated]                   BIT            NOT NULL,
    [employee_Id]                  INT            NOT NULL,
    CONSTRAINT [PK_contracts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_contracts_department_job_ref] FOREIGN KEY ([department_job_Id_ref]) REFERENCES [dbo].[department_job_ref] ([Id]),
    CONSTRAINT [FK_contracts_employee] FOREIGN KEY ([employee_Id]) REFERENCES [dbo].[employee] ([Id])
);

