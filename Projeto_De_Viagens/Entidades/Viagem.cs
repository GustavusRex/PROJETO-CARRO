using System;
using System.Collections.Generic;
using System.Text;
using Projeto_De_Viagens.Operações;

namespace Projeto_De_Viagens.Entidades
{
   public class Viagem : Base
    {
        public string ClimaAtual;
        public string Clima;
        public double Distancia;
        public void AlteracaoClima()
        {
            int mudanca = new Random().Next(1, 3);
            if (mudanca == 1)
                Clima = "SOL";
            else if (mudanca == 2)
                Clima = "CHUVA";
            else
                Clima = "NEVE";        
        }
    }
}
