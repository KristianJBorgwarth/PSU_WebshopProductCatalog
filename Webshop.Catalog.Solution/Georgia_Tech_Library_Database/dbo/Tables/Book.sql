CREATE TABLE [dbo].[Book] (
    [BookID]    UNIQUEIDENTIFIER NOT NULL,
    [Title]     VARCHAR (100)    NULL,
    [OnLoan]    BIT              NULL,
    [Subject]   VARCHAR (50)     NULL,
    [ISBN]      VARCHAR (50)     NULL,
    [LibraryID] VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([BookID] ASC),
    FOREIGN KEY ([ISBN]) REFERENCES [dbo].[BookDetail] ([ISBN]),
    FOREIGN KEY ([LibraryID]) REFERENCES [dbo].[Library] ([Name])
);

