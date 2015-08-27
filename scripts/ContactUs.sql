USE [LandmarkSitecore_Master]
GO

/****** Object:  Table [dbo].[ContactUsForms]    Script Date: 08/27/2015 09:53:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContactUsForms](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](200) NOT NULL,
	[EnquiryType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ContactUsForms] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

