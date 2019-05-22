using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;

namespace TaskAndTimeTracking.Controller.interfaces
{
    public interface ITodoController : IBaseController<TodoDTO, TodoDTO>
    {
        Task<List<UserResponseDTO>> getAssignedUsers(int todoId);

        Task<string> assignUser(int todoId, int userId);

        Task<string> unassignUser(int todoId, int userId);

        Task<List<WorkProgressResponseDTO>> getWorkProgress(int todoId);

        Task<WorkProgressResponseDTO> addWorkProgress(int todoId, WorkProgressRequestDTO dto);
    }
}