using Ardalis.Specification;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Identity
{
    public interface IIdentityUserInterface : IReadRepositoryBase<IdentityUser>
    {
    }
}
