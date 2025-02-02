using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeClientes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logotipo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logradouros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logradouros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logradouros_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logradouros_ClienteId",
                table: "Logradouros",
                column: "ClienteId");

            migrationBuilder.Sql(@"
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
                @Logotipo VARBINARY(MAX),
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
                @Logotipo VARBINARY(MAX) = NULL,
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
                UPDATE Clientes
                SET Ativo = 0
                WHERE Id = @ClienteId;

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

            -- Chamada para cadastrar usuário (exemplo)
            EXEC CadastarUsuario
                @Id = '90185e0a-c803-4e3a-9a09-a79e30ebaebf',
                @Nome = 'Matheus Henrique',
                @Email = 'matheus@email.com',
                @Senha = '123456',
                @Role = 'admin';

            -- Exec para cadastrar cliente
            EXEC [dbo].[CadastrarCliente]
                @ClienteId = 'c0e2e7f8-2ef2-4a8b-9f13-845f52014abf',
                @Nome = 'Cliente Exemplo',
                @Email = 'cliente@exemplo.com',
                @Logotipo = 0x1234567890ABCDEF,  -- Exemplo de logotipo em VARBINARY(MAX)
                @LogradouroId = '4f2b3032-4a8c-42f3-9e9e-c5ed3fe28a72',
                @Endereco = 'Rua Exemplo, 123',
                @Cep = '12345-678',
                @Bairro = 'Bairro Exemplo',
                @Numero = '123',
                @Complemento = 'Apartamento 101',
                @Cidade = 'Cidade Exemplo';
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logradouros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[CadastarUsuario]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[CadastrarCliente]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[AtualizarCliente]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DesativarCliente]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[CadastrarLogradouro]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[AtualizarLogradouro]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[DesativarLogradouro]");
        }
    }
}
