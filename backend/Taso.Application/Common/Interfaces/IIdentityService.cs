using Taso.Domain.Common;

namespace Taso.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> RegisterUserAsync(string email, string password, string fullName);
    Task<Result<string>> LoginAsync(string email, string password);
}
