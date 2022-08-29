use FashionDream


create table UserAccount
(
	ID int identity(0,1) primary key not null,
	FirstName varchar(255) not null,
	LastName varchar(255),
	Address varchar(255) not null,
	Phone varchar(255) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	DateOfBirth date,

	Constraint ck_phone check(Phone like '___________' AND Phone like '01%' ),
	Constraint ck_email check(Email like '%@gmail.com' OR Email like '%@aust.edu' OR Email like '%@yahoo.com'),
	Constraint ck_password check(LEN([Password]) >=6)
)
select * from UserAccount

create table Product
(

	ProductID int primary key not null identity(1000,1),
	ProductName varchar(255) not null,
	ProductImage varchar(255),
	ProductPrice int not null check(ProductPrice>0),
	ProductQuantity int not null,
	ProductDetails varchar(255)
	
)
select * from Product
