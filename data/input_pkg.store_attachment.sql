USE [iPattern]
GO
/****** Object:  StoredProcedure [dbo].[input_pkg.store_attachment]    Script Date: 03/29/2011 18:01:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[input_pkg.store_attachment]
	-- Add the parameters for the stored procedure here
	@analysis_input_id int,
	@title nvarchar(150),
	@binary_data image,
	@filename nvarchar(500)
AS
BEGIN
	insert into attachment(title, binary_data, analysis_input_id, [filename])
	values(@title, @binary_data, @analysis_input_id, @filename);
END
