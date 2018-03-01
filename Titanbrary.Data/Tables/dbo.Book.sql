IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type in (N'U'))
	DROP TABLE [dbo].[Book]
GO

CREATE TABLE [dbo].[Book] (
    [Name]        NVARCHAR (50)    NOT NULL,
    [Author]      NVARCHAR (50)    NOT NULL,
    [Publisher]   NVARCHAR (50)    NULL,
    [ISBN]        NVARCHAR (13)    NULL,
    [Edition]     INT              NULL,
    [Year]        DATETIME         NULL,
    [Quantity]    INT              NOT NULL,
    [Language]    NVARCHAR (20)    NULL,
    [Picture]     IMAGE            NULL,
    [Keywords]    NVARCHAR (100)   NOT NULL,
    [Active]      BIT              NOT NULL,
    [Description] NVARCHAR (100)   NULL,
    [Timestamp]   DATETIME         NOT NULL,
    [BookID]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([BookID] ASC)
);

GO