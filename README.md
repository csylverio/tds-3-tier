# MyFinance – API 3‑Tier com ASP.NET Core e EF Core (Projeto Acadêmico)

## Visão Geral

MyFinance é uma Web API em ASP.NET Core (.NET 9) construída em uma arquitetura 3 camadas (API, Business, DataAccess), com autenticação JWT, persistência com Entity Framework Core e PostgreSQL. O objetivo é didático: servir de base para estudos de APIs REST, autenticação/autorização e acesso a dados com EF Core.

Estrutura da solução:

- MyFinance.Api: camada de apresentação (controllers, DTOs, autenticação, middleware, Swagger).
- MyFinance.Business: domínio e contratos (entidades, interfaces de repositório e serviços).
- MyFinance.DataAccess: infraestrutura de dados (DbContext, repositórios, migrations EF Core).
- MyFinance.Tests: testes unitários e de integração com xUnit, Moq e EF Core InMemory.

Tecnologias principais:

- .NET 9, ASP.NET Core Web API, C#
- EF Core + Npgsql (PostgreSQL)
- JWT Bearer Authentication
- Swashbuckle/Swagger para documentação
- Docker Compose para banco de dados

> Nota: Por simplicidade acadêmica, o segredo JWT está em `appsettings.json`. Em projetos reais, use User Secrets/variáveis de ambiente.

---

## Pré‑requisitos

- .NET SDK 9 instalado: `dotnet --version`
- Docker e Docker Compose instalados
- EF Core CLI (opcional, mas recomendado): `dotnet tool install --global dotnet-ef`

---

## Como Executar (Passo a Passo)

1) Clonar e restaurar a solução

```bash
git clone <url-do-repositorio>
cd tds-3-tier
dotnet restore
```

2) Subir o PostgreSQL com Docker

```bash
docker compose up -d
```

O `docker-compose.yml` inicializa um PostgreSQL exposto em `5432` com usuário `postgres` e senha `102030`.

3) Configurar a ConnectionString e o SecretJWT

- Já existem valores didáticos em `MyFinance.Api/appsettings.json`:
  - ConnectionString: `Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=102030`
  - SecretJWT: `CarroDePalhacoComElefanteBrancoDentro`

Você pode sobrescrever via variáveis de ambiente (recomendado):

Linux/macOS

```bash
export SecretJWT="minha-chave-super-secreta"
export ConnectionStrings__MyFinanceContext="Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=102030"
```

Windows (PowerShell)

```powershell
$env:SecretJWT = "minha-chave-super-secreta"
$env:ConnectionStrings__MyFinanceContext = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=102030"
```

4) Aplicar as migrations do EF Core

As migrations estão no projeto `MyFinance.DataAccess` e a configuração (ConnectionString) vive no `MyFinance.Api`. Por isso, use o projeto de inicialização (`--startup-project`) ao atualizar o banco:

```bash
dotnet build
dotnet ef database update \
  --project MyFinance.DataAccess \
  --startup-project MyFinance.Api
```

5) Executar a API

```bash
dotnet run --project MyFinance.Api
```

Ao iniciar, a API imprime as URLs de escuta. Em desenvolvimento, acesse `http://localhost:<porta>/swagger`.

6) Autenticar e usar os endpoints

- Obter token JWT: `POST /api/token` com body:

```json
{
  "username": "Admin",
  "password": "Abc123"
}
```

- Em caso de sucesso, copie o token retornado e, no Swagger, clique em "Authorize" e informe: `Bearer <seu_token>`.
- Endpoints de contas (`/api/account`) exigem autenticação. O `DELETE` exige a role `Privileged`.

Credenciais didáticas (configuradas via middleware):

- Admin / Abc123 → Role: Privileged
- Fiap / Abc123 → Role: Public

---

## O que você encontra em cada camada

- API (`MyFinance.Api`)
  - `Program.cs`: DI, DbContext (PostgreSQL), autenticação JWT, Swagger com segurança.
  - Controllers: `AccountController`, `TokenController`.
  - DTOs com data annotations para validação.
  - Middleware didático que provisiona logins em memória a cada requisição.

- Business (`MyFinance.Business`)
  - Entidade `Account` (domínio).
  - Contratos `IAccountRepository`, `IAccountService`.
  - Serviço `AccountService` (regras de negócio e orquestração do repositório).

- DataAccess (`MyFinance.DataAccess`)
  - `MyFinanceContext` (DbContext) e `DbSet<Account>`.
  - `AccountRepository` com EF Core.
  - Migrations do EF Core.

- Tests (`MyFinance.Tests`)
  - Testes unitários de serviço com Moq.
  - Testes de integração de repositório com `Microsoft.EntityFrameworkCore.InMemory`.

---

## Como o projeto foi criado (passo a passo didático)

1) Criar a solution e os projetos

```bash
dotnet new sln -n MyFinance
dotnet new webapi -n MyFinance.Api
dotnet new classlib -n MyFinance.Business
dotnet new classlib -n MyFinance.DataAccess
dotnet new xunit -n MyFinance.Tests

dotnet sln add MyFinance.Api MyFinance.Business MyFinance.DataAccess MyFinance.Tests
```

