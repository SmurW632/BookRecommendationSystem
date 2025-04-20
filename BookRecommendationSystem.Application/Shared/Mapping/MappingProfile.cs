using AutoMapper;
using BookRecommendationSystem.Application.Books;
using BookRecommendationSystem.Application.Moduls.Authors;
using BookRecommendationSystem.Application.Moduls.Customers;
using BookRecommendationSystem.Application.Moduls.Genres;
using BookRecommendationSystem.Application.Moduls.Libraries;
using BookRecommendationSystem.Application.Moduls.Raitigs;
using BookRecommendationSystem.Application.Moduls.Recommendations;
using BookRecommendationSystem.Domain.Modules.Authors;
using BookRecommendationSystem.Domain.Modules.Books;
using BookRecommendationSystem.Domain.Modules.Customers;
using BookRecommendationSystem.Domain.Modules.Genres;
using BookRecommendationSystem.Domain.Modules.Libraries;
using BookRecommendationSystem.Domain.Modules.Ratings;
using BookRecommendationSystem.Domain.Modules.Recommendations;

namespace BookRecommendationSystem.Application.Shared.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Recommendation, RecommendationDto>().ReverseMap();
            CreateMap<UserLibrary, UserLibraryDto>().ReverseMap();
        }
    }
}
