using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
