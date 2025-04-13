namespace BookRecommendationSystem.Application.DTOs
{
    public class UserLibraryDto
    {
        public BookDto? Book { get; set; }
        public bool IsFavorite { get; set; }
        public string? ReadingStatus { get; set; }
    }
}
