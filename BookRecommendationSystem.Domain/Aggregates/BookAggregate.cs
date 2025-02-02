using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Domain.Aggregates
{
    public class BookAggregate
    {
        private readonly Book _book;

        private BookAggregate() { }

        public BookAggregate(Book book)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
        }

        /// <summary>
        /// Добавить рейтинг к книге.
        /// </summary>
        /// <param name="score">Оценка (от 1 до 5).</param>
        /// <param name="comment">Комментарий к рейтингу.</param>
        /// <exception cref="ArgumentException">Если оценка выходит за допустимые пределы.</exception>
        public void AddRating(int score, string review)
        {
            if (score < 1 || score > 5)
            {
                throw new ArgumentException("Оценка должна быть в диапазоне от 1 до 5");
            }

            var rating = new Rating()
            {
                Score = score,
                Review = review,
                BookId = _book.Id
            };

            _book.Ratings.Add(rating);
        }

        /// <summary>
        /// Удалить рейтинг по ID.
        /// </summary>
        /// <param name="ratingId">ID рейтинга.</param>
        /// <exception cref="InvalidOperationException">Если рейтинг не найден.</exception>
        public void RemoveRating(int ratingId)
        {
            var rating = _book.Ratings.FirstOrDefault(r => r.Id == ratingId)
                ?? throw new InvalidOperationException("Рейтинг не найден.");

            _book.Ratings.Remove(rating);
        }

        /// <summary>
        /// Добавить рекомендацию для книги.
        /// </summary>
        /// <param name="userId">ID пользователя, который оставляет рекомендацию.</param>
        /// <param name="reason">Причина рекомендации.</param>
        public void AddRecommendation(int userId, string reason)
        {
            var recommendation = new Recommendation
            {
                UserId = userId,
                Reason = reason,
                BookId = _book.Id
            };

            _book.Recommendations.Add(recommendation);
        }

        /// <summary>
        /// Удалить рекомендацию по ID.
        /// </summary>
        /// <param name="recommendationId">ID рекомендации.</param>
        /// <exception cref="InvalidOperationException">Если рекомендация не найдена.</exception>
        public void RemoveRecommendation(int recommendationId)
        {
            var recommendation = _book.Recommendations.FirstOrDefault(r => r.Id == recommendationId)
                ?? throw new InvalidOperationException("Рекомендация не найдена.");

            _book.Recommendations.Remove(recommendation);
        }

        /// <summary>
        /// Обновить информацию о книге.
        /// </summary>
        /// <param name="title">Новое название.</param>
        /// <param name="description">Новое описание.</param>
        /// <param name="publishedYear">Новый год публикации.</param>
        /// <param name="coverImageUrl">Новая ссылка на обложку.</param>
        public void UpdateBookInfo(string title, string description, int publishedYear, string coverImageUrl)
        {
            _book.Title = title;
            _book.Description = description;
            _book.PublishedYear = publishedYear;
            _book.CoverImageUrl = coverImageUrl;
        }
    }
}
