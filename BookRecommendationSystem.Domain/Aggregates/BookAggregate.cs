using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BookRecommendationSystem.Domain.Aggregates
{
    public class BookAggregate
    {
        private readonly Book _book;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public BookAggregate(
            Book book,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository,
            IBookRepository bookRepository,
            ICustomerRepository customerRepository)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task AddBookAsync(
            string title,
            string description,
            int publishedYear,
            string coverImageUrl,
            string authorName,
            string biography,
            List<string> genreNames)
        {
            ValidateBookParameters(title, publishedYear);

            // Работа с автором
            var author = await GetOrCreateAuthorAsync(authorName, biography);

            // Работа с жанрами
            var genres = await GetOrCreateGenresAsync(genreNames);

            // Установка свойств
            _book.Title = title;
            _book.Description = description;
            _book.PublishedYear = publishedYear;
            _book.CoverImageUrl = coverImageUrl;
            _book.AuthorId = author.Id;
            _book.CreatedAt = DateTime.UtcNow;

            // Добавляем связи с жанрами
            foreach (var genre in genres)
            {
                _book.Genres.Add(new BookGenre { GenreId = genre.Id });
            }

            await _bookRepository.AddAsync(_book);
        }

        public void AddRating(int userId, int score, string review)
        {
            if (score < 1 || score > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));

            var rating = new Rating
            {
                Score = score,
                Review = review,
                BookId = _book.Id,
                CustomerId = GetCustomerId(userId).Result, // Метод для получения CustomerId по UserId
                CreatedAt = DateTime.UtcNow
            };

            _book.Ratings.Add(rating);
        }

        public void AddRecommendation(
            int userId,
            string reason,
            string algorithmType = "content",
            decimal score = 0.9m)
        {
            var recommendation = new Recommendation
            {
                BookId = _book.Id,
                CustomerId = GetCustomerId(userId).Result,
                Reason = reason,
                AlgorithmType = algorithmType,
                Score = score,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };

            _book.Recommendations.Add(recommendation);
        }

        public async Task UpdateBookInfoAsync(
            string title,
            string description,
            int publishedYear,
            string coverImageUrl,
            string? authorName = null,
            string? biography = null,
            List<string>? genreNames = null)
        {
            ValidateBookParameters(title, publishedYear);

            _book.Title = title;
            _book.Description = description;
            _book.PublishedYear = publishedYear;
            _book.CoverImageUrl = coverImageUrl;

            // Обновление автора (если указано)
            if (!string.IsNullOrWhiteSpace(authorName))
            {
                var author = await GetOrCreateAuthorAsync(authorName, biography ?? "");
                _book.AuthorId = author.Id;
            }

            // Обновление жанров (если указано)
            if (genreNames != null && genreNames.Any())
            {
                _book.Genres.Clear();
                var genres = await GetOrCreateGenresAsync(genreNames);

                foreach (var genre in genres)
                {
                    _book.Genres.Add(new BookGenre { GenreId = genre.Id });
                }
            }

            await _bookRepository.UpdateAsync(_book);
        }

        private async Task<Author> GetOrCreateAuthorAsync(string name, string biography)
        {
            var author = await _authorRepository.GetByNameAsync(name);
            if (author == null)
            {
                author = new Author
                {
                    Name = name,
                    Biography = biography,
                    CreatedAt = DateTime.UtcNow
                };
                await _authorRepository.AddAsync(author);
            }
            return author;
        }

        private async Task<List<Genre>> GetOrCreateGenresAsync(List<string> genreNames)
        {
            var genres = new List<Genre>();
            foreach (var name in genreNames.Distinct())
            {
                var genre = await _genreRepository.GetByNameAsync(name);
                if (genre == null)
                {
                    genre = new Genre { Name = name };
                    await _genreRepository.AddAsync(genre);
                }
                genres.Add(genre);
            }
            return genres;
        }

        private void ValidateBookParameters(string title, int publishedYear)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (publishedYear < 1800 || publishedYear > DateTime.UtcNow.Year + 5)
                throw new ArgumentException($"Invalid publication year: {publishedYear}");
        }

        private async Task<int> GetCustomerId(int userId)
        {
            var customer = await _customerRepository.GetByIdAsync(userId)
                ?? throw new InvalidOperationException("Customer profile not found");
            
            return customer.Id;
        }
    }
}