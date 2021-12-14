using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
