create database mcf;
go

use mcf;
go

create table ms_storage_location
(
	location_id varchar(10) primary key,
	location_name varchar(100)
);

create table ms_user
(
	user_id bigint primary key,
	user_name varchar(20),
	password varchar(50),
	is_active bit
);

create table tr_bpkb
(
	agreement_number varchar(100) primary key,
	bpkb_no varchar(100),
	branch_id varchar(10),
	bpkb_date datetime,
	faktur_no varchar(100),
	faktur_date datetime,
	location_id varchar(10),
	police_no varchar(20),
	bpkb_date_in datetime,
	created_by varchar(10),
	created_on datetime,
	last_updated_by varchar(10),
	last_updated_on datetime,
	constraint FK_tr_bpkb_ms_storage_location foreign key (location_id) references ms_storage_location(location_id)
);

select * from tr_bpkb;

insert into tr_bpkb (agreement_number, bpkb_no, branch_id, bpkb_date, faktur_no, faktur_date, location_id, police_no, bpkb_date_in, created_by, created_on, last_updated_by, last_updated_on)
values ('agreement 1', 'bpkb no 1', 'branch 1', '2024-01-01', 'faktur no 1', '2024-01-01', 'location 1', 'police no 1', '2024-01-01', 'delfan', '2024-01-01', 'andhika', '2024-01-01');

select * from ms_storage_location;

insert into ms_storage_location (location_id, location_name) values ('location 1', 'jakarta');
insert into ms_storage_location (location_id, location_name) values ('location 2', 'bekasi');
insert into ms_storage_location (location_id, location_name) values ('location 3', 'bogor');
insert into ms_storage_location (location_id, location_name) values ('location 4', 'depok');
insert into ms_storage_location (location_id, location_name) values ('location 5', 'tangerang');

select * from ms_user;

insert into ms_user (user_id, user_name, password, is_active) values (1, 'jhonUmiro', 'admin1*', 1);
insert into ms_user (user_id, user_name, password, is_active) values (2, 'trisNatan', 'admin2@', 1);
insert into ms_user (user_id, user_name, password, is_active) values (3, 'hugoRess', 'admin3#', 0);