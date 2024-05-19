CREATE TABLE [dbo].[Campus] (
    [CampusID] INT          NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [Address]  VARCHAR (50) NULL,
    CONSTRAINT [PK_Campus] PRIMARY KEY CLUSTERED ([CampusID] ASC)
);

