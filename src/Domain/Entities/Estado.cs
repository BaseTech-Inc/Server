﻿namespace Domain.Entities
{
    public class Estado
    {
        public string Id { get; set; }

        public Pais Pais { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }
    }
}
