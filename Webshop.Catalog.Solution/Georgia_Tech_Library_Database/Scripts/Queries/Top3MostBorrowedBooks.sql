SELECT bd.Title, COUNT(bl.BookID) AS BorrowCount
FROM BooksLoaned bl
JOIN Book b ON bl.BookID = b.BookID
JOIN BookDetail bd ON b.ISBN = bd.ISBN
GROUP BY bd.Title
ORDER BY BorrowCount DESC
LIMIT 3;
