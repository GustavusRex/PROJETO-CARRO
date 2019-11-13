using Projeto_De_Viagens.BancoDeDados;
using Projeto_De_Viagens.Operações;
using System;
namespace Projeto_De_Viagens
{
    public class Menu
    {
        string opcao;
        public void MenuDaAgencia(AgenciaDeViagens agencia, BancoJSON banco)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("===============Menu da Agencia de Viagens===============\n\n");
                Console.WriteLine("Digite 1 - Para Cadastrar um Carro");
                Console.WriteLine("Digite 2 - Para Exibir a Lista de Carros Disponiveis");
                Console.WriteLine("Digite 3 - Para Cadastar uma Viagem ");
                Console.WriteLine("Digite 4 - Para Exibir a Lista de Viagens Disponiveis");
                Console.WriteLine("Digite 5 - Para Atribuir um Carro a uma Viagem");
                Console.WriteLine("Digite 6 - Para Percorrer uma viagem");
                Console.WriteLine("Digite 7 - Para Exibir os Relatorios\n\n");

                Console.WriteLine("Digite qual opção deseja");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        {
                            agencia.CadastrarCarro();
                            banco.Salvar(agencia);
                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            if (agencia.Veiculos.Count == 0)
                            {
                                Console.WriteLine("Não há carros");
                                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("===================Lista de Veiculos===================\n\n");
                                agencia.Veiculos.ForEach(c =>
                                {
                                    Console.WriteLine($"{c}\n\n");
                                });
                                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                                Console.ReadKey();
                            }
                            break;

                        }
                    case "3":
                        {
                            agencia.CadastrarViagem();
                            banco.Salvar(agencia);
                            break;
                        }
                    case "4":
                        {
                            Console.Clear();
                            if (agencia.Veiculos.Count == 0)
                            {
                                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("===================Lista de Viagens===================\n\n");
                                agencia.Viagens.ForEach(v =>
                                {
                                    Console.WriteLine($"{v}\n\n");
                                });
                                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                                Console.ReadKey();
                            }
                            break;
                        }
                    case "5":
                        {
                            agencia.AtribuirCarroAViagem();
                            banco.Salvar(agencia);
                            break;
                        }
                    case "6":
                        {
                            agencia.PercorrerViagem();
                            banco.Salvar(agencia);
                            break;
                        }
                    case "7":
                        {
                            agencia.ExibirRelatorios();
                            banco.Salvar(agencia);
                            break;
                        }
                    default:
                        Console.WriteLine("Opção inválida");
                        Console.ReadKey();
                        break;
                }
            } while (true);
        }
    }
}
