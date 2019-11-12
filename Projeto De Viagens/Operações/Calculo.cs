using Projeto_De_Viagens.Entidades;
using System;

namespace Projeto_De_Viagens.Operações
{
    static class Calculo
    {
        public static double CalcularAbastecimento(double capacidadeTanque, double tanque)
        {
            string opcao;
            double litros;

            if (tanque == capacidadeTanque)
            {//  Testa se o tanque está cheio
                Console.WriteLine("\nTanque está cheio" +
                    " Aperte qualquer coisa para continuar...");
            }

            else if (tanque < capacidadeTanque)
            {
                Console.WriteLine("Deseja encher o tanque?");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper()); // Opção para escolher caso queira abastecer ou encher o tanque 
                if (opcao == "S")
                {
                    tanque += (capacidadeTanque - tanque);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("O Tanque está cheio"); Console.ResetColor();
                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"\nQuantidade no tanque {tanque.ToString("F2")} litros");
                    Console.WriteLine("Quanto deseja abastecer ?");
                    Console.WriteLine($"LEMBRE-SE A CAPACIDADE TOTAL DO TANQUE É {capacidadeTanque} LITROS ");
                    litros = Validacao.ValidarLitros(Console.ReadLine());

                    while (litros > capacidadeTanque || litros + tanque > capacidadeTanque)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nUltrapassou o limite digite novamente"); Console.ResetColor();
                        litros = Validacao.ValidarLitros(Console.ReadLine());
                    }

