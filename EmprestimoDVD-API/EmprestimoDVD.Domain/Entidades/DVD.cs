﻿namespace EmprestimoDVD.Domain.Entidades
{
    public class DVD : BaseEntity
    {
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public FaixaEtaria Classificacao { get; set; }
        public Pessoa ArtistaPrincipal { get; set; }
        public Pessoa Diretor { get; set; }
        public Genero Genero { get; set; }
    }
}
