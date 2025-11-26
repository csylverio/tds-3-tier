# MyFinance – Sistema de Gerenciamento de Finanças

## Descrição

**MyFinance** é um sistema web desenvolvido em ASP.NET MVC com o objetivo de gerenciar finanças pessoais. A aplicação permite que os usuários registrem contas, adicionem transações financeiras (receitas e despesas) e visualizem relatórios em forma de extrato ou gráficos.

---
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
