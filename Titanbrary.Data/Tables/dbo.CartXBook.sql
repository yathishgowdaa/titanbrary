IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CartXBook]') AND type in (N'U'))
	DROP TABLE [dbo].[CartXBook]
GO

	
CREATE TABLE [dbo].[CartXBook]
(

	[CartID]	UNIQUEIDENTIFIER NOT NULL,
	[BookID]	UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT [PK_CartXBook] PRIMARY KEY CLUSTERED ([CartID] ASC, [BookID] ASC),
    CONSTRAINT [FK_Book_CartXBook] FOREIGN KEY ([BookID]) REFERENCES [dbo].[Book] ([BookID]),
    CONSTRAINT [FK_Cart_BookXGenre] FOREIGN KEY ([CartID]) REFERENCES [dbo].[Cart] ([CartID])

)

GO