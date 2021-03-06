USE [iPattern]
GO
/****** Object:  StoredProcedure [dbo].[result_pkg.generate_result_items]    Script Date: 05/25/2011 09:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[result_pkg.generate_result_items]
	@new_analysis_result_id int,
	@information_type_id int,
	@analysis_input_id int,
	@run_neural_analysis bit,
	@area_id int
AS
BEGIN
	-- Analysis Result Items ----------------------------		    
    insert into analysis_result_item (analysis_result_id, word, [weight], information_type_id)
	select @new_analysis_result_id, rw.word, rw.weight, rw.information_type_id
	from relevant_word rw
	inner join information_type it
	on rw.information_type_id = it.id
	inner join input_word_header  iwh
	on rw.word = iwh.word
	where iwh.analysis_input_id = @analysis_input_id
	and it.area_id = @area_id
	
	union all
	
	select @new_analysis_result_id, rw.word, rw.weight, it.id information_type_id
	from relevant_word rw
	inner join information_type it
	on rw.information_type_id = it.id
	and it.area_id = @area_id
	and it.id = @information_type_id
	inner join analysis_input ai
	on ai.text_input like '%' + rw.word + '%'
	and ai.id = @analysis_input_id
	where rw.word like '% %';
	
	-- ******************************************
	
	if(@run_neural_analysis = 1)
	begin
		exec [analysis_pkg.run_neural_analysis] @information_type_id, @area_id
	end
	
	-- For now run again, to ensure that automatic created words are added aswell
    delete from analysis_result_item where analysis_result_id = @new_analysis_result_id;
    
    insert into analysis_result_item (analysis_result_id, word, [weight], information_type_id)
	select @new_analysis_result_id, rw.word, rw.weight, rw.information_type_id
	from relevant_word rw
	inner join information_type it
	on rw.information_type_id = it.id
	inner join input_word_header  iwh
	on rw.word = iwh.word
	where iwh.analysis_input_id = @analysis_input_id
	and it.area_id = @area_id
	
	union all
	
	select @new_analysis_result_id, rw.word, rw.weight, it.id information_type_id
	from relevant_word rw
	inner join information_type it
	on rw.information_type_id = it.id
	and it.area_id = @area_id
	and it.id = @information_type_id
	inner join analysis_input ai
	on ai.text_input like '%' + rw.word + '%'
	and ai.id = @analysis_input_id
	where rw.word like '% %';	
	
	-- *****************************************************
END
