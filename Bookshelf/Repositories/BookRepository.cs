using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace Bookshelf.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<(bool Success, string Message)> DeleteBookAsync(int id)
        {
            if (id <= 0) return ErrorMessage();

            var bookToDelete = await appDbContext.BooksTable.FirstOrDefaultAsync(_ => _.Id == id);
            if (bookToDelete is null) return ErrorMessage();

            appDbContext.BooksTable.Remove(bookToDelete!); await appDbContext.SaveChangesAsync();
            return SuccessMessage();
        }

        public async Task<List<Book>> GetBookAsync() => await appDbContext.BooksTable.FromSql($"SELECT * FROM BooksTable").ToListAsync();
        public async Task<List<Book>> GetPromoBookAsync() => await appDbContext.BooksTable.FromSql($"SELECT * FROM BooksTable WHERE Price < (SELECT AVG(Price) FROM BooksTable)").ToListAsync();

        public async Task<(bool Success, string Message)> ManageBookAsync(Book book)
        {
            if (book is null) return ErrorMessage();

            if (book.Id == 0)
            {
                if (!await IsBookAlreadyAdded(book.Title!))
                {
                    appDbContext.BooksTable.Add(book); await appDbContext.SaveChangesAsync();
                    return SuccessMessage();
                }
            }

            var bookToUpdate = await appDbContext.BooksTable.FirstOrDefaultAsync(_ => _.Id == book.Id);
            if (bookToUpdate is null) return ErrorMessage();

            bookToUpdate.Title = book.Title;
            bookToUpdate.Category = book.Category;
            bookToUpdate.Author = book.Author;
            bookToUpdate.YearRelease = book.YearRelease;
            bookToUpdate.Publisher = book.Publisher;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Pages = book.Pages;
            bookToUpdate.Price = book.Price;
            bookToUpdate.IsBestSeller = book.IsBestSeller;
            bookToUpdate.Image = book.Image;
            await appDbContext.SaveChangesAsync();
            return SuccessMessage();
        }

        private static (bool, string) SuccessMessage() => (true, "Process successfully completed.");
        private static (bool, string) ErrorMessage() => (false, "Error occured whiles processing.");

        private async Task<bool> IsBookAlreadyAdded(string bookName)
        {
            var book = await appDbContext.BooksTable.Where(_ => _.Title!.ToLower().Equals(bookName)).FirstOrDefaultAsync();
            return book is not null;
        }
    }
}
