USE [ShopBridge]
GO
/****** Object:  Table [dbo].[ItemDetails]    Script Date: 10/8/2020 11:59:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemDetails](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](4000) NULL,
	[Price] [float] NULL,
	[StreamId] [varbinary](max) NULL,
	[Removed] [bit] NOT NULL,
 CONSTRAINT [PK_ItemDetails] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ItemDetails] ADD  CONSTRAINT [DF_ItemDetails_Removed]  DEFAULT ((0)) FOR [Removed]
GO
