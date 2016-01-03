select word, count(word) total_message_count, sum(word_count) total_word_count
from analysis_result ar
inner join input_word_header iwh
on iwh.analysis_input_id = ar.analysis_input_id
where iwh.word not in (select word from irrelevant_word)
and ar.information_type_id = 3
group by word
having count(word) > 1