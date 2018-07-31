CREATE TABLE [dbo].[shift_work_days] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [name]             NVARCHAR (50) NOT NULL,
    [shift_Id]         INT           NOT NULL,
    [week_day]         INT           NULL,
    [on_calendar_date] DATE          NULL,
    [is_routine]       BIT           NULL,
    [day_on]           BIT           NULL,
    CONSTRAINT [PK_work_days] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_work_days_shift] FOREIGN KEY ([shift_Id]) REFERENCES [dbo].[shift] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

