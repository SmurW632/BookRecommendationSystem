using Microsoft.AspNetCore.Identity;

namespace BookRecommendationSystem.Domain.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public Customer? Customer { get; set; }
    }
}
