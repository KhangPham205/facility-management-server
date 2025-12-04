-- File: sql-init/init.sql
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QLCSVC')
BEGIN
    CREATE DATABASE QLCSVC;
END;
GO