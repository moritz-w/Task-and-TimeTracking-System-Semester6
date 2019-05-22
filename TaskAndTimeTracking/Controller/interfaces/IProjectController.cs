using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;

namespace TaskAndTimeTracking.Controller.interfaces
{
    public interface IProjectController : IBaseController<ProjectResponseDTO, ProjectRequestDTO>
    {
        Task<List<TodoDTO>> getAssignedTodos(int id);

        Task<TodoDTO> createAndAssignTodo(int projectId, TodoDTO todo);

        Task<bool> unassignTodo(int projectId, int todoId);

        Task<bool> reassignTodo(int projectId, int todoId, int newProjectId);
        
        Task<List<UserResponseDTO>> getAssignedUsers(int id);

        Task<bool> assignUser(int projectId, int userId);
        
        Task<bool> unassignUser(int projectId, int userId);
    }
}