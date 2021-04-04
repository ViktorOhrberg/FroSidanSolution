CREATE TABLE [dbo].[Orders] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Cart]           NVARCHAR (MAX) NOT NULL,
    [Date]           DATETIME       NOT NULL,
    [FirstName]            NVARCHAR (MAX)     NULL,
    [LastName]             NVARCHAR (MAX)     NULL,
    [Street]               NVARCHAR (MAX)     NULL,
    [City]                 NVARCHAR (MAX)     NULL,
    [Zip]                  NVARCHAR (MAX)     NULL,
    [Customer-ID]    NVARCHAR (450) NULL,
    [PaymentMethod]  NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Customer-ID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

