Create table Genres(
Id TinyInt not null primary key,
Name nvarchar(255) not null)
Insert into Genres Values(1,'Jazz'),(2,'Blues'),(3,'Rock'),(4,'Country')


Create table Gigs(
ID int identity not null primary key,
DateTime DateTime not null ,
Venue nvarchar(255) not null,
Artist_Id nvarchar(128) not null
Constraint FK_Artist_Id Foreign Key(Artist_Id)
 references AspNetUsers(Id),
Genre_id tinyint not null
Constraint FK_Genre_id Foreign Key(Genre_id)
 references Genres(Id))
 Alter table AspNetUsers
 Add  Name nvarchar(255) 
 update AspNetUsers
 set Name='James Morrison'
 where [Id]='227a6f53-d76b-4292-bd17-d5efc1ee397b'
 create table Attendances(
 Id int not null primary key identity,
 GigId int not null
 Constraint Fk_GigId Foreign key(GigId)
 references Gigs(ID),
 AttendeeId nvarchar(128)  not null
 Constraint Fk_AttendeeId Foreign Key(AttendeeId)
 references AspNetUsers(Id)
 )
 create table follow(
 Id int not null primary key identity,
  UserId nvarchar(128)  not null
 Constraint Fk_UserId Foreign Key(UserId)
 references AspNetUsers(Id),
  ArtistId nvarchar(128)  not null
 Constraint Fk_ArtistId Foreign Key(ArtistId)
 references AspNetUsers(Id)
 )
 select * from follow
