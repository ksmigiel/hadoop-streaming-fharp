words = load '/user/ksmigiel/out/part-00000' using PigStorage() as (word:chararray, count:int);
words_long = filter words by size(word) > 3;
words_ordered = order words_long by count desc;
top10 = limit words_ordered 10;
dump top10;
