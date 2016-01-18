use master
go
if exists(select 1 from dbo.sysdatabases where name = 'AuthApp')
	drop database [AuthApp]
go
create Database [AuthApp]
ON
( NAME = 'AuthApp_Data',
	FILENAME = 'D:\SQLServer\AuthApp_Data.MDF',
	FILEROWTH = 10% )
LOG ON
(NAME = 'AuthApp_Log',
	FILENAME = 'D:\SQLServer\AuthApp_Log.LDF',
	FILEROWTH = 10%
)
GO