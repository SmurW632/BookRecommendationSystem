namespace BookRecommendationSystem.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PublishedYear { get; set; }
        public string CoverImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Внешние ключи
        public int AuthorId { get; set; }

        // Навигационные свойства
        public Author? Author { get; set; }
        public ICollection<UserLibrary> UserLibraries { get; set; } = [];
        public ICollection<BookGenre> Genres { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];
    }
}
