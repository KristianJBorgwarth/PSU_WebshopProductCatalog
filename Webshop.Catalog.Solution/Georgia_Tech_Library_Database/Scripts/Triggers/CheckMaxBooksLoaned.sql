CREATE TRIGGER trg_CheckMaxBooksLoaned
ON BooksLoaned
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MemberCardID UNIQUEIDENTIFIER;
    DECLARE @LoanCount INT;

    -- Retrieve the MemberCardID from the inserted row
    SELECT @MemberCardID = inserted.MemberCardID
    FROM inserted;

    -- Count the number of books currently loaned by this member (where ReturnDate is null)
    SELECT @LoanCount = COUNT(*)
    FROM BooksLoaned
    WHERE MemberCardID = @MemberCardID AND ReturnDate IS NULL;

    -- If the member has more than 5 books loaned, raise an error and roll back the transaction
    IF @LoanCount > 5
    BEGIN
        RAISERROR('A member cannot have more than 5 books loaned at the same time.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
