USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'CustomersRegistry')
DROP DATABASE [CustomersRegistry]
GO