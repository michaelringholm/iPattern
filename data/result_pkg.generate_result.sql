USE [iPattern]
GO
/****** Object:  StoredProcedure [dbo].[result_pkg.generate_result]    Script Date: 05/25/2011 09:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[result_pkg.generate_result]
	@area_id int,
	@analysis_input_id int
AS
BEGIN
DECLARE 
	@analysis_result_id int,
	@new_analysis_result_id int,
	@run_neural_analysis bit,
	@multiword_weight int;

	set @run_neural_analysis = 1;
	
	declare @total_weight int, @information_type_id int, @certain_limit int, @result_count int = 0, @is_read bit;


	declare cs_ cursor for
		select sum(subtotal_weight) total_weight, information_type_id
		from 
		(
			select sum(dbo.count_string(ai.text_input, rw.word) * rw.weight) subtotal_weight, it.id information_type_id
			from relevant_word rw
			inner join information_type it
			on rw.information_type_id = it.id
			and it.area_id = @area_id
			and it.id = @information_type_id
			inner join analysis_input ai
			on ai.text_input like '%' + rw.word + '%'
			and ai.id = @analysis_input_id
			where rw.word like '% %'
			group by it.id
			
			union all
			
			select sum(rw.weight*iwh.word_count) subtotal_weight, rw.information_type_id
			from relevant_word rw
			inner join input_word_header  iwh
			on rw.word = iwh.word
			inner join information_type it
			on rw.information_type_id = it.id
			where iwh.analysis_input_id = @analysis_input_id
			and it.area_id = @area_id
			group by rw.information_type_id
		) innertable
		
		group by information_type_id
		order by total_weight desc;

	-- Clear and loop
	delete from analysis_result_item 
	where analysis_result_id in (select id from analysis_result where analysis_input_id = @analysis_input_id);
	delete from meta_data_value where analysis_result_id in (select id from analysis_result 
	where analysis_input_id = @analysis_input_id);
	delete from analysis_result where analysis_input_id = @analysis_input_id;

	if exists( select id from input_meta_data where title = 'mailbox_folder' 
				and analysis_input_id = @analysis_input_id 
				and meta_value = 'sent' )
		set @is_read = 1;
		else set @is_read = 0;
	
	open cs_;
	fetch cs_ into @total_weight, @information_type_id;

	while(@@FETCH_STATUS <> -1)
	begin
		set @certain_limit = (select certain_limit from information_type it where it.id = @information_type_id);
		
		if(@total_weight >= @certain_limit)
		begin
			set @run_neural_analysis = 1;
			
			insert into analysis_result(analysis_input_id, event_time, information_type_id, is_read)
			values (@analysis_input_id, GetDate(), @information_type_id, @is_read);
		
			set @new_analysis_result_id = scope_identity();
			
			exec [result_pkg.generate_result_items] @new_analysis_result_id, @information_type_id, 
			@analysis_input_id, @run_neural_analysis, @area_id;
				
			set @result_count = @result_count+1;
		end;
		fetch cs_ into @total_weight, @information_type_id;
	end;

	close cs_;
	deallocate cs_;
	-- End of Clear and loop
	
	
	-- Insert one result with type unknown if no results were found
	if(@result_count = 0)
	begin
		set @information_type_id = (select id from information_type it where it.area_id = @area_id and it.title = 'Unknown');
		if(@information_type_id is null)
		begin
			exec [information_type_pkg.store] @information_type_id out, 'Unknown', 0, 0, null, @area_id;
		end;
		
		set @run_neural_analysis = 0;
		
		insert into analysis_result(analysis_input_id, event_time, information_type_id, is_read)
		values (@analysis_input_id, GetDate(), @information_type_id, @is_read);
	
		set @new_analysis_result_id = scope_identity();
		
		exec [result_pkg.generate_result_items] @new_analysis_result_id, @information_type_id, 
		@analysis_input_id, @run_neural_analysis, @area_id;
	end;
	-- End of Insert one result with type unknown if no results were found
  
	update [analysis_input] set [status] = 'ANALYZED', event_time = GetDate() where id = @analysis_input_id;
END
