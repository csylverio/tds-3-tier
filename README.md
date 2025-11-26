# MyFinance – Sistema de Gerenciamento de Finanças

## Descrição

**MyFinance** é um sistema web desenvolvido em ASP.NET MVC com o objetivo de gerenciar finanças pessoais. A aplicação permite que os usuários registrem contas, adicionem transações financeiras (receitas e despesas) e visualizem relatórios em forma de extrato ou gráficos.

---

## MyFinance – Arquitetura em 3 Camadas (3-Tier Architecture)

Este repositório apresenta a evolução do projeto **MyFinance**, originalmente desenvolvido como uma aplicação ASP.NET MVC monolítica, agora refatorada para utilizar o padrão **Arquitetura em 3 Camadas (Presentation → Business → DataAccess)**.

O objetivo desta versão é demonstrar uma arquitetura mais organizada, escalável e alinhada a boas práticas profissionais, servindo como base para estudos de Arquitetura de Software e .NET.

---

## 📁 Estrutura da Solution
```js
MyFinance.sln
│
├── MyFinance → Camada de Apresentação (Web MVC)
├── MyFinance.Business → Camada de Negócio (Domínio, Serviços)
└── MyFinance.DataAccess → Camada de Acesso a Dados (EF Core)
```


---

## 🧱 Camadas da Arquitetura

### **1. MyFinance (Presentation Layer)**
- Implementação ASP.NET MVC.
- Controllers e Views.
- Consome serviços da camada Business.
- Não tem acesso direto ao banco.

---

### **2. MyFinance.Business (Business Layer)**
- Contém o **modelo de domínio**, como a entidade `Account`.
- Serviços responsáveis por regras de negócio.
- Ideal para testes unitários.
- Não conhece detalhes de banco de dados (facilita substituições e refatorações).

---

### **3. MyFinance.DataAccess (Data Layer)**
- Implementação de persistência usando **Entity Framework Core**.
- Contém:
   - `MyFinanceContext`
   - Configurações EF
   - Migrations
- Referenciada pela camada Business via DI.

---

## 🔧 Melhorias Implementadas Nesta Branch

### ✔ Separação de responsabilidades
O código foi reorganizado em 3 projetos distintos, evitando mistura entre UI, regras de negócio e persistência.

### ✔ Entidade de domínio movida para a camada Business
A classe `Account` agora reflete o modelo de domínio de forma consistente.

### ✔ Migrations e DbContext isolados
Elimina dependência direta da aplicação Web com o banco de dados.

### ✔ Controller mais limpa
`AccountsController` passa a utilizar serviços de negócio, reduzindo acoplamento.

### ✔ Docker Compose adicionado
Agora você pode subir o ambiente (para inicialização do banco de dados) com:

```bash
docker compose up
``` 

Rode a aplicação:

```bash
dotnet run --project MyFinance
``` 

## 📚 Objetivo Didático
Esta branch demonstra:
- Separação clara entre camadas.
- Como estruturar soluções profissionais em .NET.
- Base ideal para introduzir testes unitários (na próxima branch).

## 🏷 Branches Relacionadas

- `main` → versão monolítica inicial
- `feature/3-tiers` → (esta) arquitetura em 3 camadas
- `feature/unit-tests` → extensão com testes unitários

## 📄 Licença
Uso educacional e acadêmico.

---

# ✅ Testes Unitários

Esta branch estende o projeto da branch **feature/3-tiers**, adicionando uma estrutura completa de **testes unitários e de integração** utilizando:

- **xUnit**
- **Moq**
- **EF Core InMemory**

O objetivo é demonstrar boas práticas de testes no contexto de uma arquitetura em camadas.

---

## 🧩 Estrutura da Solution
```
MyFinance.sln
│
├── MyFinance → Camada de Apresentação
├── MyFinance.Business → Camada de Negócio
├── MyFinance.DataAccess → Persistência (EF Core)
└── MyFinance.Tests → Projeto de Testes Unitários e de Integração
```

---

## 🧪 Tecnologias de Teste Utilizadas

### ✔ **xUnit**  
Framework de testes moderno, leve e amplamente usado no ecossistema .NET.

### ✔ **Moq**  
Usado para criação de mocks e stubs para testes unitários isolados.

### ✔ **EF Core InMemory**  
Permite testes de integração de repositórios e serviços **sem necessidade de banco real**.

---

## 📦 Pacotes Instalados

### No projeto MyFinance.Tests:

```bash
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Moq
dotnet add package Microsoft.EntityFrameworkCore.InMemory
``` 

## 🛠 Configuração do DbContext InMemory
Exemplo utilizado nos testes:

```csharp
var options = new DbContextOptionsBuilder<MyFinanceContext>()
    .UseInMemoryDatabase("BancoDeTeste")
    .Options;

var context = new MyFinanceContext(options);
``` 

Isso permite testar serviços da camada Business como:
- Criação de contas
- Listagem
- Validação de regras
- Persistência básica via EF

Sem depender de bancos externos.

## 📈 Melhorias Implementadas Nesta Branch

✔ Novo projeto de testes isolado
MyFinance.Tests adiciona uma quarta camada na solution, dedicada exclusivamente a testes.

✔ Testes unitários com Moq
Permite isolar regras de negócio sem envolver EF Core.

✔ Testes de integração com EF InMemory
Valida comportamentos reais da camada DataAccess sem subir SQL Server ou PostgreSQL.

✔ Maior aderência ao padrão de responsabilidades
Os testes ajudam a reforçar a separação entre Presentation, Business e DataAccess.

✔ README atualizado documentando o processo
Inclui instruções explícitas de configuração e exemplos de uso.

▶ Como Executar os Testes
```csharp 
dotnet test
```
Todos os testes devem rodar contra o banco em memória.

## 📚 Objetivo Didático
Esta branch ensina:
- TDD / práticas de testes unitários
- Testes de integração com EF Core
- Criação de projeto de testes separado
- Como isolar regras de negócio via mock

É uma base excelente para estudo de Clean Architecture, DDD e arquiteturas multicamadas.







