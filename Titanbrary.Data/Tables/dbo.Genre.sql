IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genre]') AND type in (N'U'))
	DROP TABLE [dbo].[Genre]
GO

CREATE TABLE [dbo].[Genre] (
    [GenreID] UNIQUEIDENTIFIER NOT NULL,
    [Title]   NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED ([GenreID] ASC)
);

GO