
# Projeto MonolitoBackendEvent - Check-in de Eventos com Autenticação JWT

Este projeto consiste em uma API REST desenvolvida com ASP.NET Core 8.0, que permite o gerenciamento de eventos e participantes, com autenticação e autorização baseada em JWT (JSON Web Token). A aplicação foi construída com uma arquitetura em camadas e utiliza PostgreSQL como banco de dados.

---

## 🎯 Funcionalidades

### 📆 Eventos

* Criar, listar, atualizar e remover eventos
* Consultar participantes por evento

### 👥 Participantes

* Criar, listar, atualizar e remover participantes
* Associar participante a evento

### 🔐 Autenticação JWT

* Registro de usuário com senha hasheada (BCrypt)
* Login com geração de token JWT
* Proteção de rotas com `[Authorize]`
* Controle por perfil (ex: Admin)

---

## 🧱 Tecnologias Utilizadas

* .NET 8.0
* ASP.NET Core Web API
* Entity Framework Core (EF Core)
* PostgreSQL (via Docker)
* JWT Bearer Authentication
* BCrypt.Net para hashing de senha
* Swagger/OpenAPI para documentação

---

## 🚀 Como Rodar o Projeto

### Pré-requisitos

* Docker (PostgreSQL)
* .NET SDK 8.0
* CLI ou Visual Studio

### 1. Inicie o container PostgreSQL

```bash
docker start monolitobackendevent-db-1
```

### 2. Crie o banco de dados e aplique as migrations

```bash
dotnet ef database update --project MonolitoBackend.Infra --startup-project MonolitoBackend.Api
```

### 3. Execute a API

```bash
dotnet run --project MonolitoBackend.Api
```

### 4. Acesse o Swagger

```
http://localhost:5000
```

---

## 🔐 Testando Autenticação JWT

### 1. Registro de usuário

`POST /api/users`

```json
{
  "userName": "admin",
  "email": "admin@example.com",
  "password": "SenhaForte123"
}
```

### 2. Login para obter token

`POST /api/auth/login`

```json
{
  "userName": "admin",
  "password": "SenhaForte123"
}
```

### 3. Use o token no Swagger (botão Authorize)

```
Bearer SEU_TOKEN_AQUI
```

---

## 🔒 Exemplo de rotas protegidas

### Rota com autenticação

```csharp
[Authorize]
[HttpGet("protegido")]
public IActionResult GetProtegido() => Ok("Autenticado!");
```

### Rota com perfil "Admin"

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("admin-only")]
public IActionResult DeleteRestrito() => Ok("Acesso Admin");
```

---

## ⚙️ Configurações (appsettings.json)

```json
"Jwt": {
  "Secret": "chave-secreta-supersegura-com-32-caracteres",
  "Issuer": "MonolitoBackend",
  "Audience": "MonolitoFrontend",
  "ExpiryMinutes": 60
}
```

---

## 📁 Estrutura do Projeto

```
MonolitoBackend.Api
├── Controllers
├── Program.cs

MonolitoBackend.Core
├── Entidade
├── Interfaces
├── DTOs

MonolitoBackend.Infra
├── Services
├── Data (ApplicationDbContext)
├── Configurations
│   └── DependencyInjectionExtensions.cs
├── Middlewares
│   └── ExceptionHandlingMiddleware.cs
```

---

## ✅ Testes Recomendados

* Registro e login de usuários
* Acesso a rotas públicas e protegidas
* Testes com e sem token JWT
* Testes com perfil correto/incorreto (ex: Admin vs User)

---

## 🛠️ Refatoração – Atividade Prática 03

Esta entrega inclui melhorias de arquitetura aplicadas sobre a base da AP 02, seguindo as boas práticas de separação de responsabilidades e tratamento centralizado de erros.

### ✅ 1. Extension Method para Serviços e Configurações

- Criado o método `AddApplicationServices` no arquivo:
  ```
  MonolitoBackend.Infra/Configurations/DependencyInjectionExtensions.cs
  ```
- Essa extensão encapsula:
  - Registro de serviços e repositórios (`UserService`, `TokenService`, etc.)
  - Configuração do banco de dados (EF Core + PostgreSQL)
  - Configuração da autenticação JWT
- Substituiu as chamadas manuais no `Program.cs` por:
  ```csharp
  builder.Services.AddApplicationServices(builder.Configuration);
  ```

### ✅ 2. Middleware para Tratamento Centralizado de Exceções

- Criado o middleware:
  ```
  MonolitoBackend.Infra/Middlewares/ExceptionHandlingMiddleware.cs
  ```
- Adicionado ao pipeline da aplicação:
  ```csharp
  app.UseMiddleware<ExceptionHandlingMiddleware>();
  ```
- Responsável por:
  - Capturar exceções não tratadas
  - Retornar JSON padronizado com status, mensagem e timestamp
  - Tratar especificamente erros como `UnauthorizedAccessException` (401) e `ValidationException` (400)

### 🔍 Exemplo de resposta de erro tratada pelo middleware:

```json
{
  "status": 500,
  "error": "Erro interno no servidor",
  "timestamp": "2025-05-12T12:34:56Z"
}
```

---

Com isso, a API agora segue princípios de **modularidade, baixo acoplamento e controle centralizado de erros**, facilitando sua manutenção e evolução.

---

## 👨‍💻 Autor

Matheus — Projeto desenvolvido para as Atividades Práticas 02 e 03 da disciplina de TDS
