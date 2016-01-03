select *, ai.id analysis_input_id
from relevant_word rw
inner join information_type it
on rw.information_type_id = it.id
and it.area_id = 12
inner join analysis_input ai
on ai.text_input like '%' + rw.word + '%'
where rw.word like '% %'

select rw.word, SUM(rw.weight) subtotal_weight --*, ai.id analysis_input_id
from relevant_word rw
inner join information_type it
on rw.information_type_id = it.id
and it.area_id = 12
inner join analysis_input ai
on ai.text_input like '%' + rw.word + '%'
and ai.id = 631
where rw.word like '% %'
group by rw.word

select ISNULL(SUM(rw.weight), 0) subtotal_weight --*, ai.id analysis_input_id
from relevant_word rw
inner join information_type it
on rw.information_type_id = it.id
and it.area_id = 12
and it.id = 1
inner join analysis_input ai
on ai.text_input like '%' + rw.word + '%'
and ai.id = 631
where rw.word like '% %'

select sum(subtotal_weight) total_weight, information_type_id
from (
select sum(dbo.count_string(ai.text_input, rw.word) * rw.weight) subtotal_weight, it.id information_type_id
--select ISNULL(SUM(rw.weight), 0) subtotal_weight --*, ai.id analysis_input_id
--select *
from relevant_word rw
inner join information_type it
on rw.information_type_id = it.id
and it.area_id = 12
--and it.id = 1
inner join analysis_input ai
on ai.text_input like '%' + rw.word + '%'
and ai.id = 631
where rw.word like '% %'
group by it.id

union all


		select sum(rw.weight*iwh.word_count) subtotal_weight, rw.information_type_id
		from relevant_word rw
		inner join input_word_header  iwh
		on rw.word = iwh.word
		inner join information_type it
		on rw.information_type_id = it.id
		where iwh.analysis_input_id = 631
		and it.area_id = 12
		group by rw.information_type_id) innertable
		
		group by information_type_id
		order by total_weight desc