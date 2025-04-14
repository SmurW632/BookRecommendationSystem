namespace BookRecommendationSystem.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Навигационное свойство
        public ICollection<BookGenre> BookGenres { get; set; } = [];
    }
}
