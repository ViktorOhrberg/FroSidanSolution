CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[Name] nvarchar(100) Not null,
	[Price] money not null,
	[TempPrice] money null,
	[Balance] int not null,
	[ImgRef] nvarchar(100) null,
	[ThumbRef] nvarchar(100) null,
	[Description] nvarchar(max) Null,
	[Category] nvarchar(100) not null,
	[SubCategory] nvarchar(100) null,
	[Quantity] int not null
)
