-- Drop the database if it already exists
DROP DATABASE IF EXISTS HotelMike;

-- Create a new database
CREATE DATABASE HotelMike;

-- Select the newly created database
USE HotelMike;

-- Create a table for rooms
CREATE TABLE ROOMS(
    RoomID INT NOT NULL AUTO_INCREMENT,
    RoomNumber INT,
    SIZE VARCHAR(30) NOT NULL,
    PRIMARY KEY(RoomID)
);

-- Create a table for clients
CREATE TABLE Clients(
    ClientID INT NOT NULL AUTO_INCREMENT,
    ReservationID INT,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    RoomNumber VARCHAR(30),
    Mobile BIGINT,
    PRIMARY KEY(ClientID),
    FOREIGN KEY(ReservationID) REFERENCES RESERVATIONS(ReservationID)
);
 
-- Create a table for reservations
CREATE TABLE RESERVATIONS(
    ReservationID INT NOT NULL AUTO_INCREMENT,
    ClientID INT NOT NULL,
    CheckInDate VARCHAR(30) NOT NULL,
    CheckOutDate VARCHAR(30) NOT NULL,
    PRIMARY KEY (ReservationID),
    FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);

-- Create a table for staff
CREATE TABLE Staff(
    StaffID INT NOT NULL AUTO_INCREMENT,
    LogUserName VARCHAR(30) NOT NULL,
    LogPassword VARCHAR(30) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    PRIMARY KEY(StaffID)
);

-- Insert a staff member
INSERT INTO Staff (LogUserName, LogPassword, FirstName, LastName) 
VALUES ('m', 'm', 'Mike', 'Mastoros');

-- Grant privileges to the user
GRANT ALL PRIVILEGES ON HotelMike.* TO 'm'@'localhost' IDENTIFIED BY 'm';

-- Refresh privileges
FLUSH PRIVILEGES;

