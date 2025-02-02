-- PROCEDURE: Cadastrar Usuário
CREATE PROCEDURE [dbo].[CadastarUsuario]
    @Id UNIQUEIDENTIFIER,
    @Nome NVARCHAR(100),
    @Email NVARCHAR(200),
    @Senha NVARCHAR(200),
    @Role NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuarios (Id, Nome, Email, Senha, Role, DataCadastro, Ativo)
    VALUES (@Id, @Nome, @Email, @Senha, @Role, GETDATE(), 1);
	
    SELECT @Id AS Id;
END;
GO

-- PROCEDURE: Cadastrar Cliente (com logotipo em VARBINARY(MAX))
CREATE PROCEDURE [dbo].[CadastrarCliente]
    @ClienteId UNIQUEIDENTIFIER,
    @Nome NVARCHAR(255),
    @Email NVARCHAR(255),
    @Logotipo VARBINARY(MAX),  -- Alterado para VARBINARY(MAX)
	@LogradouroId UNIQUEIDENTIFIER,
    @Endereco NVARCHAR(255),
    @Cep NVARCHAR(20),
    @Bairro NVARCHAR(100),
    @Numero NVARCHAR(10),
    @Complemento NVARCHAR(255),
	@Cidade NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clientes (Id, Nome, Email, Logotipo, DataCadastro, Ativo)
    VALUES (@ClienteId, @Nome, @Email, @Logotipo, GETDATE(), 1);

    INSERT INTO Logradouros (Id, ClienteId, Endereco, Cep, Bairro, Numero, Complemento, Cidade, DataCadastro, Ativo)
    VALUES (@LogradouroId, @ClienteId, @Endereco, @Cep, @Bairro, @Numero, @Complemento, @Cidade, GETDATE(), 1);

    SELECT @ClienteId AS ClienteId;
END;
GO

-- PROCEDURE: Atualizar Cliente (com logotipo em VARBINARY(MAX))
CREATE PROCEDURE [dbo].[AtualizarCliente]
    @ClienteId UNIQUEIDENTIFIER,
    @Nome NVARCHAR(100) = NULL,
    @Email NVARCHAR(200) = NULL,
    @Logotipo VARBINARY(MAX) = NULL,  -- Alterado para VARBINARY(MAX)
    @LogradouroId UNIQUEIDENTIFIER = NULL,
    @Endereco NVARCHAR(200) = NULL,
    @Cep NVARCHAR(10) = NULL,
    @Bairro NVARCHAR(100) = NULL,
    @Numero NVARCHAR(20) = NULL,
    @Complemento NVARCHAR(200) = NULL,
	@Cidade NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET 
        Nome = COALESCE(@Nome, Nome),
        Email = COALESCE(@Email, Email),
        Logotipo = COALESCE(@Logotipo, Logotipo)
    WHERE Id = @ClienteId AND Ativo = 1;

    UPDATE Logradouros
    SET 
        Endereco = COALESCE(@Endereco, Endereco),
        Cep = COALESCE(@Cep, Cep),
        Bairro = COALESCE(@Bairro, Bairro),
        Numero = COALESCE(@Numero, Numero),
        Complemento = COALESCE(@Complemento, Complemento),
		Cidade = COALESCE(@Cidade, Cidade)
    WHERE ClienteId = @ClienteId AND Ativo = 1;
END;
GO

-- PROCEDURE: Desativar Cliente
CREATE PROCEDURE [dbo].[DesativarCliente]
    @ClienteId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Desativa o cliente
    UPDATE Clientes
    SET Ativo = 0
    WHERE Id = @ClienteId;

    -- Desativa os logradouros associados ao cliente
    UPDATE Logradouros
    SET Ativo = 0
    WHERE ClienteId = @ClienteId;
END;
GO

-- PROCEDURE: Cadastrar Logradouro
CREATE PROCEDURE [dbo].[CadastrarLogradouro]
    @Id UNIQUEIDENTIFIER,
    @Endereco NVARCHAR(255),
    @Cep NVARCHAR(20),
    @Bairro NVARCHAR(100),
    @Numero NVARCHAR(20),
    @Complemento NVARCHAR(255) = NULL,
	@Cidade NVARCHAR(255),
    @ClienteId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Logradouros (Id, Endereco, Cep, Bairro, Numero, Complemento, Cidade, ClienteId, DataCadastro, Ativo)
    VALUES (@Id, @Endereco, @Cep, @Bairro, @Numero, @Complemento, @Cidade, @ClienteId, GETDATE(), 1);
	
    SELECT @Id AS Id;
END;
GO

-- PROCEDURE: Atualizar Logradouro
CREATE PROCEDURE [dbo].[AtualizarLogradouro]
    @ClienteId UNIQUEIDENTIFIER,
    @Endereco NVARCHAR(255) = NULL,
    @Cep NVARCHAR(20) = NULL,
    @Bairro NVARCHAR(100) = NULL,
    @Numero NVARCHAR(10) = NULL,
    @Complemento NVARCHAR(100) = NULL,
    @Cidade NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Logradouros
    SET 
        Endereco = COALESCE(@Endereco, Endereco),
        Cep = COALESCE(@Cep, Cep),
        Bairro = COALESCE(@Bairro, Bairro),
        Numero = COALESCE(@Numero, Numero),
        Complemento = COALESCE(@Complemento, Complemento),
        Cidade = COALESCE(@Cidade, Cidade)
    WHERE ClienteId = @ClienteId AND Ativo = 1;
END;
GO

-- PROCEDURE: Desativar Logradouro
CREATE PROCEDURE [dbo].[DesativarLogradouro]
    @ClienteId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Logradouros
    SET Ativo = 0
    WHERE ClienteId = @ClienteId AND Ativo = 1;
END;
GO

EXEC CadastarUsuario
    @Id = '90185e0a-c803-4e3a-9a09-a79e30ebaebf',
    @Nome = 'Matheus Henrique',
    @Email = 'matheus@email.com',
    @Senha = '123456',
    @Role = 'admin';
