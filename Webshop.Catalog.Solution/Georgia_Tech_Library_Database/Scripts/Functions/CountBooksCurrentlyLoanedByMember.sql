CREATE FUNCTION dbo.fn_CountCurrentLoans(@MemberCardID UNIQUEIDENTIFIER)
RETURNS INT
AS
BEGIN
    DECLARE @LoanCount INT;

    -- Count the number of books currently loaned by the member (where ReturnDate is null)
    SELECT @LoanCount = COUNT(*)
    FROM BooksLoaned
    WHERE MemberCardID = @MemberCardID AND ReturnDate IS NULL;

    RETURN @LoanCount;
END;
