use FashionDream1


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


select *from UserAccount
create table Product
(

	ProductID int primary key not null identity(1000,1),
	ProductName varchar(255) not null,
	ProductImage varchar(255),
	ProductPrice int not null check(ProductPrice>0),
	ProductQuantity int not null,
	ProductDetails varchar(255),
	ProductType varchar(255)
	
)
delete from Product where ProductID=1007

select * from Product


create table Variation(
	VariationID int primary key identity(70000,1),
	ProductID int,
	Size varchar(255),
	Color varchar(255),
	ID int,

	constraint fk_uid_v foreign key(ID) references UserAccount(ID),
	constraint fk_pid_v foreign key(ProductID) references Product(ProductID)
)




select * from Variation
delete from Variation where ID = 0
create table Cart(
	CartID int Primary key identity(3000,1),
	ID int,
	ProductID int,
	TotalCost int,
	VariationID int,

	constraint fk_vid foreign key(VariationID) references Variation(VariationID),
	constraint fk_uid foreign key(ID) references UserAccount(ID),
	constraint fk_pid foreign key(ProductID) references Product(ProductID)
)

select *from Cart



ALTER TABLE Cart
ADD Quantity int;

select * from (Cart c join Product p  on c.ProductID=p.ProductID) join 
Variation v on c.VariationID=v.VariationID where c.ID = 0

select * from UserAccount left join Variation on UserAccount.ID = Variation.ID

create table history(
	OrderID int Primary key identity(10000,1),
	CartID int,
	ID int,
	ProductID int,
	TotalCost int,
	VariationID int,
	constraint fk_pid_order foreign key(ProductID) references Product(ProductID),
	constraint fk_uid_order foreign key(ID) references UserAccount(ID),
)

select *from history

create table Payment( 

	PaymentID int Primary key identity(5000,1),
	ID int,
	BillNo int,
	Method varchar(255),
	PaymentTime date,
	PaymentStatus varchar(255),

	constraint fk_uid_payment foreign key(ID) references UserAccount(ID),
)
select *from Payment

create table Feedback(

	FBID int primary key identity(6000,1),
	ID int,
	Name varchar(255),
	Email varchar(255),
	Review varchar(255),

	constraint fk_uid_feedback foreign key(ID) references UserAccount(ID),

)

select *from Feedback