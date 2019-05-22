using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAndTimeTracking.Controller.DTO.response;

namespace TaskAndTimeTracking.Controller.interfaces
{
    public interface IBaseController<TResponse, TRequest>
    {
        Task<ResponseDTO<List<TResponse>>> getAll();

        Task<ResponseDTO<TResponse>> getById(int id);

        Task<ResponseDTO<TResponse>> add(TRequest dto);

        Task<ResponseDTO<TResponse>> update(TRequest dto);

        Task<ResponseDTO<TResponse>> delete(TRequest dto);
    }
}