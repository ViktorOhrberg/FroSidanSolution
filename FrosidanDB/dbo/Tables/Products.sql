CREATE TABLE [dbo].[Products] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Price]       MONEY          NOT NULL,
    [TempPrice]   MONEY          NULL,
    [Balance]     INT            NOT NULL,
    [ImgRef]      NVARCHAR (100) NULL,
    [ThumbRef]    NVARCHAR (100) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Category]    NVARCHAR (100) NOT NULL,
    [SubCategory] NVARCHAR (100) NULL,
    [Quantity]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

