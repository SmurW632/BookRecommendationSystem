namespace BookRecommendationSystem.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PublishedYear { get; set; }
        public string CoverImageUrl { get; set; } = null!;

        // Внешние ключи
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        // Навигационные свойства
        public Author Author { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Recommendation> Recommendations { get; set; } = [];
    }
}
