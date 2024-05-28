CREATE TRIGGER trg_CheckMaxBooksLoaned
ON BooksLoaned
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MemberCardID UNIQUEIDENTIFIER;

    -- Retrieve the MemberCardID from the inserted row
    SELECT @MemberCardID = inserted.MemberCardID
    FROM inserted;

    -- Check the number of current loans using the function
    IF dbo.fn_CountCurrentLoans(@MemberCardID) > 5
    BEGIN
        RAISERROR('A member cannot have more than 5 books loaned at the same time.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
