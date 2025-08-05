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
    PRINT 'ALERTA !!! - Banco de dados "TManagementOrdersDB" j� existe.';
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
            object_id = OBJECT_ID(N'[dbo].[Orders]')
            AND type = N'U'
    ) BEGIN CREATE TABLE [dbo].[Orders] (
        [Id] INT IDENTITY(1, 1) PRIMARY KEY,
        [IdClient] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Client](Id),
        [DataPedido] DATETIME NOT NULL DEFAULT GETDATE(),
        [Total] DECIMAL(18, 2) NOT NULL,
        [Status] INT NOT NULL
    );
	PRINT 'Tabela Orders criada'
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
        [IdOrder] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Orders](Id),
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