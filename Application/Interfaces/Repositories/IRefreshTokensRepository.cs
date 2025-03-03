using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces.Repositories;

public interface IRefreshTokensRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
    Task<List<RefreshToken>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    void Delete(RefreshToken refreshToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}