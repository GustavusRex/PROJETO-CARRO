using Projeto_De_Viagens.Operações;
using System;
namespace Projeto_De_Viagens.Entidades
{
    public class ViagemCarro : Base
    {
        public Veiculo Veiculo;
        public Viagem Viagem;
        string opcao = "";
        public ViagemCarro(Viagem viagem, Veiculo veiculo)
        {
            Veiculo = veiculo;
            Viagem = viagem;
        }

        void AutenticaoParaBViagemFlex(Relatorio re, AgenciaDeViagens agencia) //METODO LOCAL ONDE VOU REPETIR A VALIDAÇÃO SE TEM COMBUSTIVEL E SE HÁ O ESTADO DO PNEU DIFERENTE DE ZERO (PARA CARRO FLEX)
        {

            if (Veiculo.LitrosAlcool <= 0.0 && Veiculo.LitrosGasolina <= 0.0)
            {
                Console.WriteLine("Você está sem combustivel... deseja abastecer?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    Veiculo.AbastecerVeiculoFlex();
                    re.ParadasAbastecimento++;
                }
                else
                {
                    Console.WriteLine("Como não há combustivel é impossivel continuar");
                    agencia.Relatorios.Add(re);
                    return;
                }
            }
            if (Veiculo.EstadoPneu == 0)
            {
                Console.WriteLine("Seu pneu está totalmente descalibrado... deseja calibrar?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    Veiculo.Calibrar();
                    re.ParadasCalibragem++;
                }
                else
                {
                    Console.WriteLine("Como o pneu está em condição 0 é impossivel continuar");
                    agencia.Relatorios.Add(re);
                    return;
                }
            }
        }
        void AutenticaoParaBViagemPadrao(double tanque, Relatorio re, AgenciaDeViagens agencia)
        {
            Console.WriteLine($"Faltam {Viagem.Distancia} KM");
            if (tanque <= 0.0)
            {
                Console.WriteLine("Você está sem combustivel... deseja abastecer?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    Veiculo.AbastecerVeiculoPadrão();
                    re.ParadasAbastecimento++;
                }
                else
                {
                    Console.WriteLine("Como não há combustivel é impossivel continuar");
                    agencia.Relatorios.Add(re);
                    return;
                }
            }
            if (Veiculo.EstadoPneu == 0)
            {
                Console.WriteLine("Seu pneu está totalmente descalibrado... deseja calibrar?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    Veiculo.Calibrar();
                    re.ParadasCalibragem++;
                }
                else
                {
                    Console.WriteLine("Como o pneu está em condição 0 é impossivel continuar");
                    agencia.Relatorios.Add(re);
                    return;
                }
            }
        } //METODO LOCAL ONDE VOU REPETIR A VALIDAÇÃO SE TEM COMBUSTIVEL E SE HÁ O ESTADO DO PNEU DIFERENTE DE ZERO (PARA CARRO PADRAO)
        public void DirigirNaViagemAutomatico(AgenciaDeViagens agencia) // PERCORRER A VIAGEM COM VEICULO DE MODO AUTOMATICO, ONDE VAI ATÉ ONDE O COMBUSTIVEL ACABAR
        {
            Relatorio relatorio = new Relatorio(this);                 
            // VARIAVEIS AUXILIARES PARA NÃO ALTERAR AS VARIAVEIS DO CARRO
            Veiculo.KmPorAlcool = Veiculo.KmPorAlcoolAtual;   
            Veiculo.KmPorGasolina = Veiculo.KmPorGasolinaAtual;
            Veiculo.EstadoPneu = Veiculo.EstadoPneuAtual;
            Viagem.Clima = Viagem.ClimaAtual;

            double AutonomiaA;
            double AutonomiaG;
            double cont = 0.0;
            Console.Clear();
            //CADA TIPO DE COMBUSTIVEL TEM SEU MODO DE DIRIGIR POR CONTA DO GASTOS DIFERENTES EM SEU TANQUE
            if (Veiculo.TipoCombustivel == "gasolina") 
            {
                AutenticaoParaBViagemPadrao(Veiculo.LitrosGasolina, relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    AutonomiaG = Calculo.CalcularAutonomiaPadrao(Viagem.Clima, Veiculo, Veiculo.KmPorGasolinaAtual);
                    Viagem.Distancia = Math.Round((Viagem.Distancia - 0.1), 2);
                    relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                    Veiculo.LitrosGasolina = Math.Round((Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                    relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                    if (Veiculo.LitrosGasolina <= 0.0)
                    {
                        Veiculo.LitrosGasolina = 0.0;
                        AutenticaoParaBViagemPadrao(Veiculo.LitrosGasolina, relatorio, agencia); // CASO PARE E NÃO COMPLETOU 
                        if (opcao == "N") return;
                    }
                    cont = Math.Round((cont + 0.1), 1);
                    if (cont == 100)
                    {
                        Veiculo.AlteracaoPneu();
                        relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, Veiculo.EstadoPneu);
                        Viagem.AlteracaoClima();
                        relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, Viagem.Clima);

                        cont = 0;
                    }

                } while (Viagem.Distancia > 0);
                Console.WriteLine("Você completou a viagem!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();

                agencia.Relatorios.Add(relatorio);
            }

            else if (Veiculo.TipoCombustivel == "alcool")
            {
                AutenticaoParaBViagemPadrao(Veiculo.LitrosAlcool, relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    AutonomiaA = Calculo.CalcularAutonomiaPadrao(Viagem.Clima, Veiculo, Veiculo.KmPorAlcoolAtual);
                    Viagem.Distancia = Math.Round((Viagem.Distancia - 0.1), 2);
                    relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                    Veiculo.LitrosAlcool = Math.Round((Veiculo.KmPorAlcool - (0.1 / AutonomiaA)), 2);
                    relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                    if (Veiculo.LitrosAlcool <= 0.0)
                    {
                        Veiculo.LitrosGasolina = 0.0;
                        AutenticaoParaBViagemPadrao(Veiculo.LitrosAlcool, relatorio, agencia);
                        if (opcao == "N") return;
                    }
                    cont = Math.Round((cont + 0.1), 1);
                    if (cont == 100)
                    {
                        Veiculo.AlteracaoPneu();
                        relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, Veiculo.EstadoPneu);
                        Viagem.AlteracaoClima();
                        relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, Viagem.Clima);

                        cont = 0;
                    }

                } while (Viagem.Distancia > 0);

                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Você completou a viagem!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }

