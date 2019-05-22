using System.Threading.Tasks;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;

namespace TaskAndTimeTracking.Controller.interfaces
{
    public interface IAuthenticationController
    {
        Task<TokenDTO> GenerateToken(string username);

        Task<bool> CredentialsValid(CredentialsDTO credentialsDto);
    }
}