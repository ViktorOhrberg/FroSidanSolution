CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[Innehåll] nvarchar(max) not null,
	[Datum] datetime not null,
	[Leveransadress] nvarchar(100) not null,
	[Kund-ID] nvarchar(450) references AspNetUsers(Id) not null,
	[Betalningsmetod] nvarchar(50) not null
)
