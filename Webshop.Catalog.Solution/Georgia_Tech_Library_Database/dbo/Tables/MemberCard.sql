CREATE TABLE [dbo].[MemberCard] (
    [MemberCardID]  UNIQUEIDENTIFIER NOT NULL,
    [BookLoanLimit] INT              NULL,
    [ExpireDate]    DATE             NULL,
    [Photo]         VARBINARY (MAX)  NULL,
    [LoanPeriod]    INT              NULL,
    [MemberSSN]     VARCHAR (50)     NULL,
    CONSTRAINT [PK_MemberCard] PRIMARY KEY CLUSTERED ([MemberCardID] ASC),
    FOREIGN KEY ([MemberSSN]) REFERENCES [dbo].[Member] ([SSN])
);

