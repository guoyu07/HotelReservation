USE [master]
GO

IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'CustomersRegistry')
CREATE DATABASE [CustomersRegistry]
GO