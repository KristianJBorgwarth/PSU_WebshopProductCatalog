SELECT TOP 3 b.Title, COUNT(bl.BookID) AS BorrowCount
FROM BooksLoaned bl
JOIN Book b ON bl.BookID = b.BookID
JOIN BookDetail bd ON b.ISBN = bd.ISBN
GROUP BY b.Title
ORDER BY BorrowCount DESC
