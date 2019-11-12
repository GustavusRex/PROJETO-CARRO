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
        public void AlteracaoClima() // METODO QUE ALTERA O CLIMA DENTRO DA VIAGEM EXECUTADA 
        {
            int mudanca = new Random().Next(1, 4); 
            if (mudanca == 1)
                Clima = "SOL";
            else if (mudanca == 2)
                Clima = "CHUVA";
            else
                Clima = "NEVE";
        }

        public override string ToString()
        {
            return $"ID da Viagem {ID}, Com o Clima {ClimaAtual} e com a Distância {Distancia}";
        }
    }
}