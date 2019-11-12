using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_De_Viagens.Entidades
{
    public class DesgasteDoPneu
    {
        double KmDoEvento;
        int EstadoPneuAposEvento;

        public DesgasteDoPneu(double kmdoEvento, int estadoPneuAposEvento)
        {
            KmDoEvento = kmdoEvento;
            EstadoPneuAposEvento = estadoPneuAposEvento;
        }

        public override string ToString()
        {
            return $"Desgastou o Pneu no KM{KmDoEvento}, e ficou com o estado do Pneu {EstadoPneuAposEvento}";
        }
    }
}
