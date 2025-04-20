using BookRecommendationSystem.Domain.Modules.Customers;
using Microsoft.AspNetCore.Identity;

namespace BookRecommendationSystem.Domain.Entities
{
    public class IdentityUserEntity : IdentityUser<int>
    {
        public Customer? Customer { get; set; }
    }
}
