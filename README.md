# TesteTecnicoCadastroDeClientes

Este projeto consiste em um sistema de cadastro de clientes, que utiliza Docker para criação do banco de dados, migrações do Entity Framework e execução de uma API e aplicação de apresentação utilizabndo o padrão CQRS.

## Pré-requisitos

- [Docker](https://www.docker.com/get-started)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [VSCode](https://code.visualstudio.com/)

## Passo a Passo

### 1. Criar o Banco de Dados com Docker

No nível da pasta `TesteTecnicoCadastroDeClientes`, execute os seguintes comandos:

```bash
docker build -t banco-de-dados-cadastro-cliente .
```
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=senhaSqlServer123!' -p 1433:1433 --name banco-de-dados-cadastro-cliente -d banco-de-dados-cadastro-cliente
```

### 2. Executar a Migration
Atualize o banco de dados executando a migration:

```bash
dotnet ef database update --project CadastroDeClientes/CadastroDeClientes.Infrastructure --startup-project CadastroDeClientes/CadastroDeClientes.API
```

### 3. Executar o Projeto

#### 3.1 - Pelo Visual Studio

- Abra a solution no Visual Studio.
- Execute o projeto clicando no botão **Start**.

#### 3.2 - Pelo VSCode

Em terminais diferentes, execute os comandos abaixo:

```bash
dotnet run --project CadastroDeClientes.API
```

```bash
dotnet run --project CadastroDeClientes.Presentation
```

### 4. Acessar a Aplicação

Após iniciar os projetos, acesse os seguintes links:

- **Swagger da API:** [https://localhost:7226/swagger/index.html](https://localhost:7226/swagger/index.html)
- **Aplicação de Apresentação:** [http://localhost:5247](http://localhost:5247)

