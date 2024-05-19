-- Create Libraries
INSERT INTO [dbo].[Library] ([Name], [Address])
VALUES ('GeorgiaTechLibrary', '266 4th St NW, Atlanta, GA 30332'),
       ('CityLibrary', '123 Main St, Anytown, USA'),
       ('UniversityLibrary', '456 College Ave, University Town, USA');

-- Insert Campus Records
INSERT INTO [dbo].[Campus] ([CampusID],[Name], [Address])
VALUES (1,'Campus Sofiendalsvej', 'Sofiendalsvej 60, 9200 Aalborg SV'),
       (2,'Campus Hobrovej', 'Hobrovej 85, 9000 Aalborg'),
       (3,'Campus Selma Lagerløfs Vej', 'Selma Lagerløfs Vej 2, 9220 Aalborg Øst');

-- Create Librarians for GeorgiaTechLibrary
INSERT INTO [dbo].[Librarian] ([SSN], [Name], [JobType], [LibraryID])
VALUES ('01011980-1234', 'John Doe', 'Chief librarian', 'GeorgiaTechLibrary'),
       ('02021985-5678', 'Jane Smith', 'Departmental associate librarian', 'GeorgiaTechLibrary'),
       ('03031990-9101', 'Robert Brown', 'Reference librarian', 'GeorgiaTechLibrary'),
       ('04041995-1121', 'Emily Davis', 'Checkout staff', 'GeorgiaTechLibrary'),
       ('05051992-3141', 'Michael Wilson', 'Library assistant', 'GeorgiaTechLibrary');


-- Insert Members
INSERT INTO [dbo].[Member] ([SSN], [Name], [Type], [MemberCardID], [LibraryID], [HomeAddress], [CampusID])
VALUES ('01011995-1234', 'Annette', 'Student', NULL, 'GeorgiaTechLibrary', '123 Peach St, Atlanta, GA', 1),
       ('02021996-5678', 'Oliver', 'Student', NULL, 'GeorgiaTechLibrary', '456 Pine St, Atlanta, GA', 1),
       ('03031997-9101', 'Kristian', 'Student', NULL, 'GeorgiaTechLibrary', '789 Oak St, Atlanta, GA', 1),
       ('04041998-1121', 'Arosan', 'Student', NULL, 'GeorgiaTechLibrary', '321 Maple St, Atlanta, GA', 1),
       ('05051999-3141', 'Joe Biden', 'Student', NULL, 'UniversityLibrary', '654 Cedar Ave, University Town, USA', 2),
       ('06061994-4152', 'Lionel Messi', 'Student', NULL, 'CityLibrary', '987 Birch Blvd, Anytown, USA', 3),
       ('07071980-5163', 'Gianna', 'Professor', NULL, 'GeorgiaTechLibrary', '147 Spruce St, Atlanta, GA', 1),
       ('08081981-6174', 'Nadeem', 'Professor', NULL, 'GeorgiaTechLibrary', '258 Fir St, Atlanta, GA', 1),
       ('09091982-7185', 'Brain', 'Professor', NULL, 'GeorgiaTechLibrary', '369 Elm St, Atlanta, GA', 1);



-- Insert Member Phone Numbers
INSERT INTO [dbo].[MemberPhoneNumber] ([PhoneID], [PhoneNumber], [MemberSSN])
VALUES (1, '12345678', '01011995-1234'),
       (2, '23456789', '02021996-5678'),
       (3, '34567890', '03031997-9101'),
       (4, '45678901', '04041998-1121'),
       (5, '56789012', '05051999-3141'),
       (6, '67890123', '06061994-4152'),
       (7, '78901234', '07071980-5163'),
       (8, '89012345', '08081981-6174'),
       (9, '90123456', '09091982-7185');


-- Insert Member Cards
INSERT INTO [dbo].[MemberCard] ([MemberCardID], [BookLoanLimit], [ExpireDate], [Photo], [LoanPeriod], [MemberSSN])
VALUES (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 21, '02021996-5678'),  -- Oliver
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 21, '03031997-9101'),  -- Kristian
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 21, '04041998-1121'),  -- Arosan
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 21, '05051999-3141'),  -- Joe Biden
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 21, '06061994-4152'),  -- Lionel Messi
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 93, '07071980-5163'),  -- Gianna
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 93, '08081981-6174'),  -- Nadeem
       (NEWID(), 5, DATEADD(YEAR, 4, GETDATE()), NULL, 93, '09091982-7185'),  -- Brain
       (NEWID(), 5, DATEADD(YEAR, -1, GETDATE()), NULL, 21, '01011995-1234'); -- Annette expires this year

