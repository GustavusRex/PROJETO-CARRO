using Projeto_De_Viagens.Operações;
using System;

namespace Projeto_De_Viagens.Entidades
{
   public class Veiculo : Base
    {
        public string Marca;
        public string Modelo;
        public string Placa;
        public uint Ano;
        public int EstadoPneuAtual;
        public int EstadoPneu;
        public double VelocidadeMaxima;
        public double CapacidadeTanque;
        public bool Flex;
        public string TipoCombustivel;
        public double KmPorAlcoolAtual;
        public double KmPorAlcool;
        public double KmPorGasolinaAtual;
        public double KmPorGasolina;
        public double LitrosGasolina;
        public double LitrosAlcool;

        public void AbastecerVeiculoPadrão()
        {
            if (TipoCombustivel == "gasolina")
            {
                LitrosGasolina += Calculo.CalcularAbastecimento(CapacidadeTanque, LitrosGasolina);
                if (EstadoPneu != 3)
                    Calibrar();
            }
            else if (TipoCombustivel == "alcool")
            {
                LitrosAlcool += Calculo.CalcularAbastecimento(CapacidadeTanque, LitrosAlcool);
                if (EstadoPneu != 3)
                    Calibrar();

            }
            Console.WriteLine("\n aperte Qualquer coisa para continuar...");
            Console.ReadKey();
        }
        public void AbastecerVeiculoFlex()
        {
            string opcao;

            if (LitrosGasolina + LitrosAlcool == CapacidadeTanque)
            {
                Console.WriteLine("\nTanque está cheio" +
                       " Aperte qualquer coisa para continuar..."); return;
            }

            else if (LitrosAlcool + LitrosGasolina < CapacidadeTanque)
            {
                Console.WriteLine("\nDeseja encher o tanque?");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    Console.WriteLine("\nQuer encher o tanque de alcool ou gasolina?");
                    opcao = Validacao.ValidarTipoCombustivel(Console.ReadLine().ToLower()); // Opção para escolher caso queira abastecer ou encher o tanque 
                    if (opcao == "alcool")
                        LitrosAlcool += Calculo.EncherTanqueFlex(CapacidadeTanque, LitrosAlcool, LitrosGasolina);

                    else if (opcao == "gasolina")
                        LitrosGasolina += Calculo.EncherTanqueFlex(CapacidadeTanque, LitrosGasolina, LitrosAlcool);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Carro do tipo Flex");
                    Console.WriteLine("Deseja abastecer alcool ou gasolina?");
                    opcao = Validacao.ValidarTipoCombustivel(Console.ReadLine().ToLower()); // Opção para escolher caso queira abastecer ou encher o tanque 
                    if (opcao == "gasolina")
                    {
                        LitrosGasolina += Calculo.CalcularAbastecimentoFlex(CapacidadeTanque, LitrosGasolina, LitrosAlcool);
                    }
                    else if (opcao == "alcool")
                    {
                        LitrosAlcool += Calculo.CalcularAbastecimentoFlex(CapacidadeTanque, LitrosGasolina, LitrosAlcool);
                    }
                }
            }
            Console.WriteLine("\n aperte Qualquer coisa para continuar...");
            Console.ReadKey();
        }
        public void Calibrar()
        {
            Console.WriteLine("Deseja qual opção para calibrar o pneu?");
            int opcao = Validacao.Validar3opcoesINT(Console.ReadLine());

            if (opcao == 1)
                EstadoPneu = 1;
            else if (opcao == 2)
                EstadoPneu = 2;
            else
                EstadoPneu = 3;
        }
        public void AlteracaoPneu()
        {
            EstadoPneu -= new Random().Next(0, 1); 
        }
    }
}
