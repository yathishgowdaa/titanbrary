CREATE TABLE [dbo].[Cart]
(
	
	[CartID]		UNIQUEIDENTIFIER NOT NULL, 
	[UserID]		UNIQUEIDENTIFIER NOT NULL,
	[Date]			INT NOT NULL,
	[Quantity]		INT NOT NULL,
	[Type]			INT NOT NULL, /* This will be used to indicate 1 == checkout , 2 == renew, 3 == return */ 
	[Waitlist]		BIT NOT NULL, /* This will be used to indicate if the user wants to be put on a waitlist */ 
	
    CONSTRAINT [PK_MainCartTable] PRIMARY KEY ([CartID])
)
