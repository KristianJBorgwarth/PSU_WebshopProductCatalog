CREATE TABLE [dbo].[BooksLoaned] (
    [LoanID]       INT              NOT NULL,
    [BookID]       UNIQUEIDENTIFIER NULL,
    [MemberCardID] UNIQUEIDENTIFIER NULL,
    [LoanDate]     DATE             NULL,
    [ReturnDate]   DATE             NULL,
    CONSTRAINT [PK_BooksLoaned] PRIMARY KEY CLUSTERED ([LoanID] ASC),
    FOREIGN KEY ([BookID]) REFERENCES [dbo].[Book] ([BookID]),
    FOREIGN KEY ([MemberCardID]) REFERENCES [dbo].[MemberCard] ([MemberCardID])
);

