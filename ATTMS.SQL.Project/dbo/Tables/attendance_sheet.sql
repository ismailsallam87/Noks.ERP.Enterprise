CREATE TABLE [dbo].[attendance_sheet] (
    [Id]                         BIGINT   IDENTITY (1, 1) NOT NULL,
    [employee_shift_Id]          INT      NOT NULL,
    [log_day]                    DATE     NOT NULL,
    [log_In]                     TIME (7) NULL,
    [log_in_applied_shift_rule]  INT      NULL,
    [log_out]                    TIME (7) NULL,
    [log_out_applied_shift_rule] INT      NULL,
    [worked_hours]               TIME (7) NULL,
    [target_working_hours]       TIME (7) NOT NULL,
    [rated_deduction_hours]      TIME (7) NULL,
    [rated_overtime]             TIME (7) NULL,
    [rated_approved_overtime]    TIME (7) NULL,
    [is_absent]                  BIT      NOT NULL,
    [day_off]                    BIT      NULL,
    [monthly_master_ref_Id]      INT      NOT NULL,
    CONSTRAINT [PK_attendance_sheet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_attendance_sheet_employee_shift] FOREIGN KEY ([employee_shift_Id]) REFERENCES [dbo].[employee_shift] ([Id]),
    CONSTRAINT [FK_attendance_sheet_monthly_master_ref] FOREIGN KEY ([monthly_master_ref_Id]) REFERENCES [dbo].[monthly_master_ref] ([Id]),
    CONSTRAINT [FK_attendance_sheet_shift_rule] FOREIGN KEY ([log_in_applied_shift_rule]) REFERENCES [dbo].[shift_rule] ([Id]),
    CONSTRAINT [FK_attendance_sheet_shift_rule_logout] FOREIGN KEY ([log_out_applied_shift_rule]) REFERENCES [dbo].[shift_rule] ([Id])
);


GO
ALTER TABLE [dbo].[attendance_sheet] NOCHECK CONSTRAINT [FK_attendance_sheet_shift_rule];


GO
ALTER TABLE [dbo].[attendance_sheet] NOCHECK CONSTRAINT [FK_attendance_sheet_shift_rule_logout];

