namespace BookRecommendationSystem.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PublishedYear { get; set; }
        public string? CoverImageUrl { get; set; }

        public AuthorDto? Author { get; set; }
        public ICollection<GenreDto> Genres { get; set; } = [];
    }
}
