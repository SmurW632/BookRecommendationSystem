using BookRecommendationSystem.Domain.Modules.Genres;

namespace BookRecommendationSystem.Domain.Modules.Books
{
    public class BookGenre
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
