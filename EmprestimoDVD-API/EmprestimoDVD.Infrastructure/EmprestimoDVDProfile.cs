using AutoMapper;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;

namespace EmprestimoDVD.Infrastructure
{
    public class EmprestimoDVDProfile : Profile
    {
        public EmprestimoDVDProfile()
        {
            CreateMap<Amigo, AmigoDTO>(MemberList.None).ReverseMap();
            CreateMap<DVD, DVDDTO>(MemberList.None).ReverseMap();
            CreateMap<Genero, GeneroDTO>(MemberList.None).ReverseMap();
            CreateMap<Pessoa, PessoaDTO>(MemberList.None).ReverseMap();
        }
    }
}