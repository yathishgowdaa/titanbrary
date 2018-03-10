CREATE TABLE [dbo].[CartXBook]
(

	[CartID]	UNIQUEIDENTIFIER NOT NULL,
	[BookID]	UNIQUEIDENTIFIER NOT NULL,
	[Quantity]		INT NOT NULL,

	CONSTRAINT [PK_CartXBook] PRIMARY KEY CLUSTERED ([CartID] ASC, [BookID] ASC),
    CONSTRAINT [FK_Book_CartXBook] FOREIGN KEY ([BookID]) REFERENCES [dbo].[Book] ([BookID]),
    CONSTRAINT [FK_Cart_BookXGenre] FOREIGN KEY ([CartID]) REFERENCES [dbo].[Cart] ([CartID])

)

GO

CREATE TRIGGER [dbo].[Trigger_CartXBook_i]
    ON [dbo].[CartXBook]
    FOR INSERT
    AS
    BEGIN
        SET NoCount ON
		
		DECLARE @id uniqueidentifier
		SELECT @id = BookID FROM inserted

		UPDATE Book
		SET Quantity = Quantity - 1
		WHERE BookID = @id
    END
GO

CREATE TRIGGER [dbo].[Trigger_CartXBook_d]
    ON [dbo].[CartXBook]
    FOR DELETE
    AS
    BEGIN
        SET NoCount ON
				
		DECLARE @id uniqueidentifier
		SELECT @id = BookID FROM deleted

		UPDATE Book
		SET Quantity = Quantity + 1
		WHERE BookID = @id
    END