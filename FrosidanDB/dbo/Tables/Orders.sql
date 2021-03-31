CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[Cart] nvarchar(max) not null,
	[Date] datetime not null,
	[DeliveryAdress] nvarchar(100) not null,
	[Customer-ID] nvarchar(450) references AspNetUsers(Id) not null,
	[PaymentMethod] nvarchar(50) not null
)
