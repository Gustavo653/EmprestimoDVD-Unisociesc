using Common.DTO;
using Common.Infrastructure;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.Infrastructure.Service
{
    public interface IEmprestimoService
    {
        Task<ResponseDTO<Emprestimo>> EmprestaDVD(EmprestimoAmigoDTO emprestimoAmigoDTO);
        Task<ResponseDTO<Emprestimo>> DevolveDVD(EmprestimoDVDDTO emprestimoDVDDTO);
        Task<ResponseDTO<List<Emprestimo>>> HistoricoEmprestimo(EmprestimoDVDDTO emprestimoDVDDTO);
        Task<ResponseDTO<List<DVDEmprestadoDTO>>> BuscaDVDEmprestimo();
    }
}
