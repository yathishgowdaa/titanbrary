﻿CREATE TABLE [dbo].[Cart]
(
	
	[CartID]		UNIQUEIDENTIFIER NOT NULL, 
	[UserID]		UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate]			DATETIME NOT NULL,
	[ModifiedDate] DATETIME NOT NULL, 
	
    CONSTRAINT [PK_MainCartTable] PRIMARY KEY ([CartID])
)