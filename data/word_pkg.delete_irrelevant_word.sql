USE [iPattern]
GO
/****** Object:  StoredProcedure [dbo].[word_pkg.store_irrelevant_word]    Script Date: 03/29/2011 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [word_pkg.delete_irrelevant_word]
	-- Add the parameters for the stored procedure here
	@word nvarchar(150),
	@area_id int
AS
BEGIN
	delete from irrelevant_word where word = @word and area_id = @area_id;
END
