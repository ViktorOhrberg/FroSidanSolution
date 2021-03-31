CREATE TABLE [dbo].[Orders] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Cart]           NVARCHAR (MAX) NOT NULL,
    [Date]           DATETIME       NOT NULL,
    [DeliveryAdress] NVARCHAR (100) NOT NULL,
    [Customer-ID]    NVARCHAR (450) NOT NULL,
    [PaymentMethod]  NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Customer-ID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

