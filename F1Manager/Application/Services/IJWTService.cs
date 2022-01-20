using System.Threading.Tasks;

namespace Application.Services
{
    public interface IJWTService
    {
         string GenerateToken(Domain.Users.User user);
    }
}