select BooksLoaned.BookID, Member.Name, Book.Title, BooksLoaned.LoanDate, BooksLoaned.ReturnDate from BooksLoaned
join Member on BooksLoaned.MemberCardID = Member.MemberCardID
join book on BooksLoaned.BookID = book.BookID
order by BooksLoaned.BookID

Select * From Book
where OnLoan = 1
--------------------------------------------------------------------------------------------------------------------------------



