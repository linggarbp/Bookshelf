using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IBookRepository
    {
        Task<(bool Success, string Message)> ManageBookAsync(Book book);
        Task<(bool Success, string Message)> DeleteBookAsync(int id);
        Task<List<Book>> GetBookAsync();
        Task<List<Book>> GetPromoBookAsync();
    }
}
