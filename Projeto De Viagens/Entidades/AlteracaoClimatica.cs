using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_De_Viagens.Entidades
{
    public class AlteracaoClimatica
    {
        public double KmDoEvento;
        public string ClimaAlterado;

        public AlteracaoClimatica(double kmDoEvento, string climaAlterado)
        {
            KmDoEvento = kmDoEvento;
            ClimaAlterado = climaAlterado;
        }
        public override string ToString()
        {
            return $"Desgastou o Clima mudou no KM{KmDoEvento}, e ficou com o tipo de clima {ClimaAlterado}";
        }
    }
}
