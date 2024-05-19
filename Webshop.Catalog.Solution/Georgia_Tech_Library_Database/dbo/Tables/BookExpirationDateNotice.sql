CREATE TABLE [dbo].[BookExpirationDateNotice] (
    [ExpirationDateNoticeID] INT              NOT NULL,
    [BookID]                 UNIQUEIDENTIFIER NULL,
    [MemberCardID]           UNIQUEIDENTIFIER NULL,
    [LoanDate]               DATE             NULL,
    [RemiderDate]            DATE             NULL,
    CONSTRAINT [PK_BookExpirationDateNotice] PRIMARY KEY CLUSTERED ([ExpirationDateNoticeID] ASC),
    FOREIGN KEY ([BookID]) REFERENCES [dbo].[Book] ([BookID]),
    FOREIGN KEY ([MemberCardID]) REFERENCES [dbo].[MemberCard] ([MemberCardID])
);

