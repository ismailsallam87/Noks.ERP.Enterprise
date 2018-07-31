CREATE TABLE [dbo].[contract_salary_structure] (
    [contract_Id]  INT             NOT NULL,
    [allowance_Id] INT             NOT NULL,
    [amount]       DECIMAL (18, 2) NOT NULL,
    [is_rate]      BIT             NOT NULL,
    [strat_at]     DATETIME        NOT NULL,
    [end_at]       DATETIME        NOT NULL,
    [created_by]   NVARCHAR (128)  NOT NULL,
    [created_at]   DATETIME        NOT NULL,
    CONSTRAINT [PK_contract_salary_structure] PRIMARY KEY CLUSTERED ([contract_Id] ASC, [allowance_Id] ASC),
    CONSTRAINT [FK_contract_salary_structure_allowance_ref] FOREIGN KEY ([allowance_Id]) REFERENCES [dbo].[allowance_ref] ([Id]),
    CONSTRAINT [FK_contract_salary_structure_contracts] FOREIGN KEY ([contract_Id]) REFERENCES [dbo].[contracts] ([Id])
);

