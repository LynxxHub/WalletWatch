using WalletWatchAPI.Models;

namespace WalletWatchAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user, bool rememberMe);
    }
}
