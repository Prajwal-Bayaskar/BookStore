using BookStore.Models.Domain;
using BookStore.Models.DTO;

namespace BookStore.Repositories.Abstract
{
    public interface IBookService
    {
        bool Add(Books model);
        bool Delete(int id);
        Books GetById(int id);
        List<int> GetGenreByBookId(int bookId);
        BookListVm List(string term = "", bool paging = false, int currentPage = 0);
        bool Update(Books model);
       
    }
}