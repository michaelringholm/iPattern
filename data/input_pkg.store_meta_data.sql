USE [iPattern]
GO
/****** Object:  StoredProcedure [dbo].[input_pkg.store_word_header]    Script Date: 03/29/2011 12:34:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [input_pkg.store_meta_data]
	-- Add the parameters for the stored procedure here
	@title nvarchar(150),
	@meta_value nvarchar(500),
	@analysis_input_id int
AS
BEGIN
	insert into input_meta_data(title, meta_value, analysis_input_id)
	values(@title, @meta_value, @analysis_input_id);
END
