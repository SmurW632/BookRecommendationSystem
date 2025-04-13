using AutoMapper;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Domain.Entities;

namespace BookRecommendationSystem.Application.Mapping
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
