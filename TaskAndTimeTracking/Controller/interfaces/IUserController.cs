using System.Threading.Tasks;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.response;

namespace TaskAndTimeTracking.Controller.interfaces
{
    public interface IUserController : IBaseController<UserResponseDTO, UserRequestDTO>
    {
        Task<ResponseDTO<UserResponseDTO>> getByEMail(string email);
    }
}