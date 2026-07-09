# Convenções Backend

Utilizar nullable reference types.

Não utilizar regiões (#region).

Não utilizar classes estáticas para regras de negócio.

Preferir composição à herança.

Toda regra deverá possuir testes futuramente.

Utilizar records para Commands e Queries.

Handlers deverão possuir apenas uma responsabilidade.

Toda operação deverá retornar Result<T>.

Não lançar Exceptions para regras de negócio.

Utilizar FluentValidation.

Utilizar async/await em toda operação de I/O.

Nunca acessar DbContext diretamente fora dos Repositories.

Nunca utilizar AutoMapper.

Utilizar Mapster.

Toda entidade deverá herdar de BaseEntity.

Toda entidade deverá possuir CreatedAt.

Toda entidade deverá possuir UpdatedAt.

Toda entidade deverá possuir Id Guid.

Utilizar CancellationToken em todos os handlers.
