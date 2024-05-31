DECLARE @Title NVARCHAR(100), @LibraryName NVARCHAR(100);
SET @Title = 'Effective Modern C++'
SET @LibraryName = 'GeorgiaTechLibrary';

SELECT l.Name AS LibraryName, b.Title, COUNT(*) AS CopyCount
FROM Book b
JOIN Library l ON b.LibraryID = l.Name
WHERE b.Title = @Title AND l.Name = @LibraryName
GROUP BY l.Name, b.Title;



