DECLARE @Title NVARCHAR(100);
SET @Title = 'Effective Modern C++'

SELECT l.Name AS LibraryName, b.Title, COUNT(*) AS CopyCount
FROM Book b
JOIN BookDetail bd ON b.ISBN = bd.ISBN
JOIN Library l ON b.LibraryID = l.Name
WHERE b.Title = @Title
group by l.Name, b.Title
ORDER BY CopyCount desc;
