
use [NewsDb]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spInsertSearchHistory]'))
BEGIN
	DROP PROCEDURE spInsertSearchHistory
END
GO
CREATE PROCEDURE spInsertSearchHistory
(
    @SearchTitle nvarchar(250)
)
AS
BEGIN
    INSERT INTO [SearchHistory]
	(SearchTitle, SearchDate)
	VALUES
	(@SearchTitle, GETDATE())
END
GO
