CREATE TABLE [dbo].[User]
(
	[UserID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [LoginName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATETIME NULL, 
    [MemberSince] DATETIME NULL, 
    [Address] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [ZipCode] NVARCHAR(10) NULL, 
    [Phone] NVARCHAR(15) NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Gender] BIT NULL, 
    [SQAnwer1] NVARCHAR(50) NULL, 
    [SQAnswer2] NVARCHAR(50) NULL, 
    [SQAnswer3] NVARCHAR(50) NULL, 
    [UserType] INT NULL
)
