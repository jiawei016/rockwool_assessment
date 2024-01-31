IF NOT EXISTS (SELECT * FROM master.sys.databases WHERE name = N'NewsDb')
BEGIN
	CREATE DATABASE [NewsDb]
END

USE [NewsDb]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SearchHistory]'))
BEGIN
	DROP TABLE [dbo].[SearchHistory]
END
print('CREATE TABLE SearchHistory')
CREATE TABLE [SearchHistory]
(
	[SearchHistoryID] BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[SearchTitle] NVARCHAR(250) NULL,
	[SearchDate] DATETIME DEFAULT GETDATE() NULL
)


