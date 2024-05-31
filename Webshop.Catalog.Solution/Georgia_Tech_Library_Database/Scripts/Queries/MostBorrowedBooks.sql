SELECT b.Title, COUNT(bl.BookID) AS BorrowCount
FROM BooksLoaned bl
JOIN Book b ON bl.BookID = b.BookID
GROUP BY b.Title
ORDER BY BorrowCount DESC
