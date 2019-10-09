create table MyTable
(
	mt_id serial primary key,
	mt_a integer,
	mt_b integer,
	mt_sum integer
);

insert into MyTable(mt_a, mt_b, mt_sum) values(1, 2, 3);

select * from MyTable;