using Microsoft.Extensions.DependencyInjection;
using Taso.Application.Common.CQRS;
using Taso.Domain.Common;

namespace Taso.Infrastructure.CQRS;

public class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;

    public Sender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Result> SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        
        return await handler.HandleAsync((dynamic)command, cancellationToken);
    }

    public async Task<Result<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));
        
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        
        return await handler.HandleAsync((dynamic)command, cancellationToken);
    }

    public async Task<Result<TResponse>> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
        
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        
        return await handler.HandleAsync((dynamic)query, cancellationToken);
    }
}
