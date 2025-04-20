using BookRecommendationSystem.Domain.Exceptions;

namespace BookRecommendationSystem.Application.Shared.Helpers
{
    public static class Guard
    {
        public static void AgainstNull<T>(T? value, string message) where T : class
        {
            if (value == null)
            {
                throw new NotFoundException(message);
            }
        }

        public static void AgainstInvalidId(int id, string message)
        {
            if (id == 0)
            {
                throw new ValidationException(message);
            }
        }
    }
}
