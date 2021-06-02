using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoricoUsuario
    {
        public int IdHistoricoUsuario { get; set; }

        #region Usuario
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
        #endregion

        #region LocalizacaoChegada
        public int IdLocalizacaoChegada { get; set; }

        public Localizacao LocalizacaoChegada { get; set; }
        #endregion

        #region LocalizacaoPartida
        public int IdLocalizacaoPartida { get; set; }

        public Localizacao LocalizacaoPartida { get; set; }
        #endregion

        public double DistPerc { get; set; }

        public DateTime Duracao { get; set; }

        public string Rota { get; set; }
    }
}
