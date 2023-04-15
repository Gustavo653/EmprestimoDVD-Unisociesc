﻿using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;

namespace EmprestimoDVD.Persistence.Repository
{
    public class PessoaRepository : BaseRepository<Pessoa, EmprestimoDVDContext>, IPessoaRepository
    {
        public PessoaRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}
