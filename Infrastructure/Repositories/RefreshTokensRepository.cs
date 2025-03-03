using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Repositories;

public class RefreshTokensRepository(AppDbContext context) : IRefreshTokensRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }

    public void Delete(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}