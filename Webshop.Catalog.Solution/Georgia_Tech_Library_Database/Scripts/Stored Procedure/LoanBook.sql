Create PROCEDURE [dbo].[LoanBook]
    @BookID UNIQUEIDENTIFIER,
    @MemberCardID UNIQUEIDENTIFIER

AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExpireDate DATE;
    DECLARE @LoanPeriod INT;
    DECLARE @ISBN VARCHAR(50);
    DECLARE @LoanID INT;
    DECLARE @LoanDate DATE = GETDATE();
    DECLARE @DeadLineDate DATE;

    -- Get the ISBN for the book and then select 1 book that is not on loan and isloanable
    SELECT @ISBN = ISBN
    FROM dbo.Book
    WHERE BookID = @BookID;

    SELECT TOP 1 @BookID = b.BookID
    FROM Book b
    JOIN BookDetail bd ON b.ISBN = bd.ISBN
    WHERE b.ISBN = @ISBN AND b.OnLoan = 0 AND bd.IsLoanable = 1

    -- Check if the member's card is expired
    SELECT @ExpireDate = ExpireDate, @LoanPeriod = LoanPeriod
    FROM dbo.MemberCard
    WHERE MemberCardID = @MemberCardID;

    IF @ExpireDate < GETDATE()
    BEGIN
        RAISERROR('The member''s card is expired.', 16, 1);
        RETURN;
    END
    -- Calculate the deadline date
    SET @DeadlineDate = DATEADD(DAY, @LoanPeriod, @LoanDate);

    -- Record the loan in the BooksLoaned table
    INSERT INTO dbo.BooksLoaned (LoanID, BookID, MemberCardID, LoanDate, DeadlineDate)
    VALUES (NEXT VALUE FOR dbo.LoanIDSequence, @BookID, @MemberCardID, @LoanDate, @DeadlineDate);

     -- Get the generated LoanID
    SELECT @LoanID = SCOPE_IDENTITY();

    -- Update the Book table to reflect the loan status
    UPDATE dbo.Book
    SET OnLoan = 1
    WHERE BookID = @BookID;

    PRINT 'The book has been successfully loaned.';
END;
