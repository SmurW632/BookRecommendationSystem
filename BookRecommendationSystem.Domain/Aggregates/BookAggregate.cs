using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Domain.Aggregates
{
    public class BookAggregate
    {
        private readonly Book _book;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookAggregate(Book book, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
        }

        // Метод для добавления книги
        public async Task AddBookAsync(string title, string description, int publishedYear, string coverImageUrl, string authorName, string biography, string genreName)
        {
            // Ищем автора по имени
            var author = await _authorRepository.GetByNameAsync(authorName);
            if (author == null)
            {
                // Если автор не найден, создаем нового
                author = new Author { Name = authorName, Biography = biography};
                await _authorRepository.AddAsync(author);
            }

            // Ищем жанр по названию
            var genre = await _genreRepository.GetByNameAsync(genreName);
            if (genre == null)
            {
                // Если жанр не найден, создаем новый
                genre = new Genre { Name = genreName };
                await _genreRepository.AddAsync(genre);
            }

            // Устанавливаем свойства книги
            _book.Title = title;
            _book.Description = description;
            _book.PublishedYear = publishedYear;
            _book.CoverImageUrl = coverImageUrl;
            _book.AuthorId = author.Id;
            _book.GenreId = genre.Id;
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
