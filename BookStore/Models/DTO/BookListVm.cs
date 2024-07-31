using BookStore.Models.Domain;

namespace BookStore.Models.DTO
{
    public class BookListVm
    {
        public IQueryable<Books> Books { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
