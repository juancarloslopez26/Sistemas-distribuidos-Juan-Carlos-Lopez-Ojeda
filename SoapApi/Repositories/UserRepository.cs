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

    public async Task DeleteByIdAsync(UserModel user, CancellationToken cancellationToken)
    {
        var userEntity = user.ToEntity();
        _dbContext.Users.Remove(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserModel> CreateAsync(UserModel user, CancellationToken cancellationToken)
    {
        user.Id = Guid.NewGuid();
        await _dbContext.AddAsync(user.ToEntity(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public Task<UserModel> UpdateAsync(UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
