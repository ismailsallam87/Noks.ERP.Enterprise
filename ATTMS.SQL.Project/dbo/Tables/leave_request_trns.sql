CREATE TABLE [dbo].[leave_request_trns] (
    [Id]                        BIGINT         IDENTITY (1, 1) NOT NULL,
    [leave_request_ref_Id]      INT            NOT NULL,
    [requested_for_employee_Id] INT            NOT NULL,
    [req_from]                  DATETIME       NOT NULL,
    [req_to]                    DATETIME       NOT NULL,
    [posted_at]                 DATETIME       NOT NULL,
    [posted_by]                 NVARCHAR (128) NOT NULL,
    [approved]                  BIT            NOT NULL,
    [rejected]                  BIT            NOT NULL,
    [manager_approval]          BIT            NULL,
    [manager_Id]                INT            NULL,
    [coach_approval]            BIT            NULL,
    [coach_Id]                  INT            NULL,
    CONSTRAINT [PK_leave_request_trns] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_leave_request_trns_AspNetUsers] FOREIGN KEY ([posted_by]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_leave_request_trns_employee_coach] FOREIGN KEY ([coach_Id]) REFERENCES [dbo].[employee] ([Id]),
    CONSTRAINT [FK_leave_request_trns_employee_manager] FOREIGN KEY ([manager_Id]) REFERENCES [dbo].[employee] ([Id]),
    CONSTRAINT [FK_leave_request_trns_leave_request_ref] FOREIGN KEY ([leave_request_ref_Id]) REFERENCES [dbo].[leave_request_ref] ([Id])
);


GO
ALTER TABLE [dbo].[leave_request_trns] NOCHECK CONSTRAINT [FK_leave_request_trns_employee_manager];

