namespace BookRecommendationSystem.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PublishedYear { get; set; }
        public string CoverImageUrl { get; set; } = null!;

        public AuthorDto Author { get; set; } = null!;
        public GenreDto Genre { get; set; } = null!;
    }
}
