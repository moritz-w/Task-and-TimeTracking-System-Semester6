using AutoMapper;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Persistence.Entity;

namespace TaskAndTimeTracking.Controller
{
    public class Mapping
    {
        private static IMapper ConfiguredMapper { get; set; }

        private Mapping()
        {

        }

        public static IMapper getInstance()
        {
            if (ConfiguredMapper == null)
            {
                ConfiguredMapper = init();
            }
            return ConfiguredMapper;
        }

        private static IMapper init()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserRequestDTO, UserEntity>();
                config.CreateMap<UserEntity, UserResponseDTO>();
                
                config.CreateMap<ProjectRequestDTO, ProjectEntity>()
                    .ForMember(entity => entity.Owner, opt => opt.Ignore());
                config.CreateMap<ProjectEntity, ProjectResponseDTO>();
                
                config.CreateMap<TodoEntity, TodoDTO>();
                config.CreateMap<TodoDTO, TodoEntity>();

                config.CreateMap<WorkProgressEntity, WorkProgressResponseDTO>();
                config.CreateMap<WorkProgressRequestDTO, WorkProgressEntity>()
                    .ForMember(entity => entity.Person, opt => opt.Ignore());
            });
            return mapperConfig.CreateMapper();
        }
    }
}