            else if (Veiculo.Flex)
            {
                AutenticaoParaBViagemFlex(relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    AutonomiaA = Calculo.CalcularAutonomiaFlexA(Viagem.Clima, Veiculo);
                    AutonomiaG = Calculo.CalcularAutonomiaFlexG(Viagem.Clima, Veiculo);
                    Viagem.Distancia = Math.Round((Viagem.Distancia - 0.1), 2);
                    relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                    if (Veiculo.LitrosAlcool > 0.0)
                    Veiculo.LitrosAlcool = Math.Round((Veiculo.LitrosAlcool - (0.1 / AutonomiaA)), 2);
                    relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                    if (Veiculo.LitrosAlcool <= 0.0)
                    {
                        Veiculo.LitrosAlcool = 0.0;
                        Veiculo.LitrosGasolina = Math.Round((Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                        relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                    }
                    if (Veiculo.LitrosAlcool <= 0.0 && Veiculo.LitrosGasolina <= 0.0)
                    {
                        Veiculo.LitrosGasolina = 0.0;
                        Veiculo.LitrosAlcool = 0.0;
                        AutenticaoParaBViagemFlex(relatorio, agencia);
                        if (opcao == "N") return;
                    }
                    cont = Math.Round((cont + 0.1), 1);
                    if (cont == 100)
                    {
                        Veiculo.AlteracaoPneu();
                        relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, Veiculo.EstadoPneu);
                        Viagem.AlteracaoClima();
                        relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, Viagem.Clima);

                        cont = 0;
                    }

                } while (Viagem.Distancia > 0);

                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Você completou a viagem!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }
        }
        public void DirigirNaViagemProcedural(AgenciaDeViagens agencia) // PERCORRER A VIAGEM COM UM VEICULO DE MODO PROCEDURAL, ONDE RODA DEPENDENDO ATÉ ONDE O USUARIO DIGITOU E PARA EM UM POSTO
        {
            Relatorio relatorio = new Relatorio(this);  

            Veiculo.KmPorAlcool = Veiculo.KmPorAlcoolAtual;
            Veiculo.KmPorGasolina = Veiculo.KmPorGasolinaAtual;
            Veiculo.EstadoPneu = Veiculo.EstadoPneuAtual;
            Viagem.Clima = Viagem.ClimaAtual;

            double KmRodar = 0.0;
            double AutonomiaA;
            double AutonomiaG;
            double cont = 0.0;

            if (Veiculo.TipoCombustivel == "gasolina")
            {
                AutenticaoParaBViagemPadrao(Veiculo.LitrosGasolina, relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"A viagem tem: {Viagem.Distancia}");
                    Console.Write("Digite quantos KM deseja rodar com o veiculo: ");
                    KmRodar += Validacao.ValidarNumerosDouble(Console.ReadLine());
                    while (relatorio.KmPercorrido < KmRodar && Viagem.Distancia > 0)
                    {
                        Console.Clear();
                        AutonomiaG = Calculo.CalcularAutonomiaPadrao(Viagem.Clima, Veiculo, Veiculo.KmPorGasolinaAtual);
                        Viagem.Distancia = Math.Round((Viagem.Distancia - 0.1), 2);
                        relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                        Veiculo.LitrosGasolina = Math.Round((Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                        relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                        if (Veiculo.LitrosGasolina <= 0.0)
                        {
                            Veiculo.LitrosGasolina = 0.0;
                            AutenticaoParaBViagemPadrao(Veiculo.LitrosGasolina, relatorio, agencia);
                            if (opcao == "N") return;
                        }
                        cont = Math.Round((cont + 0.1), 1);
                        if (cont == 100)
                        {
                            Veiculo.AlteracaoPneu();
                            relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, Veiculo.EstadoPneu);
                            Viagem.AlteracaoClima();
                            relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, Viagem.Clima);

                            cont = 0;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Você Percorreu a distancia digitada e parou no posto");
                    Console.Write("Deseja Abastecer? Digite S para Sim e N para Não: ");
                    opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                    if (opcao == "S")
                    {
                        Veiculo.AbastecerVeiculoPadrão();
                        relatorio.ParadasAbastecimento++;
                        Console.Clear();
                    }
                    Console.WriteLine("Deseja fazer alguma calibragem? Digite S para Sim e N para Não");
                    opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                    if (opcao == "S")
                    {
                        Veiculo.Calibrar();
                        relatorio.ParadasCalibragem++;
                    }

                } while (Viagem.Distancia > 0);

                Console.WriteLine("Você completou a viagem!");
                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }

            else if (Veiculo.TipoCombustivel == "alcool")
            {
                AutenticaoParaBViagemPadrao(Veiculo.LitrosAlcool, relatorio, agencia);
                if (opcao == "N") return;

                do
                {                                 
                    Console.WriteLine($"A viagem tem: {Viagem.Distancia}");
                    Console.Write("Digite quantos KM deseja rodar com o veiculo: ");
                    KmRodar += Validacao.ValidarNumerosDouble(Console.ReadLine());
                    while (relatorio.KmPercorrido < KmRodar && Viagem.Distancia > 0)
                    {
                        Console.Clear();
                        AutonomiaA = Calculo.CalcularAutonomiaPadrao(Viagem.Clima, Veiculo, Veiculo.KmPorAlcoolAtual);
                        Viagem.Distancia = Math.Round((Viagem.Distancia - 0.1), 2);
                        Veiculo.LitrosAlcool = Math.Round((Veiculo.LitrosAlcool - (0.1 / AutonomiaA)), 2);
                        relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                        if (Veiculo.LitrosAlcool <= 0.0)
                        {
                            Veiculo.LitrosAlcool = 0.0;
                            AutenticaoParaBViagemPadrao(Veiculo.LitrosAlcool, relatorio, agencia);
                            if (opcao == "N") return;
                        }
                        cont++;
                        if (cont == 100)
                        {
                            Veiculo.AlteracaoPneu();
                            relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, Veiculo.EstadoPneu);
                            Viagem.AlteracaoClima();
                            relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, Viagem.Clima);

                            cont = 0;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Você Percorreu a distancia digitada e parou no posto");
                    Console.Write("Deseja Abastecer? Digite S para Sim e N para Não");
                    opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                    if (opcao == "S")
                    {
                        Veiculo.AbastecerVeiculoPadrão();
                        relatorio.ParadasAbastecimento++;
                        Console.Clear();
                    }
                    Console.WriteLine("Deseja fazer alguma calibragem? Digite S para Sim e N para Não");
                    opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                    if (opcao == "S")
                    {
                        Veiculo.Calibrar();
                        relatorio.ParadasCalibragem++;
                    }

                } while (Viagem.Distancia > 0);

                Console.WriteLine("Você completou a viagem!");
                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }

            else if (Veiculo.Flex)
            {
                AutenticaoParaBViagemFlex(relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                   
                    Console.WriteLine($"A viagem tem: {Viagem.Distancia}");
                    Console.Write("Digite quantos KM deseja rodar com o veiculo: ");
                    KmRodar += Validacao.ValidarNumerosDouble(Console.ReadLine());
                    while (relatorio.KmPercorrido < KmRodar && Viagem.Distancia > 0)
                    {
                        Console.Clear();
                        AutonomiaA = Calculo.CalcularAutonomiaPadrao(Viagem.Clima, Veiculo, Veiculo.KmPorAlcoolAtual);
                        AutonomiaG = Calculo.CalcularAutonomiaPadrao(Viagem.Clima, Veiculo, Veiculo.KmPorAlcoolAtual);
                        Viagem.Distancia = Math.Round((Viagem.Distancia - 0.1), 2);
                        relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                        if (Veiculo.LitrosAlcool > 0.0)
                            Veiculo.LitrosAlcool = Math.Round((Veiculo.LitrosAlcool - (0.1 / AutonomiaA)), 2);
                            relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                        if (Veiculo.LitrosAlcool <= 0.0)
                        {
                            Veiculo.LitrosAlcool = 0.0;
                            Veiculo.LitrosGasolina = Math.Round((Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                            relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                        }
                        if (Veiculo.LitrosAlcool <= 0.0 && Veiculo.LitrosGasolina <= 0.0)
                        {
                            Veiculo.LitrosGasolina = 0.0;
                            Veiculo.LitrosAlcool = 0.0;
                            AutenticaoParaBViagemFlex(relatorio, agencia);
                            if (opcao == "N") return;
                        }
                        cont = Math.Round((cont + 0.1), 1);
                        if (cont == 100)
                        {
                            Veiculo.AlteracaoPneu();
                            relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, Veiculo.EstadoPneu);
                            Viagem.AlteracaoClima();
                            relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, Viagem.Clima);
                            cont = 0;
                        }

                    }
                    Console.Clear();
                    Console.WriteLine("Você Percorreu a distancia digitada e parou no posto");
                    Console.Write("Deseja Abastecer? Digite S para Sim e N para Não: ");
                    opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                    if (opcao == "S")
                    {
                        Veiculo.AbastecerVeiculoPadrão();
                        relatorio.ParadasAbastecimento++;
                        Console.Clear();
                    }
                    Console.WriteLine("Deseja fazer alguma calibragem? Digite S para Sim e N para Não");
                    opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                    if (opcao == "S")
                    {
                        Veiculo.Calibrar();
                        relatorio.ParadasCalibragem++;
                    }

                } while (Viagem.Distancia > 0);

                Console.WriteLine("Você completou a viagem!");
                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }
        }
        public override string ToString()
        {
            return $"ID da ViagemProgramada {ID}, com a Data {DataCadastro} \n " +
                   $"Com o carro {Veiculo.Marca} \n " +
                   $"e com a viagem {Viagem.Distancia} KM \n ";
        }
    }
}
