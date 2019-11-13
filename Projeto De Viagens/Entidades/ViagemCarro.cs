using Projeto_De_Viagens.Operações;
using System;
namespace Projeto_De_Viagens.Entidades
{
    public class ViagemCarro : Base
    {
        public Veiculo _Veiculo;
        public Viagem _Viagem;
        string opcao = "";
        public ViagemCarro(Viagem viagem, Veiculo veiculo)
        {
            _Veiculo = veiculo;
            _Viagem = viagem;
        }

        void PosCorrida(Relatorio re)
        {
            Console.Clear();
            Console.WriteLine($"Você Percorreu a distancia digitada e parou no posto... Seu tanque está com {_Veiculo.LitrosAlcool} Litros de alcool e o estado do pneu está {_Veiculo.EstadoPneu}");
            Console.Write("Deseja Abastecer? Digite S para Sim e N para Não: ");
            opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
            if(opcao == "S" && _Veiculo.Flex)
            {
                _Veiculo.AbastecerVeiculoFlex();
                re.ParadasAbastecimento++;
                Console.Clear();
            }
           else if (opcao == "S")
            {
                _Veiculo.AbastecerVeiculoPadrão();
                re.ParadasAbastecimento++;
                Console.Clear();
            }
            Console.WriteLine("Deseja fazer alguma calibragem? Digite S para Sim e N para Não");
            opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
            if (opcao == "S")
            {
                _Veiculo.Calibrar();
                re.ParadasCalibragem++;
            }
        }
        void AutenticaoParaBViagemFlex(Relatorio re, AgenciaDeViagens agencia) //METODO LOCAL ONDE VOU REPETIR A VALIDAÇÃO SE TEM COMBUSTIVEL E SE HÁ O ESTADO DO PNEU DIFERENTE DE ZERO (PARA CARRO FLEX)
        {
            Console.WriteLine($"Faltam {_Viagem.Distancia} KM\n");
            if (_Veiculo.LitrosAlcool <= 0.0 && _Veiculo.LitrosGasolina <= 0.0)
            {
                Console.WriteLine("Você está sem combustivel... deseja abastecer?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    _Veiculo.AbastecerVeiculoFlex();
                    re.ParadasAbastecimento++;
                }
                else
                {
                    Console.WriteLine("Como não há combustivel é impossivel continuar");
                    agencia.Relatorios.Add(re);
                    return;
                }
            }
            if (_Veiculo.EstadoPneu == 0)
            {
                Console.WriteLine("Seu pneu está totalmente descalibrado... deseja calibrar?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    _Veiculo.Calibrar();
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
            Console.WriteLine($"Faltam {_Viagem.Distancia} KM");
            if (tanque <= 0.0)
            {
                Console.WriteLine("Você está sem combustivel... deseja abastecer?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    _Veiculo.AbastecerVeiculoPadrão();
                    re.ParadasAbastecimento++;
                }
                else
                {
                    Console.WriteLine("Como não há combustivel é impossivel continuar");
                    agencia.Relatorios.Add(re);
                    return;
                }
            }
            if (_Veiculo.EstadoPneu == 0)
            {
                Console.WriteLine("Seu pneu está totalmente descalibrado... deseja calibrar?\n");
                Console.Write("Digite S para Sim ou N para Não: ");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                {
                    _Veiculo.Calibrar();
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
            _Veiculo.KmPorAlcool = _Veiculo.KmPorAlcoolAtual;   
            _Veiculo.KmPorGasolina = _Veiculo.KmPorGasolinaAtual;
            _Veiculo.EstadoPneu = _Veiculo.EstadoPneuAtual;
            _Viagem.Clima = _Viagem.ClimaAtual;

            double AutonomiaA;
            double AutonomiaG;
            double cont = 0.0;
            Console.Clear();
            //CADA TIPO DE COMBUSTIVEL TEM SEU MODO DE DIRIGIR POR CONTA DO GASTOS DIFERENTES EM SEU TANQUE
            if (_Veiculo.TipoCombustivel == "gasolina") 
            {
                AutenticaoParaBViagemPadrao(_Veiculo.LitrosGasolina, relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    AutonomiaG = Calculo.CalcularAutonomiaPadrao(_Viagem.Clima, _Veiculo, _Veiculo.KmPorGasolinaAtual);
                    _Viagem.Distancia = Math.Round((_Viagem.Distancia - 0.1), 2);
                    relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                    _Veiculo.LitrosGasolina = Math.Round((_Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                    relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                    if (_Veiculo.LitrosGasolina <= 0.0)
                    {
                        _Veiculo.LitrosGasolina = 0.0;
                        AutenticaoParaBViagemPadrao(_Veiculo.LitrosGasolina, relatorio, agencia); // CASO PARE E NÃO COMPLETOU 
                        if (opcao == "N") return;
                    }
                    cont = Math.Round((cont + 0.1), 1);
                    if (cont == 100)
                    {
                        _Veiculo.AlteracaoPneu();
                        relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, _Veiculo.EstadoPneu);
                        _Viagem.AlteracaoClima();
                        relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, _Viagem.Clima);
                        AutenticaoParaBViagemPadrao(_Veiculo.LitrosGasolina, relatorio, agencia);
                        if (opcao == "N") return;
                        cont = 0;
                    }

                } while (_Viagem.Distancia > 0);
                Console.WriteLine("Você completou a viagem!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();

                agencia.Relatorios.Add(relatorio);
            }

            else if (_Veiculo.TipoCombustivel == "alcool")
            {
                AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    AutonomiaA = Calculo.CalcularAutonomiaPadrao(_Viagem.Clima, _Veiculo, _Veiculo.KmPorAlcoolAtual);
                    _Viagem.Distancia = Math.Round((_Viagem.Distancia - 0.1), 2);
                    relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                    _Veiculo.LitrosAlcool = Math.Round((_Veiculo.KmPorAlcool - (0.1 / AutonomiaA)), 2);
                    relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                    if (_Veiculo.LitrosAlcool <= 0.0)
                    {
                        _Veiculo.LitrosAlcool = 0.0;
                        AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                        if (opcao == "N") return;
                    }
                    cont = Math.Round((cont + 0.1), 1);
                    if (cont == 100)
                    {
                        _Veiculo.AlteracaoPneu();
                        relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, _Veiculo.EstadoPneu);
                        _Viagem.AlteracaoClima();
                        relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, _Viagem.Clima);
                        AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                        if (opcao == "N") return;
                        cont = 0;
                    }

                } while (_Viagem.Distancia > 0);

                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Você completou a viagem!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }

            else if (_Veiculo.Flex)
            {
                AutenticaoParaBViagemFlex(relatorio, agencia);
                if (opcao == "N") return;
                do
                {
                    AutonomiaA = Calculo.CalcularAutonomiaFlexA(_Viagem.Clima, _Veiculo);
                    AutonomiaG = Calculo.CalcularAutonomiaFlexG(_Viagem.Clima, _Veiculo);
                    _Viagem.Distancia = Math.Round((_Viagem.Distancia - 0.1), 2);
                    relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                    if (_Veiculo.LitrosAlcool > 0.0)
                    _Veiculo.LitrosAlcool = Math.Round((_Veiculo.LitrosAlcool - (0.1 / AutonomiaA)), 2);
                    relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                    if (_Veiculo.LitrosAlcool <= 0.0)
                    {
                        _Veiculo.LitrosAlcool = 0.0;
                        _Veiculo.LitrosGasolina = Math.Round((_Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                        relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                        AutenticaoParaBViagemFlex(relatorio, agencia);
                        if (opcao == "N") return;
                    }
                    if (_Veiculo.LitrosAlcool <= 0.0 && _Veiculo.LitrosGasolina <= 0.0)
                    {
                        _Veiculo.LitrosGasolina = 0.0;
                        _Veiculo.LitrosAlcool = 0.0;
                        AutenticaoParaBViagemFlex(relatorio, agencia);
                        if (opcao == "N") return;
                    }
                    cont = Math.Round((cont + 0.1), 1);
                    if (cont == 100)
                    {
                        _Veiculo.AlteracaoPneu();
                        relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, _Veiculo.EstadoPneu);
                        _Viagem.AlteracaoClima();
                        relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, _Viagem.Clima);
                        AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                        if (opcao == "N") return;
                        cont = 0;
                    }

                } while (_Viagem.Distancia > 0);

                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Você completou a viagem!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }
        }
        public void DirigirNaViagemProcedural(AgenciaDeViagens agencia) // PERCORRER A VIAGEM COM UM VEICULO DE MODO PROCEDURAL, ONDE RODA DEPENDENDO ATÉ ONDE O USUARIO DIGITOU E PARA EM UM POSTO
        {
            Relatorio relatorio = new Relatorio(this);  

            _Veiculo.KmPorAlcool = _Veiculo.KmPorAlcoolAtual;
            _Veiculo.KmPorGasolina = _Veiculo.KmPorGasolinaAtual;
            _Veiculo.EstadoPneu = _Veiculo.EstadoPneuAtual;
            _Viagem.Clima = _Viagem.ClimaAtual;

            double KmRodar = 0.0;
            double AutonomiaA;
            double AutonomiaG;
            double cont = 0.0;

            if (_Veiculo.TipoCombustivel == "gasolina")
            {
                AutenticaoParaBViagemPadrao(_Veiculo.LitrosGasolina, relatorio, agencia);
                if (opcao == "N") return;
                do
                {

                    Console.WriteLine($"A viagem tem: {_Viagem.Distancia}");
                    Console.Write("Digite quantos KM deseja rodar com o veiculo: ");
                    KmRodar += Validacao.ValidarNumerosDouble(Console.ReadLine());
                    while (relatorio.KmPercorrido < KmRodar && _Viagem.Distancia > 0)
                    {
                        Console.Clear();
                        AutonomiaG = Calculo.CalcularAutonomiaPadrao(_Viagem.Clima, _Veiculo, _Veiculo.KmPorGasolinaAtual);
                        _Viagem.Distancia = Math.Round((_Viagem.Distancia - 0.1), 2);
                        relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                        _Veiculo.LitrosGasolina = Math.Round((_Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                        relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                        if (_Veiculo.LitrosGasolina <= 0.0)
                        {
                            _Veiculo.LitrosGasolina = 0.0;
                            AutenticaoParaBViagemPadrao(_Veiculo.LitrosGasolina, relatorio, agencia);
                            if (opcao == "N") return;
                        }
                        cont = Math.Round((cont + 0.1), 1);
                        if (cont == 100)
                        {
                            _Veiculo.AlteracaoPneu();
                            relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, _Veiculo.EstadoPneu);
                            _Viagem.AlteracaoClima();
                            relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, _Viagem.Clima);
                            if(_Veiculo.EstadoPneu == 0)
                            AutenticaoParaBViagemPadrao(_Veiculo.LitrosGasolina, relatorio, agencia);
                            if (opcao == "N") return;
                            cont = 0;
                        }
                    }
                    PosCorrida(relatorio);
                } while (_Viagem.Distancia > 0);

                Console.WriteLine("Você completou a viagem!");
                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }

            else if (_Veiculo.TipoCombustivel == "alcool")
            {
                AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                if (opcao == "N") return;
                do
                {                  
                    Console.WriteLine($"A viagem tem: {_Viagem.Distancia}");
                    Console.Write("Digite quantos KM deseja rodar com o veiculo: ");
                    KmRodar += Validacao.ValidarNumerosDouble(Console.ReadLine());
                    while (relatorio.KmPercorrido < KmRodar && _Viagem.Distancia > 0)
                    {
                        
                        AutonomiaA = Calculo.CalcularAutonomiaPadrao(_Viagem.Clima, _Veiculo, _Veiculo.KmPorAlcoolAtual);
                        _Viagem.Distancia = Math.Round((_Viagem.Distancia - 0.1), 2);
                        relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                        _Veiculo.LitrosAlcool = Math.Round((_Veiculo.LitrosAlcool - (0.1 / AutonomiaA)), 2);
                        relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                        if (_Veiculo.LitrosAlcool <= 0.0)
                        {
                            _Veiculo.LitrosAlcool = 0.0;
                            AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                            if (opcao == "N") return;
                        }
                        cont = Math.Round((cont + 0.1), 1);
                        if (cont == 100)
                        {
                            _Veiculo.AlteracaoPneu();
                            relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, _Veiculo.EstadoPneu);
                            _Viagem.AlteracaoClima();
                            relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, _Viagem.Clima);
                            if (_Veiculo.EstadoPneu == 0)
                                AutenticaoParaBViagemPadrao(_Veiculo.LitrosAlcool, relatorio, agencia);
                            if (opcao == "N") return;
                            cont = 0;
                        }
                    }
                    PosCorrida(relatorio);

                } while (_Viagem.Distancia > 0);

                Console.WriteLine("Você completou a viagem!");
                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }

            else if (_Veiculo.Flex)
            {
                AutenticaoParaBViagemFlex(relatorio, agencia);
                if (opcao == "N") return;
                do
                {                
                    Console.WriteLine($"A viagem tem: {_Viagem.Distancia}");
                    Console.Write("Digite quantos KM deseja rodar com o veiculo: ");
                    KmRodar += Validacao.ValidarNumerosDouble(Console.ReadLine());
                    while (relatorio.KmPercorrido < KmRodar && _Viagem.Distancia > 0)
                    {
                        Console.Clear();
                        AutonomiaA = Calculo.CalcularAutonomiaPadrao(_Viagem.Clima, _Veiculo, _Veiculo.KmPorAlcoolAtual);
                        AutonomiaG = Calculo.CalcularAutonomiaPadrao(_Viagem.Clima, _Veiculo, _Veiculo.KmPorAlcoolAtual);
                        _Viagem.Distancia = Math.Round((_Viagem.Distancia - 0.1), 2);
                        relatorio.KmPercorrido = Math.Round((relatorio.KmPercorrido + 0.1), 2);
                        if (_Veiculo.LitrosAlcool > 0.0)
                            _Veiculo.LitrosAlcool = Math.Round((_Veiculo.LitrosAlcool - (0.1 / AutonomiaA)), 2);
                            relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaA)), 2);
                        if (_Veiculo.LitrosAlcool <= 0.0)
                        {
                            _Veiculo.LitrosAlcool = 0.0;
                            _Veiculo.LitrosGasolina = Math.Round((_Veiculo.LitrosGasolina - (0.1 / AutonomiaG)), 2);
                            relatorio.LitrosConsumidos = Math.Round((relatorio.LitrosConsumidos + (0.1 / AutonomiaG)), 2);
                            if (opcao == "N") return;
                        }
                        if (_Veiculo.LitrosAlcool <= 0.0 && _Veiculo.LitrosGasolina <= 0.0)
                        {
                            _Veiculo.LitrosGasolina = 0.0;
                            _Veiculo.LitrosAlcool = 0.0;
                            AutenticaoParaBViagemFlex(relatorio, agencia);
                            if (opcao == "N") return;
                        }
                        cont = Math.Round((cont + 0.1), 1);
                        if (cont == 100)
                        {
                            _Veiculo.AlteracaoPneu();
                            relatorio.DesgasteDoPneuEvento(relatorio.KmPercorrido, _Veiculo.EstadoPneu);
                            _Viagem.AlteracaoClima();
                            relatorio.MudancaClimaticaDoEvento(relatorio.KmPercorrido, _Viagem.Clima);
                            if (_Veiculo.EstadoPneu == 0)
                                AutenticaoParaBViagemFlex(relatorio, agencia);
                            if (opcao == "N") return;
                            cont = 0;
                        }
                    }
                    PosCorrida(relatorio);

                } while (_Viagem.Distancia > 0);

                Console.WriteLine("Você completou a viagem!");
                agencia.Relatorios.Add(relatorio);
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
            }
        }
        public override string ToString()
        {
            return $"ID da ViagemProgramada {ID}, com a Data {DataCadastro} \n " +
                   $"Com o carro {_Veiculo.Marca} \n " +
                   $"e com a viagem {_Viagem.Distancia} KM \n ";
        }
    }
}