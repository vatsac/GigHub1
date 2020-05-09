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