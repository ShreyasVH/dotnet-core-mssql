create table books
(
	Id int identity
		constraint books_pk
			primary key nonclustered,
	Name varchar(25) not null,
	Author varchar(25) not null
)
go