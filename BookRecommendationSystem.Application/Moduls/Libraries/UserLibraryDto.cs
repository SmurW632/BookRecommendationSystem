using BookRecommendationSystem.Application.Books;

namespace BookRecommendationSystem.Application.Moduls.Libraries
{
    public class UserLibraryDto
    {
        public BookDto? Book { get; set; }
        public bool IsFavorite { get; set; }
        public string? ReadingStatus { get; set; }
    }
}
