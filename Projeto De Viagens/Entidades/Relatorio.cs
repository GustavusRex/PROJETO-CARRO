using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_De_Viagens.Entidades
{ // RELATORIO DA VIAGEM PERCORRIDA
    public class Relatorio
    {
        public ViagemCarro _viagemCarro; // UTILIZA O OBJETO VIAGEMCARRO COMO IDENTIFICADOR 

        public int ParadasAbastecimento;

        public int ParadasCalibragem;

        public double LitrosConsumidos;

        public double KmPercorrido;

        public List<DesgasteDoPneu> DesgastePneu = new List<DesgasteDoPneu>(); // LISTAS DE ONDE O PNEU DESGASTA NA KILOMETRAGEM, QUAL ESTADO DO PNEU

        public List<AlteracaoClimatica> MudancaClimatica = new List<AlteracaoClimatica>(); // LISTA DE ONDE O CLIMA ALTERA NA KILOMETRAGEM, E QUAL CLIMA


        public Relatorio(ViagemCarro viagemCarro)
        {
            _viagemCarro = viagemCarro;
        }

        public void DesgasteDoPneuEvento(double km, int estadoPneu) 
        {
            DesgastePneu.Add (new DesgasteDoPneu(km, estadoPneu));                  
        }

        public void MudancaClimaticaDoEvento(double km, string clima)
        {
            MudancaClimatica.Add(new AlteracaoClimatica(km, clima));
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{_viagemCarro._Veiculo}\n\n\n");
            sb.AppendLine($"ID da viagem executada {_viagemCarro.ID}");
            sb.AppendLine($"Teve {ParadasAbastecimento} Paradas para Abastecer, Teve {ParadasCalibragem} Paradas para Calibrar");
            sb.AppendLine($"Teve {LitrosConsumidos} Litros de combustivel consumidos, Teve {KmPercorrido} KM percorridos");
            sb.AppendLine("===========Lista de Desgaste do Pneu===========\n");
            DesgastePneu.ForEach(x => {
                sb.AppendLine($"{x}\n");
            });
            sb.AppendLine("===========Lista de Alteração do Clima===========\n");
            MudancaClimatica.ForEach(x => {
                sb.AppendLine($"{x}\n");
            });
            if (KmPercorrido == 0)
                sb.AppendLine("Viagem Não percorrida\n\n");
            else if (_viagemCarro._Viagem.Distancia > 0)
                sb.AppendLine("Viagem não completada\n\n");
            else if (_viagemCarro._Viagem.Distancia <= 0)
                sb.AppendLine("Viagem Completa\n\n");

            return sb.ToString();
        }

    }
}