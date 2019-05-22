using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Controller
{
    public class ProjectController : BaseController<ProjectResponseDTO, ProjectRequestDTO, ProjectEntity>, IProjectController
    {
        private IUserRepository UserRepository;
        private IProjectRepository ProjectRepository;
        private ITodoRepository TodoRepository;

        public ProjectController(
            IProjectRepository repository, 
            IUserRepository userRepository,
            ITodoRepository todoRepository) 
            : base(repository)
        {
            UserRepository = userRepository;
            ProjectRepository = repository;
            TodoRepository = todoRepository;
        }

        public override async Task<ResponseDTO<ProjectResponseDTO>> add(ProjectRequestDTO dto)
        {
            var entity = ControllerMapper.Map<ProjectRequestDTO, ProjectEntity>(dto);
            entity.Owner = await UserRepository.getById(dto.Owner);
            var createdEntity = await Repo.add(entity);
            return new ResponseDTO<ProjectResponseDTO>(ControllerMapper.Map<ProjectEntity, ProjectResponseDTO>(createdEntity));
        }

        public async Task<List<TodoDTO>> getAssignedTodos(int id)
        {
            var entity = await ProjectRepository.getDetailsById(id);
            if (entity?.Todos == null)
            {
                return new List<TodoDTO>();
            }
            return ControllerMapper.Map<List<TodoEntity>, List<TodoDTO>>(entity.Todos.ToList());
        }

        public async Task<TodoDTO> createAndAssignTodo(int projectId, TodoDTO todo)
        {
            var entity = await ProjectRepository.getDetailsById(projectId);
            if (entity == null)
            {
                return null;
            }

            var createdTodo = await TodoRepository.add(ControllerMapper.Map<TodoDTO, TodoEntity>(todo));
            if (createdTodo == null)
            {
                return null;
            }
            entity.Todos.Add(createdTodo);
            await Repo.update(entity);

            return ControllerMapper.Map<TodoEntity, TodoDTO>(createdTodo);
        }

        
        public async Task<bool> unassignTodo(int projectId, int todoId)
        {
            var entity = await ProjectRepository.getDetailsById(projectId);
            if (entity == null)
            {
                return false;
            }

            var entityToRemove = await TodoRepository.getById(todoId);
            if (entityToRemove == null)
            {
                return false;
            }
            entity.Todos.Remove(entityToRemove);
            await Repo.update(entity);
            return true;
        }

        public async Task<bool> reassignTodo(int projectId, int todoId, int newProjectId)
        {
            return false;
        }

        
        public async Task<List<UserResponseDTO>> getAssignedUsers(int projectId)
        {
            var entity = await ProjectRepository.getDetailsById(projectId);
            if (entity == null)
            {
                return new List<UserResponseDTO>();
            }
            
            if (entity.ProjectUserAssignments == null || entity.ProjectUserAssignments.Count < 1)
            {
                return new List<UserResponseDTO>();
            }

            var userAssignments = entity.ProjectUserAssignments.ToList();
            List<UserEntity> assignedUsers = new List<UserEntity>();
            foreach (var assignment in userAssignments)
            {
                assignedUsers.Add(await UserRepository.getById(assignment.UserId));
            }
            
            return ControllerMapper.Map<List<UserEntity>, List<UserResponseDTO>>(assignedUsers);
        }

        public async Task<bool> assignUser(int projectId, int userId)
        {
            var entity = await ProjectRepository.getDetailsById(projectId);
            if (entity == null)
            {
                return false;
            }

            var userEntity = await UserRepository.getById(userId);
            if (userEntity == null)
            {
                return false;
            }
            entity.ProjectUserAssignments.Add(new ProjectUserAssignment
            {
                ProjectId = projectId,
                UserId = userEntity.Id
            });
            await Repo.update(entity);
            return true;
        }

        public async Task<bool> unassignUser(int projectId, int userId)
        {
            var entity = await ProjectRepository.getDetailsById(projectId);
            if (entity == null)
            {
                return false;
            }

            var assignmentToDelete = entity.ProjectUserAssignments.ToList().FirstOrDefault(assignment =>
            {
                return assignment.ProjectId == projectId && assignment.UserId == userId;
            });
            if (assignmentToDelete == null)
            {
                return false;
            }

            entity.ProjectUserAssignments.Remove(assignmentToDelete);
            await Repo.update(entity);

            return true;
        }
        
        public override async Task SafeMap(ProjectEntity toEntity, ProjectRequestDTO dto)
        {
            toEntity.Description = dto.Description;
            toEntity.Name = dto.Name;
            toEntity.ProgressEstimate = dto.ProgressEstimate;
            toEntity.Owner = await UserRepository.getById(dto.Owner);
        }
    }
}