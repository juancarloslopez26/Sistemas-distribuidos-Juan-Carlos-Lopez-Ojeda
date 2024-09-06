using Microsoft.EntityFrameworkCore;
using SoapApi.Dtos;
using SoapApi.Infrastructure;
using SoapApi.Mappers;

namespace SoapApi.Repositories;

public class BookRepository : IBookRepository{
    private readonly RelationalDbContext _dbContext;
    public BookRepository(RelationalDbContext dbContext){
        _dbContext = dbContext;
    }

    async Task<BookModel> IBookRepository.GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return book?.ToModel(); 
    }

    
}