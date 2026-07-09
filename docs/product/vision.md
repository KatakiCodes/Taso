# Taso

## Visão Geral

Taso é uma aplicação para gerenciamento de tarefas pessoais construída com foco em simplicidade, organização e escalabilidade arquitetural.

Embora seja um projeto de estudo, sua arquitetura deve refletir boas práticas utilizadas em aplicações reais de mercado.

O objetivo principal é permitir o gerenciamento de tarefas independentes e tarefas dependentes, possibilitando a criação de cadeias de execução (Task Dependencies).

---

## Objetivos

- Criar tarefas.
- Atualizar tarefas.
- Excluir tarefas.
- Listar tarefas.
- Filtrar tarefas.
- Criar relações de dependência entre tarefas.

---

## Conceito de Dependência

Uma tarefa pode depender de outra.

Exemplo:

Instalar Visual Studio

↓

Criar Projeto

↓

Criar Banco de Dados

↓

Implementar API

↓

Criar Frontend

Neste cenário:

Criar Projeto depende de Instalar Visual Studio.

Criar Banco depende de Criar Projeto.

Implementar API depende de Criar Banco.

---

## Regra de Negócio

Uma tarefa poderá possuir apenas uma tarefa pai.

Uma tarefa poderá possuir diversas tarefas filhas.

Ao excluir uma tarefa, todas as tarefas descendentes deverão ser removidas em cascata.

---

## MVP

Cadastro de tarefas

CRUD

Dependências

Pesquisa

Ordenação

Filtro

Persistência SQLite

API REST

SPA Angular

---

## Fora do MVP

Autenticação

Usuários

Compartilhamento

Notificações

Upload de arquivos

Comentários
