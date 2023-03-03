drop DATABASE if exists HotelMike;
create database HotelMike;
 
use HotelMike;
 
create table CLIENTS
(
Clientid INT NOT NULL AUTO_INCREMENT,
Staffid INT,
FirstName VARCHAR(30),
LastName VARCHAR(30),
Mobile BIGINT,
ROOM INT,
PAID VARCHAR(30),
SIZE VARCHAR(30),
PRIMARY KEY(Clientid),
FOREIGN KEY(Staffid) REFERENCES Staff(Staffid)
);

 INSERT INTO CLIENTS VALUES (null,'1','Michalis','Mastoros','6948798568','22','Paid','Suite');
 
create table STAFF
(
Staffid INT NOT NULL AUTO_INCREMENT,
LogUserName VARCHAR(30),
LogPassword VARCHAR(30),
FirstName VARCHAR(30),
LastName VARCHAR(30),
PRIMARY KEY(Staffid)
);
 
INSERT INTO STAFF VALUES (null,'1','1','Michail','Mastoros');
 
 create table ROOMS
(
ROOMid INT NOT NULL AUTO_INCREMENT,
Clientid INT,
OCCUPANT VARCHAR(30),
ROOMNUMBER SMALLINT,
PRICE INT,
SIZE VARCHAR(30),
PRIMARY KEY(ROOMid),
FOREIGN KEY(Clientid) REFERENCES CLIENTS(Clientid)
);
SELECT * FROM CLIENTS;
SELECT * FROM STAFF;
SELECT * FROM ROOMS;
