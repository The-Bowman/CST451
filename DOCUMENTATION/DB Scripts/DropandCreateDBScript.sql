USE [master]
GO

/****** Object:  Database [dbPCPARTS]    Script Date: 9/11/2022 9:17:26 PM ******/
DROP DATABASE [dbPCPARTS]
GO

/****** Object:  Database [dbPCPARTS]    Script Date: 9/11/2022 9:17:26 PM ******/
CREATE DATABASE [dbPCPARTS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbPCPARTS', FILENAME = N'C:\Users\wiseb\dbPCPARTS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbPCPARTS_log', FILENAME = N'C:\Users\wiseb\dbPCPARTS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbPCPARTS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [dbPCPARTS] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [dbPCPARTS] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [dbPCPARTS] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [dbPCPARTS] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [dbPCPARTS] SET ARITHABORT OFF 
GO

ALTER DATABASE [dbPCPARTS] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [dbPCPARTS] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [dbPCPARTS] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [dbPCPARTS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [dbPCPARTS] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [dbPCPARTS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [dbPCPARTS] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [dbPCPARTS] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [dbPCPARTS] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [dbPCPARTS] SET  DISABLE_BROKER 
GO

ALTER DATABASE [dbPCPARTS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [dbPCPARTS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [dbPCPARTS] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [dbPCPARTS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [dbPCPARTS] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [dbPCPARTS] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [dbPCPARTS] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [dbPCPARTS] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [dbPCPARTS] SET  MULTI_USER 
GO

ALTER DATABASE [dbPCPARTS] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [dbPCPARTS] SET DB_CHAINING OFF 
GO

ALTER DATABASE [dbPCPARTS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [dbPCPARTS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [dbPCPARTS] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [dbPCPARTS] SET QUERY_STORE = OFF
GO

USE [dbPCPARTS]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [dbPCPARTS] SET  READ_WRITE 
GO

