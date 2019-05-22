using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Controller
{
    public abstract class BaseController<TResponse, TRequest, TEntity> : IBaseController<TResponse, TRequest> where TRequest : BaseDTO
    {
        protected IBaseRepository<TEntity> Repo;
        protected IMapper ControllerMapper;


        public BaseController(IBaseRepository<TEntity> repository)
        {
            Repo = repository;
            ControllerMapper = Mapping.getInstance();
        }

        public virtual async Task<ResponseDTO<List<TResponse>>> getAll()
        {
            var entities = await Repo.getAll();
            var dtos = ControllerMapper.Map<List<TEntity>, List<TResponse>>(entities);
            return new ResponseDTO<List<TResponse>>(dtos);
        }

        public virtual async Task<ResponseDTO<TResponse>> getById(int id)
        {
            if (id < 1)
            {
                return new ResponseDTO<TResponse>("Invalid ID");
            }
            TEntity byId = await Repo.getById(id);
            return new ResponseDTO<TResponse>(
                ControllerMapper.Map<TEntity, TResponse>(byId)
            );
        }

        public virtual async Task<ResponseDTO<TResponse>> add(TRequest dto)
        {
            var entity = ControllerMapper.Map<TRequest, TEntity>(dto);
            var createdEntity = await Repo.add(entity);
            return new ResponseDTO<TResponse>(ControllerMapper.Map<TEntity, TResponse>(createdEntity));
        }

        public virtual async Task<ResponseDTO<TResponse>> update(TRequest dto)
        {
            if (dto.Id < 0)
            {
                new ResponseDTO<TRequest>("No ID was given for update");
            }

            var oldEntity = await Repo.getById(dto.Id);
            await SafeMap(oldEntity, dto);

            await Repo.update(oldEntity);
            return new ResponseDTO<TResponse>();
        }

        public virtual async Task<ResponseDTO<TResponse>> delete(TRequest dto)
        {
            throw new System.NotImplementedException();
        }

        public abstract Task SafeMap(TEntity toEntity, TRequest dto);
    }
}