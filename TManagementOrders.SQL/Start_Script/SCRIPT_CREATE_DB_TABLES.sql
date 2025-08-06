IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = N'TManagementOrdersDB'
)
BEGIN
    CREATE DATABASE TManagementOrdersDB;
    PRINT 'Banco de dados "TManagementOrdersDB" criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'ALERTA !!! - Banco de dados "TManagementOrdersDB" ja existe.';
END
GO
--////////////////////////////////////////////////////////////

USE TManagementOrdersDB;

--////////////////////////////////////////////////////////////

IF NOT EXISTS (
        SELECT
            *
        FROM
            sys.objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Client]')
            AND type = N'U'
    ) 
BEGIN 
	CREATE TABLE [dbo].[Client] (
			[Id] INT IDENTITY(1, 1) PRIMARY KEY,
			[Name] NVARCHAR(100) NOT NULL,
			[Email] NVARCHAR(100) NOT NULL,
			[Telephone] VARCHAR(20) NOT NULL,
			[DateRegister] DATETIME NOT NULL DEFAULT GETDATE()
		);

	PRINT 'Tabela Client criada'
END
ELSE 
BEGIN 
	PRINT 'ALERTA !!! - Tabela Client ja exite';
END
GO

--////////////////////////////////////////////////////////////

   IF NOT EXISTS (
        SELECT
            *
        FROM
            sys.objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Product]')
            AND type = N'U'
    ) BEGIN CREATE TABLE [dbo].[Product] (
        [Id] INT IDENTITY(1, 1) PRIMARY KEY,
        [Name] NVARCHAR(100) NOT NULL,
        [Description] NVARCHAR(255) NULL,
        [Price] DECIMAL(18, 2) NOT NULL,
        [Quantity] INT NOT NULL
    );
	PRINT 'Tabela Product criada'
END
ELSE 
BEGIN 
	PRINT 'ALERTA !!! - Tabela Product ja exite';
END
GO

--////////////////////////////////////////////////////////////

 IF NOT EXISTS (
          SELECT
            *
        FROM
            sys.objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[Order]')
            AND type = N'U'
    ) BEGIN CREATE TABLE [dbo].[Order] (
        [Id] INT IDENTITY(1, 1) PRIMARY KEY,
        [IdClient] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Client](Id),
        [DateOrder] DATETIME NOT NULL DEFAULT GETDATE(),
        [Total] DECIMAL(18, 2) NOT NULL,
        [Status] INT NOT NULL
    );
	PRINT 'Tabela Order criada'
END
ELSE 
BEGIN 
	PRINT 'ALERTA !!! - Tabela Orders ja exite';
END
GO

--////////////////////////////////////////////////////////////

 IF NOT EXISTS (
        SELECT
            *
        FROM
            sys.objects
        WHERE
            object_id = OBJECT_ID(N'[dbo].[OrderItem]')
            AND type = N'U'
    ) BEGIN CREATE TABLE [dbo].[OrderItem] (
        [Id] INT IDENTITY(1, 1) PRIMARY KEY,
        [IdOrder] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Order](Id),
        [IdProduct] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Product](Id),
        [Quantity] INT NOT NULL,
        [UnitPrice] DECIMAL(18, 2) NOT NULL
    );
	PRINT 'Tabela OrderItem criada'
END
ELSE 
BEGIN 
	PRINT 'ALERTA !!! - Tabela OrderItem ja exite';
END
GO

--////////////////////////////////////////////////////////////
--      CARGA DE DADOS INICIAIS 

INSERT INTO [dbo].[Client]
           ([Name]
           ,[Email]
           ,[Telephone]
           ,[DateRegister])
     VALUES
           ('Ragnar lothbrok',
           'ragnar_lg@firstpalace.com',
           '11589785069',
           GETDATE())
GO

INSERT INTO [dbo].[Client]
           ([Name]
           ,[Email]
           ,[Telephone]
           ,[DateRegister])
     VALUES
           ('Nicola Tesla',
           'energy@firstpalace.com',
           '11269021458',
           GETDATE())
GO

INSERT INTO [dbo].[Client]
           ([Name]
           ,[Email]
           ,[Telephone]
           ,[DateRegister])
     VALUES
           ('Mike tyson',
           'punchpunch@firstpalace.com',
           '11269021458',
           GETDATE())
GO

PRINT 'Tabela Client Carregada'

--///////////////////////////////////////////////////////

INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Price]
           ,[Quantity])
     VALUES
           ('Iphone 16'
           ,'The best Iphone'
           ,7000.00
           ,20)
GO

INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Price]
           ,[Quantity])
     VALUES
           ('Samsung S21'
           ,'Excellent mobile'
           ,3000.00
           ,90)
GO

INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Price]
           ,[Quantity])
     VALUES
           ('Notebook Dell'
           ,'Simplemente o melhor de todos'
           ,4000.00
           ,10)
GO

INSERT INTO [dbo].[Product]
           ([Name]
           ,[Description]
           ,[Price]
           ,[Quantity])
     VALUES
           ('Cell LG 22'
           ,'NÃO COMPRE É HORRIVEL'
           ,0.50
           ,1)
GO

PRINT 'Tabela Product Carregada'
