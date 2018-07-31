CREATE TABLE [dbo].[device_logs] (
    [LogID]     UNIQUEIDENTIFIER NOT NULL,
    [Device_ID] INT              NOT NULL,
    [Enroll_No] INT              NOT NULL,
    [Log_Mode]  NVARCHAR (150)   NOT NULL,
    [Log_Date]  DATETIME         NOT NULL,
    [Sync_Date] DATETIME         NOT NULL,
    [Processed] BIT              NOT NULL,
    CONSTRAINT [PK_Device_Logs] PRIMARY KEY CLUSTERED ([LogID] ASC),
    CONSTRAINT [FK_Device_Logs_Device1] FOREIGN KEY ([Device_ID]) REFERENCES [dbo].[device] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

