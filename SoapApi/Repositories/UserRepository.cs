using Microsoft.EntityFrameworkCore;
using SoapApi.Dtos;
using SoapApi.Infrastructure;
using SoapApi.Mappers;

namespace SoapApi.Repositories;

public class UserRepository : IUserRepository{
    private readonly RelationalDbContext _dbContext;
    public UserRepository(RelationalDbContext dbContext){
        _dbContext = dbContext;
    }

    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken){
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return user?.ToModel(); 
    }

    public async Task<IList<UserModel>> GetAllAsync(CancellationToken cancellationToken){
        var users = await _dbContext.Users.AsNoTracking().ToListAsync(cancellationToken);
        return users.Select(user => user.ToModel()).ToList(); 
    }

    public async Task<IList<UserModel>> GetAllByEmailAsync(string email, CancellationToken cancellationToken){
        var users = await _dbContext.Users
            .AsNoTracking()
            .Where(user => EF.Functions.Like(user.Email, $"%{email}%"))
            .ToListAsync(cancellationToken);
        
        return users.Select(user => user.ToModel()).ToList(); 
    }
}
