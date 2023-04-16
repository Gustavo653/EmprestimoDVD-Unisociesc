using Common.DTO;

namespace Common.Infrastructure
{
    public interface IServiceBase<T, TDTO>
    {
        Task<ResponseDTO<IEnumerable<T>>> GetAll();
        Task<ResponseDTO<T>> GetById(int? id);
        Task<ResponseDTO<T>> CreateOrUpdate(int? id, TDTO dto);
        Task<ResponseDTO<T>> Delete(int id);
    }
}
