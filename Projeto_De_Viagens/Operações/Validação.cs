using System;
using System.Text.RegularExpressions;

namespace Projeto_De_Viagens.Operações
{
    static class Validacao
    {
        public static string ValidarDescricao(string s) // Metodo para checar as descrições do veículo da por momento da MARCA E MODELO
        {
            while (!Regex.IsMatch(s, @"^[a-z A-ZãàâäèéêëîïôœùûüÿçÀÃÂÄÈÉÊËÎÏÔŒÙÛÜŸÇ0-9]+$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Descrição inválida, digite novamente");
                Console.ResetColor();
                s = Console.ReadLine();


            }
            return s;
        }

        public static string ValidarPlaca(string s) // Metodo para validar uma PLACA padronizada
        {
            while (!Regex.IsMatch(s, @"^[A-Z]{3}\-\d{4}$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Digite novamente uma placa válida Exemplo(KUR-2202)");
                Console.ResetColor();
                s = Console.ReadLine().ToUpper();
            }
            return s;
        }

        public static uint ValidarAno(string s) // Metodo para validar o ano entre 1910 e 2019
        {
            uint n;
            while ((!uint.TryParse(s, out n)) || (n < 1910 || n > 2019))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ano inválido, digite novamente, um ano entre 1910 e 2019");
                Console.ResetColor();
                s = Console.ReadLine();
            }
            return n;
        }

        public static double ValidarNumerosDouble(string s) // Metodo validar a maioria dos números double onde não há colocado um limite, mas onde não pode haver negativos
        {
            double n;
            while ((!double.TryParse(s, out n)) || (n <= 0.0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Numero inválido, digite novamente, que não seja negativo ou igual a 0");
                Console.ResetColor();
                s = Console.ReadLine();
            }
            return n;
        }

        public static int ValidarNumerosINT(string s) // Metodo validar a maioria dos números double onde não há colocado um limite, mas onde não pode haver negativos
        {
            int n;
            while ((!int.TryParse(s, out n)) || (n <= 0.0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Numero inválido, digite novamente, que não seja negativo ou igual a 0");
                Console.ResetColor();
                s = Console.ReadLine();
            }
            return n;
        }

        public static string ValidarSimOuNao(string s) // Metodo para validar resposta onde só aceita sim ou não (S OU N)
        {
            while (s != "S" && s != "N")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção inválida, digite novamente entre S para Sim e N para Não");
                Console.ResetColor();
                s = Console.ReadLine().ToUpper();
            }
            return s;
        }

        public static double ValidarAutonomia(string s) // Validar a autonomia do carro
        {
            double n;
            while ((!double.TryParse(s, out n)) || (n <= 0 || n > 15))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Autonomia inválida, digite novamente entre 1 a 15");
                Console.ResetColor();
                s = Console.ReadLine();
            }
            return n;
        }

        public static string ValidarTipoCombustivel(string s) // Validar o tipo de combustivel entre gasolina e alcool
        {
            while (s != "gasolina" && s != "alcool")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Combustivel inválido, digite algo entre gasolina e alcool");
                Console.ResetColor();
                s = Console.ReadLine().ToLower();
            }
            return s;
        }

        public static int Validar3opcoesINT(string s)
        {
            int n;
            while ((!int.TryParse(s, out n)) || (n < 1 || n > 3))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção invalida");
                Console.ResetColor();
                s = Console.ReadLine();

            }
            return n;

        }

        public static double ValidarLitros(string s) // Validar os litros de gasolina sendo maior que 0
        {
            double n;
            while ((!double.TryParse(s, out n)) || (n < 0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Litros inválido, digite novamente");
                Console.ResetColor();
                s = Console.ReadLine();
            }
            return n;
        }

        public static string Validar3opcoesString(string s)
        {
            while (s != "SOL" && s != "NEVE" && s != "CHUVA")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção invalida");
                Console.ResetColor();
                s = Console.ReadLine().ToUpper();
            }
            return s;
        }
    }
}
