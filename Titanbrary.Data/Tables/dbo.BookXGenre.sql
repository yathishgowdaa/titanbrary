IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookXGenre]') AND type in (N'U'))
	DROP TABLE [dbo].[BookXGenre]
GO

CREATE TABLE [dbo].[BookXGenre] (
    [GenreID] UNIQUEIDENTIFIER NOT NULL,
    [BookID]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BookXGenre] PRIMARY KEY CLUSTERED ([GenreID] ASC, [BookID] ASC),
    CONSTRAINT [FK_Book_BookXGenre] FOREIGN KEY ([BookID]) REFERENCES [dbo].[Book] ([BookID]),
    CONSTRAINT [FK_Genre_BookXGenre] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID])
);

GO