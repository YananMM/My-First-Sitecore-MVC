USE [master]
GO
/****** Object:  Database [Landmark_Forms]    Script Date: 11/24/2015 10:17:27 ******/
CREATE DATABASE [Landmark_Forms]
GO
ALTER DATABASE [Landmark_Forms] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Landmark_Forms].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Landmark_Forms] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Landmark_Forms] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Landmark_Forms] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Landmark_Forms] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Landmark_Forms] SET ARITHABORT OFF
GO
ALTER DATABASE [Landmark_Forms] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Landmark_Forms] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Landmark_Forms] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Landmark_Forms] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Landmark_Forms] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Landmark_Forms] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Landmark_Forms] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Landmark_Forms] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Landmark_Forms] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Landmark_Forms] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Landmark_Forms] SET  DISABLE_BROKER
GO
ALTER DATABASE [Landmark_Forms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Landmark_Forms] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Landmark_Forms] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Landmark_Forms] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Landmark_Forms] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Landmark_Forms] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Landmark_Forms] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Landmark_Forms] SET  READ_WRITE
GO
ALTER DATABASE [Landmark_Forms] SET RECOVERY FULL
GO
ALTER DATABASE [Landmark_Forms] SET  MULTI_USER
GO
ALTER DATABASE [Landmark_Forms] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Landmark_Forms] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'Landmark_Forms', N'ON'
GO
USE [Landmark_Forms]
GO
/****** Object:  Table [dbo].[EmailSignup]    Script Date: 11/24/2015 10:17:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSignup](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Channel] [nvarchar](50) NOT NULL,
	[Interest] [nvarchar](255) NULL,
	[Other Interest] [nvarchar](255) NULL,
	[Room] [nvarchar](50) NULL,
	[Building] [nvarchar](50) NULL,
	[Street] [nvarchar](50) NULL,
	[Area] [nvarchar](50) NULL,
	[District] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Postcode] [nvarchar](10) NULL,
	[Country] [nvarchar](50) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NULL,
	[OptIn] [bit] NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailSignup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
