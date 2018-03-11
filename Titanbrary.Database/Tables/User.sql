CREATE TABLE [dbo].[User]
(
	[UserID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [LoginName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATETIME NOT NULL, 
    [MemberSince] DATETIME NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [ZipCode] NVARCHAR(10) NOT NULL, 
    [Phone] NVARCHAR(15) NOT NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Gender] BIT NULL, 
    [SQAnwer1] NVARCHAR(50) NOT NULL, 
    [SQAnswer2] NVARCHAR(50) NOT NULL, 
    [SQAnswer3] NVARCHAR(50) NOT NULL, 
    [UserType] INT NOT NULL
)
