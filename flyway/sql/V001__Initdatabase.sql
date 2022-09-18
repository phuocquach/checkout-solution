USE master;
Go

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'checkout-service')
  BEGIN
    CREATE DATABASE [checkout-service];
  END

GO