-- Update MemberCardID in Member table
UPDATE [dbo].[Member]
SET MemberCardID = mc.MemberCardID
FROM [dbo].[Member] m
JOIN [dbo].[MemberCard] mc ON m.SSN = mc.MemberSSN
WHERE m.MemberCardID IS NULL;

-- Insert CardRenewalNotice for Annette make it 1 month before the expire date
INSERT INTO [dbo].[CardRenewalNotice] ([CardRenewalNoticeID], [ReminderDate], [MemberCardID])
SELECT 1, DATEADD(MONTH, -1, mc.ExpireDate), mc.MemberCardID
FROM [dbo].[Member] m
JOIN [dbo].[MemberCard] mc ON m.MemberCardID = mc.MemberCardID
WHERE m.SSN = '01011995-1234';

-- Insert Books (Type: Book, WishToAcquire = 0, IsLoanable = 1)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780134685991', 'A comprehensive guide to modern software development using Clean Code principles.', 'English', 'Book', '2017-08-11', 0, 1),
('9780132350884', 'An in-depth look at Agile software development methodologies and practices.', 'English', 'Book', '2008-08-01', 0, 1),
('9781491950357', 'A practical introduction to JavaScript and modern web development.', 'English', 'Book', '2016-09-03', 0, 1);

-- Insert Books (Type: Book, WishToAcquire = 1, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780131103627', 'A detailed analysis of the C programming language.', 'English', 'Book', '1988-04-01', 1, 0),
('9780201616224', 'Insights into the development of object-oriented software.', 'English', 'Book', '1994-10-01', 1, 0),
('9780137081073', 'Explores design patterns in software engineering.', 'English', 'Book', '2010-03-10', 1, 0);

-- Insert Maps (Type: Map, WishToAcquire = 0, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006124', 'Detailed map of the state of California.', 'English', 'Map', '2018-01-01', 0, 0),
('9780528006131', 'Road map of the state of Texas.', 'English', 'Map', '2018-02-01', 0, 0),
('9780528006148', 'Topographic map of the Rocky Mountains.', 'English', 'Map', '2018-03-01', 0, 0);

-- Insert Maps (Type: Map, WishToAcquire = 1, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006155', 'Historic map of the state of New York.', 'English', 'Map', '2018-04-01', 1, 0),
('9780528006162', 'Tourist map of the city of Paris.', 'English', 'Map', '2018-05-01', 1, 0),
('9780528006179', 'Political map of the European Union.', 'English', 'Map', '2018-06-01', 1, 0);

-- Insert Rare Books (Type: Rare Book, WishToAcquire = 1, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006186', 'A first edition of Shakespeares Hamlet.', 'English', 'Rare Book', '1603-01-01', 1, 0),
('9780528006193', 'Original manuscripts of Leonardo da Vinci.', 'Italian', 'Rare Book', '1519-01-01', 1, 0),
('9780528006209', 'Rare collection of Isaac Newtons works.', 'Latin', 'Rare Book', '1687-01-01', 1, 0);

-- Insert Rare Books (Type: Rare Book, WishToAcquire = 0, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006216', 'Early edition of Charles Darwins On the Origin of Species.', 'English', 'Rare Book', '1859-01-01', 0, 0),
('9780528006223', 'Ancient Greek texts of Homers Iliad.', 'Greek', 'Rare Book', '1753-01-01', 0, 0),  -- Approximate date
('9780528006230', 'Medieval illuminated manuscript of The Divine Comedy.', 'Italian', 'Rare Book', '1753-01-01', 0, 0);  -- Approximate date


-- Insert Reference Books (Type: Reference Book, WishToAcquire = 1, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006247', 'Comprehensive encyclopedia of world history.', 'English', 'Reference Book', '2020-01-01', 1, 0),
('9780528006254', 'Detailed atlas of the world.', 'English', 'Reference Book', '2019-01-01', 1, 0),
('9780528006261', 'Extensive dictionary of the English language.', 'English', 'Reference Book', '2018-01-01', 1, 0);

-- Insert Reference Books (Type: Reference Book, WishToAcquire = 0, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006278', 'Handbook of physics and chemistry.', 'English', 'Reference Book', '2017-01-01', 0, 0),
('9780528006285', 'Comprehensive guide to biological sciences.', 'English', 'Reference Book', '2016-01-01', 0, 0);

-- Insert Magazines (Type: Magazine, WishToAcquire = 1, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006292', 'Latest issue of Vogue magazine.', 'English', 'Magazine', '2023-01-01', 1, 0),
('9780528006308', 'Current issue of TopGear magazine.', 'English', 'Magazine', '2023-01-01', 1, 0);

-- Insert Magazines (Type: Magazine, WishToAcquire = 0, IsLoanable = 0)
INSERT INTO [dbo].[BookDetail] ([ISBN], [Description], [Language], [Type], [ReleaseDate], [WishToAcquire], [IsLoanable])
VALUES 
('9780528006315', 'Previous issue of Vogue magazine.', 'English', 'Magazine', '2022-01-01', 0, 0),
('9780528006322', 'Past issue of TopGear magazine.', 'English', 'Magazine', '2022-01-01', 0, 0);

-- Insert Books into the Book table
INSERT INTO [dbo].[Book] ([BookID], [Title], [OnLoan], [Subject], [ISBN], [LibraryID])
VALUES 
-- Books
(NEWID(), 'Clean Code: A Handbook of Agile Software Craftsmanship', 0, 'Software Development', '9780134685991', 'GeorgiaTechLibrary'),
(NEWID(), 'Clean Code: A Handbook of Agile Software Craftsmanship', 0, 'Software Development', '9780134685991', 'GeorgiaTechLibrary'),

(NEWID(), 'Agile Software Development, Principles, Patterns, and Practices', 0, 'Software Development', '9780132350884', 'GeorgiaTechLibrary'),
(NEWID(), 'Agile Software Development, Principles, Patterns, and Practices', 0, 'Software Development', '9780132350884', 'GeorgiaTechLibrary'),

(NEWID(), 'JavaScript: The Good Parts', 0, 'Web Development', '9781491950357', 'GeorgiaTechLibrary'),
(NEWID(), 'JavaScript: The Good Parts', 0, 'Web Development', '9781491950357', 'GeorgiaTechLibrary'),

(NEWID(), 'The C Programming Language', 0, 'Programming', '9780131103627', 'GeorgiaTechLibrary'),

(NEWID(), 'Design Patterns: Elements of Reusable Object-Oriented Software', 0, 'Software Design', '9780201616224', 'GeorgiaTechLibrary'),

(NEWID(), 'Patterns of Enterprise Application Architecture', 0, 'Software Design', '9780137081073', 'GeorgiaTechLibrary')

-- Maps
INSERT INTO [dbo].[Book] ([BookID], [Title], [OnLoan], [Subject], [ISBN], [LibraryID])
VALUES 
(NEWID(), 'Map of California', 0, 'Geography', '9780528006124', 'GeorgiaTechLibrary'),
(NEWID(), 'Map of California', 0, 'Geography', '9780528006124', 'GeorgiaTechLibrary'),

(NEWID(), 'Road Map of Texas', 0, 'Geography', '9780528006131', 'GeorgiaTechLibrary'),
(NEWID(), 'Road Map of Texas', 0, 'Geography', '9780528006131', 'GeorgiaTechLibrary'),

(NEWID(), 'Topographic Map of the Rocky Mountains', 0, 'Geography', '9780528006148', 'GeorgiaTechLibrary'),
(NEWID(), 'Topographic Map of the Rocky Mountains', 0, 'Geography', '9780528006148', 'GeorgiaTechLibrary')

-- Rare Books
INSERT INTO [dbo].[Book] ([BookID], [Title], [OnLoan], [Subject], [ISBN], [LibraryID])
VALUES 
(NEWID(), 'On the Origin of Species', 0, 'Biology', '9780528006216', 'GeorgiaTechLibrary'),
(NEWID(), 'On the Origin of Species', 0, 'Biology', '9780528006216', 'GeorgiaTechLibrary'),

(NEWID(), 'Homers Iliad', 0, 'Literature', '9780528006223', 'GeorgiaTechLibrary'),
(NEWID(), 'Homers Iliad', 0, 'Literature', '9780528006223', 'GeorgiaTechLibrary'),

(NEWID(), 'The Divine Comedy', 0, 'Literature', '9780528006230', 'GeorgiaTechLibrary'),
(NEWID(), 'The Divine Comedy', 0, 'Literature', '9780528006230', 'GeorgiaTechLibrary')

-- Reference Books
INSERT INTO [dbo].[Book] ([BookID], [Title], [OnLoan], [Subject], [ISBN], [LibraryID])
VALUES 
(NEWID(), 'Encyclopedia of World History', 0, 'History', '9780528006247', 'GeorgiaTechLibrary'),

(NEWID(), 'Atlas of the World', 0, 'Geography', '9780528006254', 'GeorgiaTechLibrary'),

(NEWID(), 'Dictionary of the English Language', 0, 'Language', '9780528006261', 'GeorgiaTechLibrary')

-- Magazines
INSERT INTO [dbo].[Book] ([BookID], [Title], [OnLoan], [Subject], [ISBN], [LibraryID])
VALUES 
(NEWID(), 'Vogue Magazine', 0, 'Fashion', '9780528006292', 'GeorgiaTechLibrary'),

(NEWID(), 'TopGear Magazine', 0, 'Automotive', '9780528006308', 'GeorgiaTechLibrary')
