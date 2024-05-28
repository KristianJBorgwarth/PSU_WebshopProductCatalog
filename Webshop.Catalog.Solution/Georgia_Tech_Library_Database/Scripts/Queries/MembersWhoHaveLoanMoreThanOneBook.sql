SELECT m.Name, m.SSN, (SELECT COUNT(*)
                       FROM BooksLoaned bl
                       WHERE bl.MemberCardID = mc.MemberCardID) AS TotalBooksBorrowed
FROM Member m
JOIN MemberCard mc ON m.SSN = mc.MemberSSN
WHERE (SELECT COUNT(*)
       FROM BooksLoaned bl
       WHERE bl.MemberCardID = mc.MemberCardID) > 0
ORDER BY TotalBooksBorrowed desc;
