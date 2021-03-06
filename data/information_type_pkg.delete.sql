USE [iPattern]
GO
/****** Object:  StoredProcedure [dbo].[information_type_pkg.store]    Script Date: 03/29/2011 20:40:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[information_type_pkg.delete]
	-- Add the parameters for the stored procedure here
	@id int output,
	@area_id int
AS
BEGIN
	delete from analysis_result_item where analysis_result_id
	in (select id from analysis_result where information_type_id = @id);

	delete from analysis_result_item 
	where information_type_id = @id;
	--where analysis_result_id in 
	--(select id from analysis_result where information_type_id = @id);
	
	delete from meta_data_value where analysis_result_id in 
	(select id from analysis_result where information_type_id = @id);
	
	delete from analysis_result 
	where information_type_id = @id;

	--delete from input_meta_data where analysis_input_id = @id;
	--delete from attachment where analysis_input_id = @id;
	--delete from input_word_header where analysis_input_id = @id;
	
	delete from meta_data_key where information_type_id = @id;	
	delete from relevant_word where information_type_id = @id;
	delete from information_type where id = @id and area_id = @area_id;
END
