CREATE TABLE [dbo].[Librarian] (
    [SSN]       VARCHAR (50) NOT NULL,
    [Name]      VARCHAR (50) NULL,
    [JobType]   VARCHAR (50) NULL,
    [LibraryID] VARCHAR (50) NULL,
    CONSTRAINT [PK_Librarian] PRIMARY KEY CLUSTERED ([SSN] ASC),
    FOREIGN KEY ([LibraryID]) REFERENCES [dbo].[Library] ([Name])
);

