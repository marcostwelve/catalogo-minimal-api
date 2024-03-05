using CatalogoAPI.Models;

namespace CatalogoAPI.Services
{
    public interface ITokenService
    {
        string GerarToken(string Key, string issuer, string audience, UserModel user);
    }
}
