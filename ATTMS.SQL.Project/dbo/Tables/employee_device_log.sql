CREATE TABLE [dbo].[employee_device_log] (
    [employee_Id] INT NOT NULL,
    [enroll_no]   INT NOT NULL,
    [device_Id]   INT NOT NULL,
    CONSTRAINT [PK_employee_deice_log] PRIMARY KEY CLUSTERED ([employee_Id] ASC, [enroll_no] ASC, [device_Id] ASC),
    CONSTRAINT [FK_employee_device_log_Device] FOREIGN KEY ([device_Id]) REFERENCES [dbo].[device] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_employee_device_log_employee] FOREIGN KEY ([employee_Id]) REFERENCES [dbo].[employee] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

