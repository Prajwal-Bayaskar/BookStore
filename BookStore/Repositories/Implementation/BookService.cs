using BookStore.Models.Domain;
using BookStore.Models.DTO;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
    public class BookService :IBookService
    {
        private readonly BookStoreDbContext ctx;
        public BookService(BookStoreDbContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Books model)
        {
            try
            {
                
                ctx.Books.Add(model);
                ctx.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var bookGenre = new BookGenre
                    {
                        BookId = model.Id,
                        GenreId = genreId
                    };
                    ctx.BookGenre.Add(bookGenre);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var bookGenres= ctx.BookGenre.Where(a => a.BookId == data.Id);
                foreach(var book in bookGenres)
                {
                    ctx.BookGenre.Remove(book);
                }
                ctx.Books.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Books GetById(int id)
        {
            return ctx.Books.Find(id);
        }

        public BookListVm List(string term="",bool paging=false, int currentPage=0)
        {
            var data = new BookListVm();
           
            var list = ctx.Books.ToList();
           
           
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.BookName.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                //  paging applied
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var book in list)
            {
                var genres = (from genre in ctx.Genre
                              join mg in ctx.BookGenre
                              on genre.Id equals mg.GenreId
                              where mg.Id == book.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                book.GenreNames = genreNames;
            }
            data.Books = list.AsQueryable();
            return data;
        }

        public bool Update(Books model)
        {
            try
            {
                // these genreIds are not selected by users and still present is movieGenre table corresponding to
               
                var genresToDeleted = ctx.BookGenre.Where(a => a.Id == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach(var bGenre in genresToDeleted)
                {
                    ctx.BookGenre.Remove(bGenre);
                }
                foreach (int genId in model.Genres)
                {
                    var bookGenre = ctx.BookGenre.FirstOrDefault(a => a.Id == model.Id && a.GenreId == genId);
                    if (bookGenre == null)
                    {
                        bookGenre = new BookGenre { GenreId = genId, Id = model.Id };
                        ctx.BookGenre.Add(bookGenre);
                    }
                }

                ctx.Books.Update(model);
               
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetGenreByBookId(int bookId)
        {
            var genreIds = ctx.BookGenre.Where(a => a.Id == bookId).Select(a => a.GenreId).ToList();
            return genreIds;
        }

       
    }
}
