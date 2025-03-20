namespace BookRecommendationSystem.Domain.Exceptions
{
    public class EntityNotFoundException(string message) : Exception(message)
    {
    }
}
