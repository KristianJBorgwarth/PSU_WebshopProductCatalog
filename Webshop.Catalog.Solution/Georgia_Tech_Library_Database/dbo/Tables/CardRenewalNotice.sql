CREATE TABLE [dbo].[CardRenewalNotice] (
    [CardRenewalNoticeID] INT              NOT NULL,
    [MemberCardID]        UNIQUEIDENTIFIER NULL,
    [ReminderDate]        DATE             NULL,
    CONSTRAINT [PK_CardRenewalNotice] PRIMARY KEY CLUSTERED ([CardRenewalNoticeID] ASC),
    FOREIGN KEY ([MemberCardID]) REFERENCES [dbo].[MemberCard] ([MemberCardID])
);

