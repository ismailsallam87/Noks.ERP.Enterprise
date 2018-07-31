CREATE TABLE [dbo].[leave_request_ref] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [leave_request_title]  NVARCHAR (50) NOT NULL,
    [open_balance_defualt] INT           NOT NULL,
    [is_default]           BIT           NOT NULL,
    [valid]                BIT           NOT NULL,
    [starting_from_days]   INT           NOT NULL,
    CONSTRAINT [PK_leave_request_ref] PRIMARY KEY CLUSTERED ([Id] ASC)
);

