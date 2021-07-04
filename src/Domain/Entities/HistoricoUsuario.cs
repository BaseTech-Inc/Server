using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoricoUsuario
    {
        public string Id { get; set; }

        #region Usuario
        public Usuario Usuario { get; set; }
        #endregion

        #region LocalizacaoChegada
        public Localizacao LocalizacaoChegada { get; set; }
        #endregion

        #region LocalizacaoPartida
        public Localizacao LocalizacaoPartida { get; set; }
        #endregion

        public double DistPerc { get; set; }

        public DateTime Duracao { get; set; }

        public string Rota { get; set; }
    }
}
