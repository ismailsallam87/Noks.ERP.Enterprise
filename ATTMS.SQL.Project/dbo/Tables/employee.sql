CREATE TABLE [dbo].[employee] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [name]                  NVARCHAR (50)  NOT NULL,
    [photo]                 NVARCHAR (250) NULL,
    [code]                  NVARCHAR (50)  NULL,
    [user_Id]               NVARCHAR (128) NULL,
    [ssn]                   NVARCHAR (50)  NULL,
    [mobile]                NVARCHAR (50)  NULL,
    [social_insurance_code] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED ([Id] ASC)
);

