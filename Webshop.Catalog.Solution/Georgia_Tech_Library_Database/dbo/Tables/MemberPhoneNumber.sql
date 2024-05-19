CREATE TABLE [dbo].[MemberPhoneNumber] (
    [PhoneID]     INT          NOT NULL,
    [PhoneNumber] VARCHAR (20) NULL,
    [MemberSSN]   VARCHAR (50) NULL,
    CONSTRAINT [PK_MemberPhoneNumber] PRIMARY KEY CLUSTERED ([PhoneID] ASC),
    FOREIGN KEY ([MemberSSN]) REFERENCES [dbo].[Member] ([SSN])
);

