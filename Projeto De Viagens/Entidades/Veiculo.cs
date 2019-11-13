using Projeto_De_Viagens.Operações;
using System;
using System.Text;

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

        public void AbastecerVeiculoPadrão() // ABASTECER CARROS DO TIPO SÓ ALCOOL OU SÓ GASOLINA
        {
            if (TipoCombustivel == "gasolina")
            {
                LitrosGasolina += Calculo.CalcularAbastecimento(CapacidadeTanque, LitrosGasolina);
            }
            else if (TipoCombustivel == "alcool")
            {
                LitrosAlcool += Calculo.CalcularAbastecimento(CapacidadeTanque, LitrosAlcool);
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
        } // ABASTECER UM CARRO QUE PODE OS 2 COMBUSTIVEIS
        public void Calibrar()
        {
            Console.WriteLine("Deseja qual opção para calibrar o pneu? 1 - Ruim , 2 - Moderado , 3 - Perfeito");
            int opcao = Validacao.Validar3opcoesINT(Console.ReadLine());

            if (opcao == 1)
                EstadoPneu = 1;
            else if (opcao == 2)
                EstadoPneu = 2;
            else
                EstadoPneu = 3;
        } // CALIBRAGEM DOS PNEUS
        public void AlteracaoPneu() // METODO QUE ALTERA O PNEU DURANTE A VIAGEM EXECUTADA
        {
           int random = new Random().Next(0, 2);
            EstadoPneu -= random;
        } 
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"ID {ID}, Data {DataCadastro}");
            sb.AppendLine($"O carro da marca {Marca}, do modelo {Modelo} com a placa {Placa}");
            sb.AppendLine($"Sendo do ano {Ano}, alcança a velocidade maxima de {VelocidadeMaxima}, tem a capacidade de {CapacidadeTanque} litros e o estado do pneu é {EstadoPneuAtual}");
            if (TipoCombustivel == "gasolina")
                sb.AppendLine($"Seu tipo de combustivel é {TipoCombustivel}, com seu Tanque inicial {LitrosGasolina} e com a autonomia de {KmPorGasolinaAtual} KM/L");
            else if (TipoCombustivel == "alcool")
                sb.AppendLine($"Seu tipo de combustivel é {TipoCombustivel}, com seu Tanque inicial {LitrosAlcool} e com a autonomia de {KmPorAlcoolAtual} KM/L");
            else if (Flex)
                sb.AppendLine($"Carro do tipo flex com seu Tanque inicial {LitrosAlcool} de alcool e {LitrosGasolina} de gasolina e com " +
                    $"a autonomia de alcool {KmPorAlcoolAtual} KM/L " +
                    $"e com a autonomia de gasolina {KmPorGasolinaAtual} KM/L");
            return sb.ToString();
        }
    }
    
}