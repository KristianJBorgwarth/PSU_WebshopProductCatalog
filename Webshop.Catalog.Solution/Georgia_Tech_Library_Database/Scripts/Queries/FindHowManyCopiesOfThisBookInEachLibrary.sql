DECLARE @Title NVARCHAR(100);
SET @Title = 'Effective Modern C++'

SELECT b.LibraryID AS LibraryName, b.Title, COUNT(*) AS CopyCount
FROM Book b
WHERE b.Title = @Title
group by l.Name, b.Title
ORDER BY CopyCount desc;
