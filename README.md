# MyFinance – Sistema de Gerenciamento de Finanças

## Descrição

**MyFinance** é um sistema web desenvolvido em ASP.NET MVC com o objetivo de gerenciar finanças pessoais. A aplicação permite que os usuários registrem contas, adicionem transações financeiras (receitas e despesas) e visualizem relatórios em forma de extrato ou gráficos.

---

## Funcionalidades

- **Cadastro de Usuário:** Permite a criação de contas para uso do sistema.
- **Menu Principal:** Acesso centralizado às funcionalidades.
- **Cadastro de Contas:** Inclui descrição e data de abertura.
- **Cadastro de Transações:** Registro de receitas e despesas associadas a contas.
- **Relatório de Extrato:** Visualização das transações por conta e período.
- **Relatório Gráfico:** Demonstração de receitas e despesas por período e por conta.

---

## Requisitos Não-Funcionais

- **IDE:** Microsoft Visual Studio 2022
- **Framework:** ASP.NET MVC (.NET 8)
- **Linguagem:** C#
- **Banco de Dados:** PostgreSQL, MySQL ou SQL Server

---

## Estrutura do Projeto e Passos de Desenvolvimento

### ✅ Organização Inicial

- Criar a pasta `ViewModels` e mover o `ErrorViewModel` para ela.
- Compilar (CTRL + SHIFT + B) para verificar erros.

---

### ✅ Modelagem Inicial - `Account`

1. Criar classe `Models/Account`.
2. Criar `AccountsController` (MVC Controller Empty).
3. Instanciar `List<Account>` e passar como parâmetro para a View.
4. Criar pasta `Views/Accounts`.
5. Criar View `Index` com template "List" e model `Account`.
6. Alterar o título para "Accounts".

---

### 🔄 Refatoração

- Excluir `AccountsController` e a pasta `Views/Accounts`.

---

### ✅ Scaffold de CRUD

1. Criar Scaffold: Controller + Views com Entity Framework.
2. Selecionar model `Account`, contexto de dados e opções de visualização.
3. Nomear como `AccountsController`.

---

### ✅ Adaptação e Primeira Migration

1. Ajustar a string de conexão.
2. Verificar injeção de dependência no `Program.cs`.
3. Instalar provider do banco.
4. No **Package Manager Console**:
   ```bash
   Add-Migration Initial
   Update-Database
   ```

---

### ✅ Outras Entidades e Segunda Migration

1. Criar demais modelos de domínio.
   - Atributos
   - Relacionamentos (`ICollection`)
   - Construtores e métodos específicos
2. Adicionar `DbSet` no `DbContext`.
3. Migration:
   ```bash
   Add-Migration OtherEntities
   Update-Database
   ```

---

### ✅ Seeding Service

1. Criar `SeedingService` na pasta `Data`.
2. Registrar e chamar o serviço no `Program.cs`.

---

### ✅ Transactions

#### Criação do Controller e View

1. Criar links no navbar para Account e Transaction.
2. Criar pasta `Views/Transactions` e View `Index`.
3. Alterar título da View.

#### Criar `TransactionService` e método `FindAll`

1. Criar pasta `Services` e classe `TransactionService`.
2. Registrar no `Program.cs`.
3. Implementar `FindAll()` retornando `List<Transaction>`.
4. Usar no `TransactionController`.

#### Criar Formulário Simples

1. Criar link "Create" em `Views/Transactions/Index`.
2. Implementar `Create` (GET) na controller.
3. Criar View `Create`.
4. Implementar `Insert()` no `TransactionService`.
5. Implementar `Create` (POST) na controller.

---

### ✅ Chave Estrangeira Obrigatória

1. Adicionar `AccountId` em `Transaction`.
2. Dropar banco, recriar migration:
   ```bash
   Drop-Database
   Add-Migration TransactionFK
   Update-Database
   ```
3. Atualizar `Insert()` no `TransactionService`.

---

### ✅ TransactionFormViewModel e Select de Contas

1. Criar `AccountService` com `FindAll()`.
2. Registrar no `Program.cs`.
3. Criar `TransactionFormViewModel`.
4. Atualizar Controller:
   - Injetar `AccountService`
   - Atualizar `Create` GET
5. Atualizar View `Create`:
   - Usar `TransactionFormViewModel`
   - Adicionar campo `select` para `AccountId`

---

### ✅ Detalhes da Transação e Eager Loading

1. Adicionar link para `Details` em `Views/Transactions/Index`.
2. Criar Action `Details` no Controller.
3. Criar View `Details`.
4. Incluir `Include(obj => obj.Account)` no `FindAll()`.

---

## Tecnologias Utilizadas

- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core
- PostgreSQL / MySQL / SQL Server
- Bootstrap (para layout)

---

## Instruções para Execução

1. Clone o repositório.
2. Configure a string de conexão em `appsettings.json`.
3. Execute os comandos de migration:
   ```bash
   Add-Migration Init
   Update-Database
   ```
4. Execute o projeto via Visual Studio (F5).

---

## Autor

Desenvolvido como material didático para aulas de ASP.NET MVC com Entity Framework Core.