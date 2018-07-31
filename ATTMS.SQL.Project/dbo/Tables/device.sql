CREATE TABLE [dbo].[device] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Device_Name]        NVARCHAR (50)  NOT NULL,
    [Device_No]          INT            NOT NULL,
    [Device_ID]          INT            NOT NULL,
    [Device_IP]          NVARCHAR (150) NOT NULL,
    [Device_Port]        NVARCHAR (150) NOT NULL,
    [Device_Password]    NVARCHAR (150) NOT NULL,
    [Device_Last_Status] TINYINT        CONSTRAINT [DF_Device_Device_Last_Status] DEFAULT ((0)) NULL,
    [Device_Last_Sync]   DATETIME       NULL,
    CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0=On,1=Off', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'device', @level2type = N'COLUMN', @level2name = N'Device_Last_Status';

