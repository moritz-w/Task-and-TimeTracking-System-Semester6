using System;
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
    public class TodoController : BaseController<TodoDTO, TodoDTO, TodoEntity>, ITodoController
    {
        private ITodoRepository TodoRepository;
        private IWorkProgressRepository WorkProgressRepository;
        private IUserRepository UserRepository;
        private IProjectRepository ProjectRepository;

        public TodoController(
            ITodoRepository repository, 
            IWorkProgressRepository workProgressRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository) 
            : base(repository)
        {
            TodoRepository = repository;
            WorkProgressRepository = workProgressRepository;
            UserRepository = userRepository;
            ProjectRepository = projectRepository;
        }
        
        
        public override Task SafeMap(TodoEntity toEntity, TodoDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserResponseDTO>> getAssignedUsers(int todoId)
        {
            var entity = await TodoRepository.getDetailsById(todoId);
            if (entity?.TodoUserAssignments == null)
            {
                return new List<UserResponseDTO>();
            }

            List<UserEntity> assignedUsers = new List<UserEntity>();
            foreach (var assignment in entity.TodoUserAssignments.ToList())
            {
                assignedUsers.Add(await UserRepository.getById(assignment.UserId));
            }

            return ControllerMapper.Map<List<UserEntity>, List<UserResponseDTO>>(assignedUsers);
        }

        public async Task<string> assignUser(int todoId, int userId)
        {
            var entity = await TodoRepository.getDetailsById(todoId);
            if (entity == null)
            {
                return "TODO does not exist";
            }
            
            var inParentProject = entity.Project?.ProjectUserAssignments?.Any(assignment => assignment.UserId == userId);
            if (inParentProject == false)
            {
                return "User is not assigned to the parent project of this TODO";
            }

            entity.TodoUserAssignments.Add(new TodoUserAssignment
            {
                TodoId = entity.Id,
                UserId = userId
            });
            await Repo.update(entity);
            
            return string.Empty;
        }

        public async Task<string> unassignUser(int todoId, int userId)
        {
            var entity = await TodoRepository.getDetailsById(todoId);
            if (entity == null)
            {
                return "TODO does not exist";
            }

            var assignmentToDelete = entity.TodoUserAssignments.ToList().FirstOrDefault(assignment =>
            {
                return assignment.TodoId == todoId && assignment.UserId == userId;
            });
            if (assignmentToDelete == null)
            {
                return "User is not assigned to this TODO";
            }

            entity.TodoUserAssignments.Remove(assignmentToDelete);
            await Repo.update(entity);

            return String.Empty;
        }

        public async Task<List<WorkProgressResponseDTO>> getWorkProgress(int todoId)
        {
            var entity = await TodoRepository.getDetailsById(todoId);
            if (entity == null)
            {
                return new List<WorkProgressResponseDTO>(todoId);
            }
            
            List<WorkProgressEntity> workProgressEntities = new List<WorkProgressEntity>();
            foreach (var we in entity.WorkProgressEntities)
            {
                workProgressEntities.Add(await WorkProgressRepository.getById(we.Id));    
            }

            return ControllerMapper.Map<List<WorkProgressEntity>, List<WorkProgressResponseDTO>>(workProgressEntities);
        }
        
        public async Task<WorkProgressResponseDTO> addWorkProgress(int todoId, WorkProgressRequestDTO dto)
        {
            var entity = await TodoRepository.getDetailsById(todoId);
            if (entity == null)
            {
                return null;
            }
            
            var wprgEntity = ControllerMapper.Map<WorkProgressRequestDTO, WorkProgressEntity>(dto);
            wprgEntity.Person = await UserRepository.getById(dto.Person);

            var createdEntity = await WorkProgressRepository.add(wprgEntity);
            if (createdEntity == null)
            {
                return null;
            }
            
            entity.WorkProgressEntities.Add(createdEntity);
            await Repo.update(entity);

            return ControllerMapper.Map<WorkProgressEntity, WorkProgressResponseDTO>(createdEntity);
        }
    }
}