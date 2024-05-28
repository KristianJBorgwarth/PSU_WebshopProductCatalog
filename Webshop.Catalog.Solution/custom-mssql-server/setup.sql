-- Create database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'psuwebshop')
BEGIN
    CREATE DATABASE psuwebshop;
END
GO

-- Use the database
USE psuwebshop;
GO

-- Create tables
CREATE TABLE [dbo].[Category](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [ParentId] [int] NOT NULL,
    [Description] [ntext] NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

CREATE TABLE [dbo].[Customer](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [Address] [nvarchar](200) NOT NULL,
    [Address2] [nvarchar](200) NULL,
    [City] [nvarchar](200) NOT NULL,
    [Region] [nvarchar](200) NOT NULL,
    [PostalCode] [nvarchar](50) NOT NULL,
    [Country] [nvarchar](150) NOT NULL,
    [Email] [nvarchar](100) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

CREATE TABLE [dbo].[Product](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [SKU] [nvarchar](50) NOT NULL,
    [Price] [int] NOT NULL,
    [Currency] [nvarchar](3) NOT NULL,
    [Description] [ntext] NULL,
    [AmountInStock] [int] NULL,
    [MinStock] [int] NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
GO

CREATE TABLE [dbo].[ProductCategory](
    [ProductId] [int] NOT NULL,
    [CategoryId] [int] NOT NULL,
     CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([ProductId] ASC, [CategoryId] ASC)
    );
GO

-- Insert data into Customer table
INSERT INTO [dbo].[Customer] (Name, Address, Address2, City, Region, PostalCode, Country, Email) VALUES
    ('Lola McCarthy', '1050 Oak Street', '', 'Seattle', '', '98104', 'United States', 'lola0@adventure-works.com'),
    ('Peter Houston', '1200 First Ave.', '', 'Joliet', '', '60433', 'United States', 'peter3@adventure-works.com'),
    ('Joseph Mitzner', '123 Camelia Avenue', '', 'Oxnard', '', '93030', 'United States', 'joseph4@adventure-works.com');
GO

-- Insert data into Category table
INSERT INTO [dbo].[Category] (Name, ParentId, Description) VALUES
    ('Electronics', 0, 'Devices and gadgets'),
    ('Home Appliances', 0, 'Appliances for home'),
    ('Furniture', 0, 'Home and office furniture');
GO
