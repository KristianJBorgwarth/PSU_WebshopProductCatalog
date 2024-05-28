select BooksLoaned.BookID, Member.Name, Book.Title, BooksLoaned.LoanDate, BooksLoaned.ReturnDate from BooksLoaned
join Member on BooksLoaned.MemberCardID = Member.MemberCardID
join book on BooksLoaned.BookID = book.BookID
order by BooksLoaned.BookID

Select * From Book
where OnLoan = 1
--------------------------------------------------------------------------------------------------------------------------------

INSERT INTO [dbo].[Book] ([BookID], [Title], [OnLoan], [Subject], [ISBN], [LibraryID])
Values
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'UniversityLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'UniversityLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'CityLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'CityLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'GeorgiaTechLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'GeorgiaTechLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'GeorgiaTechLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'GeorgiaTechLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'CityLibrary'),
(NEWID(), 'Effective Modern C++', 0, 'Programming', '9780134757599', 'CityLibrary')

Select * from BooksLoaned