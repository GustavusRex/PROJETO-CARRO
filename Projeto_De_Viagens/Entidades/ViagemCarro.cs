using Projeto_De_Viagens.Operações;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_De_Viagens.Entidades
{
    public class ViagemCarro : Base
    {
        public Veiculo Veiculo;
        public Viagem Viagem;

        public ViagemCarro(Viagem viagem, Veiculo veiculo)
        {
            Veiculo = veiculo;
            Viagem = viagem;
        }

        public void DirigirNaViagemAutomatico(AgenciaDeViagens agencia)
        {

        }
    }
}
