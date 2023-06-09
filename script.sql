USE [master]
GO
/****** Object:  Database [proje2]    Script Date: 9.06.2023 14:00:34 ******/
CREATE DATABASE [proje2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'proje2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\proje2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'proje2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\proje2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [proje2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [proje2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [proje2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [proje2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [proje2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [proje2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [proje2] SET ARITHABORT OFF 
GO
ALTER DATABASE [proje2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [proje2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [proje2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [proje2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [proje2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [proje2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [proje2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [proje2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [proje2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [proje2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [proje2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [proje2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [proje2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [proje2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [proje2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [proje2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [proje2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [proje2] SET RECOVERY FULL 
GO
ALTER DATABASE [proje2] SET  MULTI_USER 
GO
ALTER DATABASE [proje2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [proje2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [proje2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [proje2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [proje2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [proje2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'proje2', N'ON'
GO
ALTER DATABASE [proje2] SET QUERY_STORE = ON
GO
ALTER DATABASE [proje2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [proje2]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 9.06.2023 14:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[book_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[isbn] [nvarchar](50) NOT NULL,
	[pusblish_year] [int] NOT NULL,
	[cover_price] [decimal](18, 0) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[book_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Borrowers]    Script Date: 9.06.2023 14:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Borrowers](
	[borrower_id] [int] IDENTITY(100,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[mobile_number] [nvarchar](50) NOT NULL,
	[national_id] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Borrowers] PRIMARY KEY CLUSTERED 
(
	[borrower_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Checkouts]    Script Date: 9.06.2023 14:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Checkouts](
	[chekout_id] [int] IDENTITY(1000,1) NOT NULL,
	[book_id] [int] NOT NULL,
	[borrower_id] [int] NOT NULL,
	[chekout_date] [datetime] NOT NULL,
	[return_date] [datetime] NOT NULL,
	[actual_return_day] [datetime] NOT NULL,
	[late_penalty] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Checkouts] PRIMARY KEY CLUSTERED 
(
	[chekout_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([book_id], [title], [isbn], [pusblish_year], [cover_price], [status]) VALUES (1, N'Kitap1', N'12313546', 2001, CAST(50 AS Decimal(18, 0)), 1)
INSERT [dbo].[Books] ([book_id], [title], [isbn], [pusblish_year], [cover_price], [status]) VALUES (2, N'Kitap2', N'12345612', 1999, CAST(45 AS Decimal(18, 0)), 1)
INSERT [dbo].[Books] ([book_id], [title], [isbn], [pusblish_year], [cover_price], [status]) VALUES (3, N'Kitap3', N'12456789', 2005, CAST(30 AS Decimal(18, 0)), 1)
INSERT [dbo].[Books] ([book_id], [title], [isbn], [pusblish_year], [cover_price], [status]) VALUES (4, N'Kitap4', N'15792123', 2016, CAST(40 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Borrowers] ON 

INSERT [dbo].[Borrowers] ([borrower_id], [name], [mobile_number], [national_id]) VALUES (140, N'Kerem', N'22-4444 333', N'11480222684')
SET IDENTITY_INSERT [dbo].[Borrowers] OFF
GO
SET IDENTITY_INSERT [dbo].[Checkouts] ON 

INSERT [dbo].[Checkouts] ([chekout_id], [book_id], [borrower_id], [chekout_date], [return_date], [actual_return_day], [late_penalty]) VALUES (1017, 1, 140, CAST(N'2023-06-08T00:00:00.000' AS DateTime), CAST(N'2023-06-23T00:00:00.000' AS DateTime), CAST(N'2023-06-08T00:00:00.000' AS DateTime), CAST(0 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Checkouts] OFF
GO
ALTER TABLE [dbo].[Checkouts]  WITH CHECK ADD  CONSTRAINT [FK_Checkouts_Books] FOREIGN KEY([book_id])
REFERENCES [dbo].[Books] ([book_id])
GO
ALTER TABLE [dbo].[Checkouts] CHECK CONSTRAINT [FK_Checkouts_Books]
GO
ALTER TABLE [dbo].[Checkouts]  WITH CHECK ADD  CONSTRAINT [FK_Checkouts_Borrowers] FOREIGN KEY([borrower_id])
REFERENCES [dbo].[Borrowers] ([borrower_id])
GO
ALTER TABLE [dbo].[Checkouts] CHECK CONSTRAINT [FK_Checkouts_Borrowers]
GO
USE [master]
GO
ALTER DATABASE [proje2] SET  READ_WRITE 
GO
