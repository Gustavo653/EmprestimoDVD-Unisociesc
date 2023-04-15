using AutoMapper;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;

namespace EmprestimoDVD.Infrastructure
{
    public class EmprestimoDVDProfile : Profile
    {
        public EmprestimoDVDProfile()
        {
            CreateMap<Genero, GeneroDTO>(MemberList.None).ReverseMap();
        }
    }
}