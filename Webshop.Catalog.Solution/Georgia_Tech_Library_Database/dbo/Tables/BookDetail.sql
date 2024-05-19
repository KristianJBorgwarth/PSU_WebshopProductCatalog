CREATE TABLE [dbo].[BookDetail] (
    [ISBN]          VARCHAR (50)  NOT NULL,
    [Description]   VARCHAR (MAX) NULL,
    [Language]      VARCHAR (50)  NULL,
    [Type]          VARCHAR (50)  NULL,
    [ReleaseDate]   DATE          NULL,
    [WishToAcquire] BIT           NULL,
    [IsLoanable]    BIT           NULL,
    CONSTRAINT [PK_BookDetail] PRIMARY KEY CLUSTERED ([ISBN] ASC)
);

