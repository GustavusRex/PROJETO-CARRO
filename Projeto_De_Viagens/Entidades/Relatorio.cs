using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_De_Viagens.Entidades
{
   public class Relatorio : Base
    {
        public ViagemCarro _viagemCarro;

        public int ParadasAbastecimento;

        public int ParadasCalibragem;

        public double LitrosConsumidos;

        public List<string> DesgastePneu = new List<string>();

        public List<string> MudancaClimatica = new List<string>();


        public Relatorio(ViagemCarro viagemCarro)
        {
            _viagemCarro = viagemCarro;
        }
    }
}
