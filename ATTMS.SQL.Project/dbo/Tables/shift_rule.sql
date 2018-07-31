CREATE TABLE [dbo].[shift_rule] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [title]      NVARCHAR (50) NOT NULL,
    [range_from] TIME (7)      NOT NULL,
    [range_to]   TIME (7)      NOT NULL,
    [rate]       INT           NOT NULL,
    [color]      NVARCHAR (15) NOT NULL,
    [type_Id]    INT           NOT NULL,
    [shift_Id]   INT           NOT NULL,
    CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_role_role_type] FOREIGN KEY ([type_Id]) REFERENCES [dbo].[shift_rule_type] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_role_shift] FOREIGN KEY ([shift_Id]) REFERENCES [dbo].[shift] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