2) Referências entre projetos

```bash
dotnet add MyFinance.Api reference MyFinance.Business MyFinance.DataAccess
dotnet add MyFinance.DataAccess reference MyFinance.Business
dotnet add MyFinance.Tests reference MyFinance.Business MyFinance.DataAccess
```

3) Pacotes NuGet principais

- API

```bash
dotnet add MyFinance.Api package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add MyFinance.Api package Microsoft.AspNetCore.OpenApi
dotnet add MyFinance.Api package Swashbuckle.AspNetCore
```

- DataAccess

```bash
dotnet add MyFinance.DataAccess package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add MyFinance.DataAccess package Microsoft.EntityFrameworkCore.Tools
```

- Tests

```bash
dotnet add MyFinance.Tests package xunit
dotnet add MyFinance.Tests package xunit.runner.visualstudio
dotnet add MyFinance.Tests package Moq
dotnet add MyFinance.Tests package Microsoft.EntityFrameworkCore.InMemory
```

4) Domínio e acesso a dados

- Criar a entidade `Account` em `MyFinance.Business`.
- Definir `IAccountRepository` e implementá-lo em `MyFinance.DataAccess` com EF Core.
- Criar `MyFinanceContext` e registrar `DbSet<Account>`.

5) Configuração da API

- Registrar `DbContext` com `UseNpgsql` no `Program.cs`.
- Registrar serviços: `IAccountService`, `ITokenService`, `IAccountRepository`.
- Adicionar Swagger e configurar definição de segurança Bearer.
- Configurar autenticação JWT (chave lida de configuração).

6) Controllers e DTOs

- `AccountController` com endpoints CRUD protegidos por `[Authorize]`.
- `TokenController` para emissão de JWT a partir de credenciais didáticas.
- DTOs para requests/responses com validação.

7) Migrations e banco

```bash
dotnet ef migrations add Initial \
  --project MyFinance.DataAccess \
  --startup-project MyFinance.Api
dotnet ef database update \
  --project MyFinance.DataAccess \
  --startup-project MyFinance.Api
```

8) Testes

- Testes de unidade do serviço com Moq.
- Testes de integração do repositório com EF InMemory.

---

## Dicas e Solução de Problemas

- `dotnet ef` não encontrado: instale o CLI com `dotnet tool install --global dotnet-ef` e reabra o terminal.
- Erro ao conectar no PostgreSQL: verifique `docker compose ps`, portas (`5432`) e `ConnectionStrings__MyFinanceContext`.
- Conflito de porta 5432: pare outros Postgres locais ou altere o mapeamento no `docker-compose.yml`.
- HTTPS no Linux/macOS: se necessário, rode `dotnet dev-certs https --trust`.
- JWT inválido/401: gere token em `/api/token` e informe `Bearer <token>` no botão "Authorize" do Swagger.

---

## Próximos Passos (para estudo)

- Mover segredos (JWT/ConnectionString) para User Secrets/variáveis de ambiente.
- Adicionar validação de tempo de vida do token e emissor/audiência.
- Incluir Health Checks (ex.: `/health`) e logs com `ILogger` em vez de `Console.WriteLine`.
- Habilitar CORS e Rate Limiting conforme cenário.
- Adicionar pipeline CI (GitHub Actions) com `dotnet build/test` e cobertura.

---

## Autor

Projeto acadêmico para aulas de ASP.NET Core + EF Core, com foco em boas práticas de camadas, autenticação JWT e persistência com PostgreSQL.

## Histórico de Refatorações

- Adicionada Solution na raiz para suportar múltiplos projetos (camadas).
- Adicionado `docker-compose` para provisionar banco PostgreSQL.
- Criados projetos `MyFinance.Business` e `MyFinance.DataAccess` e adicionadas referências.
- Movidos diretórios `Data` e `Migrations` para `MyFinance.DataAccess`.
 - Adicionando pacote Entity com Account que representa instância do banco de dados (conceito de DDD) para o MyFinance.Business
 - Adequação das dependencias aos projetos (removendo dependencias do projeto Presentation e adicionando ao projeto DataAccess
 - Adequação de namespace
 - Remoção do Attribute Key do objeto Account do projeto Presentation.Models (Models devem representar informações 
 - Refactory da controller AccountsController (removendo dependencia com banco, acessando com Service)


 ## Testes

### Teste unitário

1. Adicionar o pacote NuGet do xUnit, Moq
2. Adicionar referencia aos projetos que serão testados 
``` 
    dotnet add package xunit
    dotnet add package Moq
``` 

### Testes de Integração para Serviços e Bancos de Dados

1. Adicionar o pacote NuGet correspondente ao provedor de banco de dados em memória do Entity Framework Core.
``` 
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
``` 
2. o arquivo .cs, certifique-se de ter a seguinte diretiva de using:
``` 
    using Microsoft.EntityFrameworkCore;
``` 
3. Exemplo de uso correto
``` 
    var options = new DbContextOptionsBuilder<MyFinanceContext>()
                                .UseInMemoryDatabase("BancoDeTeste")
                                .Options;
``` 
