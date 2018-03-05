IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cart]') AND type in (N'U'))
	DROP TABLE [dbo].[Cart]
GO

CREATE TABLE [dbo].[Cart]
(
	
	[CartID]	UNIQUEIDENTIFIER NOT NULL,
	[UserID]	UNIQUEIDENTIFIER NOT NULL,
	[Date]		DATETIME NULL,
	[BookList]	NVARCHAR(36)

    CONSTRAINT [PK_MainCartTable] PRIMARY KEY ([CartID])
)

GO