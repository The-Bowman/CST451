USE [dbPCPARTS]
GO

ALTER TABLE [dbo].[OrderLines] DROP CONSTRAINT [FK_OrderLines_Product]
GO

ALTER TABLE [dbo].[OrderLines] DROP CONSTRAINT [FK_OrderLines_Orders]
GO

/****** Object:  Table [dbo].[OrderLines]    Script Date: 9/11/2022 9:22:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderLines]') AND type in (N'U'))
DROP TABLE [dbo].[OrderLines]
GO

/****** Object:  Table [dbo].[OrderLines]    Script Date: 9/11/2022 9:22:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderLines](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Order_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Product_Name] [nvarchar](50) NOT NULL,
	[Product_Price] [money] NOT NULL,
	[Product_Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderLines] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderLines]  WITH CHECK ADD  CONSTRAINT [FK_OrderLines_Orders] FOREIGN KEY([Order_ID])
REFERENCES [dbo].[Orders] ([Order_ID])
GO

ALTER TABLE [dbo].[OrderLines] CHECK CONSTRAINT [FK_OrderLines_Orders]
GO

ALTER TABLE [dbo].[OrderLines]  WITH CHECK ADD  CONSTRAINT [FK_OrderLines_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([ID])
GO

ALTER TABLE [dbo].[OrderLines] CHECK CONSTRAINT [FK_OrderLines_Product]
GO

