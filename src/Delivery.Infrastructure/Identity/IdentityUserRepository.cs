using Ardalis.Specification.EntityFrameworkCore;
using Delivery.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Infrastructure.Identity
{
    public class IdentityUserRepository : RepositoryBase<IdentityUser>, IIdentityUserInterface
    {
        public IdentityUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
