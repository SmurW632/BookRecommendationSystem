namespace BookRecommendationSystem.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Навигационные свойства
        public ICollection<Book> Books { get; set; } = [];
    }
}
