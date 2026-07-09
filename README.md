# Taso

> **Organize. Conecte. Conclua.**

Taso é uma aplicação moderna de gerenciamento de tarefas desenvolvida com **ASP.NET Core** e **Angular**, projetada para demonstrar uma arquitetura escalável, boas práticas de desenvolvimento e uma experiência de usuário limpa e intuitiva.

O principal diferencial do Taso é o suporte a **tarefas dependentes**, permitindo criar fluxos de execução em cascata, onde uma tarefa pode depender de outra para representar processos, projetos e sequências de trabalho.

---

# ✨ Principais funcionalidades

- ✅ Cadastro de tarefas
- ✏️ Edição de tarefas
- 🗑️ Exclusão de tarefas
- 🔍 Pesquisa
- 🎯 Filtros
- 📌 Prioridades
- 📅 Data limite
- 🔗 Dependência entre tarefas
- 🌳 Hierarquia de tarefas
- ⚡ Exclusão em cascata das tarefas dependentes

---

# 🏗️ Arquitetura

O projeto foi desenvolvido seguindo princípios de arquitetura moderna e boas práticas de engenharia de software.

## Backend

- ASP.NET Core Web API
- Clean Architecture
- Vertical Slice Architecture
- CQRS
- Repository Pattern
- Result Pattern
- Fluent Validation
- Mapster
- Entity Framework Core
- SQLite
- Scrutor
- Dispatcher próprio (compatível com futura substituição por MediatR)

Estrutura:

```
src/

├── Taso.Api
├── Taso.Application
├── Taso.Domain
├── Taso.Infra.Data
└── Taso.Infra.Ioc
```

---

## Frontend

- Angular
- Standalone Components
- Signals
- Control Flow
- Tailwind CSS
- Feature First Architecture
- Lazy Loading
- OnPush Change Detection

Estrutura:

```
src/app/

core/
layout/
shared/
features/
```

---

# 📂 Estrutura do repositório

```
taso/

docs/
backend/
frontend/

README.md
```

---

# 📖 Documentação

Toda a arquitetura do projeto é documentada antes da implementação.

```
docs/

product/
backend/
frontend/
design-system/
decisions/
```

A documentação contém:

- Visão do produto
- Arquitetura
- Convenções
- Design System
- ADRs (Architecture Decision Records)
- Contextos para agentes de IA

---

# 🎯 Objetivos do projeto

O Taso possui dois objetivos principais:

- Servir como um gerenciador de tarefas moderno e intuitivo.
- Demonstrar uma implementação profissional utilizando arquitetura limpa, padrões de projeto e boas práticas de desenvolvimento.

Este projeto também foi concebido para explorar o uso de Inteligência Artificial como apoio ao desenvolvimento, mantendo todas as decisões arquiteturais sob controle do desenvolvedor.

---

# 🚀 Stack

## Backend

- C#
- ASP.NET Core
- Entity Framework Core
- SQLite

## Frontend

- Angular
- TypeScript
- Tailwind CSS

---

# 📌 Princípios adotados

- Clean Architecture
- SOLID
- Separation of Concerns
- Dependency Injection
- Vertical Slice
- CQRS
- Feature First
- Design System
- Componentização
- Código limpo
- Escalabilidade
- Manutenibilidade

---

# 📅 Roadmap

- [ ] MVP
- [ ] CRUD de tarefas
- [ ] Dependências entre tarefas
- [ ] Pesquisa
- [ ] Filtros
- [ ] Dashboard
- [ ] Tema escuro
- [ ] Testes automatizados
- [ ] Docker
- [ ] CI/CD

---

# 🤝 Contribuindo

Contribuições são bem-vindas.

Antes de iniciar qualquer implementação, consulte toda a documentação presente na pasta `docs`, que define a arquitetura, as convenções e os padrões adotados pelo projeto.

---

# 📄 Licença

Este projeto está licenciado sob a licença MIT.

---

## Desenvolvido por

**Nelson Cândido Miguel Dos Santos**
