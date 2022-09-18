USE [checkout-service];
Go

If NOT EXISTS (Select Table_Name from INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='Basket')
	BEGIN
		CREATE TABLE [dbo].[Basket] (
			BasketId INT PRIMARY KEY CLUSTERED IDENTITY (1, 1),
			CustomerName VARCHAR(100) NULL,
			PaysVat bit NOT NULL
		)
	END
GO

If NOT EXISTS (Select Table_Name from INFORMATION_SCHEMA.TABLES Where TABLE_SCHEMA='dbo' and TABLE_NAME='BasketProduct')
	BEGIN
		CREATE TABLE [dbo].[BasketProduct] (
			BasketProductId INT PRIMARY KEY CLUSTERED IDENTITY (1, 1),
			ProductName VARCHAR(100) NULL,
			ProductPrice DECIMAL(28,8)	NOT NULL,
			BasketId INT
		)
	END

GO


If NOT EXISTS (SELECT * FROM sys.indexes WHERE name='IX_BasketProduct_BasketId' AND object_id = OBJECT_ID('dbo.BasketProduct'))
  CREATE NONCLUSTERED INDEX  IX_BasketProduct_BasketId on dbo.BasketProduct (BasketId ASC)
GO

If NOT EXISTS (Select CONSTRAINT_NAME From INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
        WHERE CONSTRAINT_SCHEMA = 'dbo' and CONSTRAINT_NAME = 'FK_BasketProduct_BasketId')
    ALTER TABLE dbo.BasketProduct ADD CONSTRAINT FK_BasketProduct_BasketId FOREIGN KEY (BasketId) REFERENCES dbo.Basket(BasketId)
	
GO