                    tanque += litros;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\nO Carro foi Abastecido! Há {tanque.ToString("F2")} Litros"); Console.ResetColor();

                }

            }
            return tanque;
        } // CALCULA OS ABASTECIMENTOS DE CARROS ONDE SÓ PODE GASOLINA OU ALCOOL
        public static double CalcularAbastecimentoFlex(double capacidadeTanque, double tanqueG, double tanqueA)
        {
            double litros;
            double tanqueflex = 0;

            Console.Clear();
            Console.WriteLine($"Quantidade no tanque {(tanqueG + tanqueA).ToString("F2")} litros");
            Console.WriteLine("Quanto deseja abastecer?");
            Console.WriteLine($"LEMBRE-SE A CAPACIDADE TOTAL DO TANQUE É {capacidadeTanque} LITROS ");
            litros = Validacao.ValidarLitros(Console.ReadLine()); // Opção para escolher caso queira abastecer ou encher o tanque 

            while (litros > capacidadeTanque || litros + tanqueG + tanqueA > capacidadeTanque)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ultrapassou o limite digite novamente");
                Console.ResetColor();
                litros = Validacao.ValidarLitros(Console.ReadLine());
            }

            tanqueflex += litros;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n O Carro foi Abastecido Há {(tanqueflex + tanqueG + tanqueA).ToString("F2")} Litros");
            Console.ResetColor();

            return tanqueflex;
        } // CALCULA O ABASTECIMENTO DE UM CARRO PODE AMBOS OS COMBUSTIVEIS
        public static double EncherTanqueFlex(double capacidadeTanque, double tanqueG, double tanqueA)
        {
            double tanqueCheio;

            tanqueG += (capacidadeTanque - tanqueA - tanqueG);
            tanqueCheio = tanqueG;
            Console.WriteLine("O Tanque está cheio");
            Console.ResetColor();
            return tanqueCheio;
        } // ENCHE O TANQUE DE UM CARRO FLEX
        public static double CalcularAutonomiaPadrao(string clima, Veiculo veiculo, double alcool = 0, double gasolina = 0)
        {
            double autonomia = 0;
            autonomia = alcool + gasolina;
            if (clima == "SOL" && veiculo.EstadoPneu == 2)
                autonomia -= autonomia * 0.0725;

            else if (clima == "SOL" && veiculo.EstadoPneu == 1)
                autonomia -= autonomia * 0.0915;

            else if (clima == "CHUVA" && veiculo.TipoCombustivel == "gasolina" && veiculo.EstadoPneu == 3)
                autonomia -= autonomia * 0.12;

            else if (clima == "CHUVA" && veiculo.TipoCombustivel == "gasolina" && veiculo.EstadoPneu == 2)
                autonomia -= autonomia * 0.1925;

            else if (clima == "CHUVA" && veiculo.TipoCombustivel == "gasolina" && veiculo.EstadoPneu == 1)
                autonomia -= autonomia * 0.2115;

            else if (clima == "CHUVA" && veiculo.TipoCombustivel == "alcool" && veiculo.EstadoPneu == 3)
            {
                alcool -= alcool * 0.12;
                autonomia -= alcool * 0.30;
            }
            else if (clima == "CHUVA" && veiculo.TipoCombustivel == "alcool" && veiculo.EstadoPneu == 2)
            {
                alcool -= alcool * 0.12;
                autonomia -= alcool * (0.30 + 0.0725);
            }
            else if (clima == "CHUVA" && veiculo.TipoCombustivel == "alcool" && veiculo.EstadoPneu == 1)
            {
                alcool -= alcool * 0.12;
                autonomia -= alcool * (0.30 + 0.0915);
            }
            else if (clima == "NEVE" && veiculo.TipoCombustivel == "gasolina" && veiculo.EstadoPneu == 3)
                autonomia -= autonomia * 0.19;

            else if (clima == "NEVE" && veiculo.TipoCombustivel == "gasolina" && veiculo.EstadoPneu == 2)
                autonomia -= autonomia * 0.2625;
            
            else if (clima == "NEVE" && veiculo.TipoCombustivel == "gasolina" && veiculo.EstadoPneu == 1)
                autonomia -= autonomia * 0.2815;
            
            else if (clima == "NEVE" && veiculo.TipoCombustivel == "alcool" && veiculo.EstadoPneu == 3)
            {
                alcool -= alcool * 0.19;
                autonomia -= alcool * 0.30;
            }
            else if (clima == "NEVE" && veiculo.TipoCombustivel == "alcool" && veiculo.EstadoPneu == 2)
            {
                alcool -= alcool * 0.19;
                autonomia -= alcool * (0.30 + 0.0725);
            }
            else if (clima == "NEVE" && veiculo.TipoCombustivel == "alcool" && veiculo.EstadoPneu == 2)
            {
                alcool -= alcool * 0.19;
                autonomia -= alcool * (0.30 + 0.0915);
            }
            return autonomia;
        } // CALCULA AUTONOMIA DE UM CARRO ONDE SÓ PODE GASOLINA OU ALCOOL
        public static double CalcularAutonomiaFlexA(string clima, Veiculo veiculo)
        {
            double autonomia = 0;
            autonomia = veiculo.KmPorAlcool;

            if (clima == "SOL")
            {
                if (veiculo.EstadoPneu == 2)
                    autonomia -= autonomia * 0.0725;
                else if (veiculo.EstadoPneu == 1)
                    autonomia -= autonomia * 0.0915;
            }
            else if (clima == "CHUVA" && veiculo.EstadoPneu == 3)
            {
                veiculo.KmPorGasolina -= veiculo.KmPorGasolinaAtual * 0.12;
                autonomia -= veiculo.KmPorGasolina * 0.30;
            }
            else if (clima == "CHUVA" && veiculo.EstadoPneu == 2)
            {
                veiculo.KmPorGasolina -= veiculo.KmPorGasolinaAtual * 0.12;
                autonomia -= veiculo.KmPorGasolina * (0.30 + 0.0725);
            }
            else if (clima == "CHUVA" && veiculo.EstadoPneu == 1)
            {
                veiculo.KmPorGasolina -= veiculo.KmPorGasolinaAtual * 0.12;
                autonomia -= veiculo.KmPorGasolina * (0.30 + 0.0915);
            }
            else if (clima == "NEVE" && veiculo.EstadoPneu == 3)
            {
                veiculo.KmPorGasolina -= veiculo.KmPorGasolinaAtual * 0.19;
                autonomia -= veiculo.KmPorGasolina * 0.30;
            }
            else if (clima == "NEVE" && veiculo.EstadoPneu == 2)
            {
                veiculo.KmPorGasolina -= veiculo.KmPorGasolinaAtual * 0.19;
                autonomia -= veiculo.KmPorGasolina * (0.30 + 0.0725);
            }
            else if (clima == "NEVE" && veiculo.EstadoPneu == 1)
            {
                veiculo.KmPorGasolina -= veiculo.KmPorGasolinaAtual * 0.19;
                autonomia -= veiculo.KmPorGasolina * (0.30 + 0.0915);
            }
            return autonomia;
        } // CALCULA AUTONOMIA DO ALCOOL DE UM CARRO ONDE PODE AMBOS OS COMBUSTIVEIS
        public static double CalcularAutonomiaFlexG(string clima, Veiculo veiculo)
        {
            double autonomia = 0;
            autonomia = veiculo.KmPorGasolinaAtual;
            if (clima == "SOL")
            {
                if (veiculo.EstadoPneu == 2)
                    autonomia -= autonomia * 0.0725;
                else if (veiculo.EstadoPneu == 1)
                    autonomia -= autonomia * 0.0915;
            }
            else if (clima == "CHUVA" && veiculo.EstadoPneu == 3)
                autonomia -= autonomia * 0.12;
            else if (clima == "CHUVA" && veiculo.EstadoPneu == 2)
                autonomia -= autonomia * (0.12 + 0.0725);
            else if (clima == "CHUVA" && veiculo.EstadoPneu == 1)
                autonomia -= autonomia * (0.12 + 0.0915);
            else if (clima == "NEVE" && veiculo.EstadoPneu == 3)
                autonomia -= autonomia * 0.19;
            else if (clima == "NEVE" && veiculo.EstadoPneu == 2)
                autonomia -= autonomia * (0.19 + 0.0725);
            else if (clima == "NEVE" && veiculo.EstadoPneu == 1)
                autonomia -= autonomia * (0.19 + 0.0915);

            return autonomia;
        } // CALCULA AUTONOMIA DO GASOLINA DE UM CARRO ONDE PODE AMBOS OS COMBUSTIVEIS
    }
}