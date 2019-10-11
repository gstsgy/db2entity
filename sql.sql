SELECT * FROM USER_TABLES;
select table_name from user_tables; 

select A.COLUMN_NAME,A.DATA_TYPE,A.DATA_LENGTH,A.DATA_SCALE  from user_tab_columns A
where TABLE_NAME='EMP';
