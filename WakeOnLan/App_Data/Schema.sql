if not exists ( select * from sys.databases where name = 'WakeOnLan')
begin
   create database WakeOnLan
end

go

use 

if exists ( select * from sys.objects where name = 'Computer' and type='U' )
begin
   drop table Computer 
end

go

create table Computer (ComputerID int identity(1,1), 
                       DisplayName varchar(255),
                       Hostname varchar(255),
                       macAddress varchar(255) )

-------------------------------

if exists ( select * from sys.objects where name = 'GetAllComputers' and type='P' )
begin
   drop procedure GetAllComputers 
end

GO

create procedure GetAllComputers  
as
   select ComputerID,  DisplayName, hostName, macAddress from Computer 
go

-------------------------------

if exists ( select * from sys.objects where name = 'GetStatusOfComputers' and type='P' )
begin
   drop procedure GetStatusOfComputers 
end

GO

create procedure GetStatusOfComputers  ( @displayName varchar(255))
as
   select ComputerID,  DisplayName, hostName, macAddress from Computer 
  -- where DisplayName = @displayName
   order by hostName
go


-------------------------------


if exists ( select * from sys.objects where name = 'CreateNewComputer' and type='P' )
begin
   drop procedure CreateNewComputer 
end

GO

create procedure CreateNewComputer ( @displayName varchar(255), @hostName varchar(255), @macAddress varchar(255) )
as
    insert into computer ( displayName, hostName, macAddress) values (@displayname, @hostname, @macAddress )
go

-------------------------------

if exists ( select * from sys.objects where name = 'GetMACAddress' and type='P' )
begin
   drop procedure GetMACAddress 
end

GO

create procedure GetMACAddress ( @computerId int )
as
   select macAddress from computer where computerId = @computerId 
go

-------------------------------

if exists ( select * from sys.objects where name = 'GetMACAddressByHostName' and type='P' )
begin
   drop procedure GetMACAddressByHostName 
end

GO

create procedure GetMACAddressByHostName ( @hostName varchar(255) )
as
   select macAddress from computer where hostName = @hostName 
go

-------------------------------

if exists ( select * from sys.objects where name = 'GetOwners' and type='P' )
begin
   drop procedure GetOwners 
end

GO

create procedure GetOwners 
as
   select distinct DisplayName from computer
go

-------------------------------

if exists ( select * from sys.objects where name = 'DeleteComputer' and type='P' )
begin
   drop procedure DeleteComputer 
end

GO

create procedure DeleteComputer( @computerId int )
as
   delete from computer where computerId = @computerId
go

-------------------------------

CREATE ROLE [WakeOnLanServer] AUTHORIZATION [dbo]

GRANT  EXECUTE ON [dbo].[CreateNewComputer] TO [WakeOnLanServer]
GRANT  EXECUTE ON [dbo].[DeleteComputer] TO [WakeOnLanServer]
GRANT  EXECUTE ON [dbo].[GetAllComputers] TO [WakeOnLanServer]
GRANT  EXECUTE ON [dbo].[GetMACAddress] TO [WakeOnLanServer]
GRANT  EXECUTE ON [dbo].[GetMACAddressByHostName] TO [WakeOnLanServer]
GRANT  EXECUTE ON [dbo].[GetStatusOfComputers] TO [WakeOnLanServer]
GRANT  EXECUTE ON [dbo].[GetOwners] TO [WakeOnLanServer]
