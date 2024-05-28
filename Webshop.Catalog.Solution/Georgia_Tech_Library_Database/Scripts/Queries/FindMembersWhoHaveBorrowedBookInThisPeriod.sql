DECLARE @FromDate DATE, @ToDate DATE;

SET @FromDate = '2024-01-01';
SET @ToDate = '2024-12-31';

SELECT mc.MemberCardID, m.Name, mp.PhoneNumber, bl.ReturnDate
FROM BooksLoaned bl
JOIN MemberCard mc ON bl.MemberCardID = mc.MemberCardID
JOIN Member m ON mc.MemberSSN = m.SSN
JOIN MemberPhoneNumber mp ON m.SSN = mp.MemberSSN
WHERE bl.ReturnDate BETWEEN @FromDate AND @ToDate;
