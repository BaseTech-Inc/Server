using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Imagem
    {
        public string Id { get; set; }

        public string TituloImagem { get; set; }

        public byte[] DataImagem { get; set; }
    }
}
