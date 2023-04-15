﻿using System;

namespace EmprestimoDVD.Domain.Entidades
{
    public class Amigo : Pessoa
    {
        public long NumTelefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
