CREATE TABLE [dbo].[employee_shift] (
    [Id]          INT  IDENTITY (1, 1) NOT NULL,
    [employee_Id] INT  NOT NULL,
    [shift_Id]    INT  NOT NULL,
    [join_at]     DATE NOT NULL,
    [left_at]     DATE NULL,
    CONSTRAINT [PK_employee_shift] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_employee_shift_employee] FOREIGN KEY ([employee_Id]) REFERENCES [dbo].[employee] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_employee_shift_shift] FOREIGN KEY ([shift_Id]) REFERENCES [dbo].[shift] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

