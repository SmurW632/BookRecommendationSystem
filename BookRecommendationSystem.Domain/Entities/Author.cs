namespace BookRecommendationSystem.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Biography { get; set; } = null!;

        // Навигационные свойства
        public ICollection<Book> Books { get; set; } = [];
    }
}
