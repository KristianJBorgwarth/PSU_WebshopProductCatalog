CREATE TABLE [dbo].[Member] (
    [SSN]          VARCHAR (50)     NOT NULL,
    [Name]         VARCHAR (50)     NULL,
    [Type]         VARCHAR (50)     NULL,
    [MemberCardID] UNIQUEIDENTIFIER NULL,
    [LibraryID]    VARCHAR (50)     NULL,
    [HomeAddress]  VARCHAR (100)    NULL,
    [CampusID]     INT              NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([SSN] ASC),
    FOREIGN KEY ([CampusID]) REFERENCES [dbo].[Campus] ([CampusID]),
    FOREIGN KEY ([LibraryID]) REFERENCES [dbo].[Library] ([Name])
);

