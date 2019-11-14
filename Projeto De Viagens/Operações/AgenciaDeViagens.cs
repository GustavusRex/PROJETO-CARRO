using Projeto_De_Viagens.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_De_Viagens.Operações
{
    public class AgenciaDeViagens
    {
        public List<Veiculo> Veiculos = new List<Veiculo>();
        public List<Viagem> Viagens = new List<Viagem>();
        public List<ViagemCarro> CarroViagens = new List<ViagemCarro>();
        public List<Relatorio> Relatorios = new List<Relatorio>();
        int Codigo;
        string opcao;
        public void CadastrarCarro()
        {
            Console.Clear();
            Veiculo veiculo = new Veiculo();

            Console.WriteLine("===================Cadastro de Carro Selecionado===================");
            Console.Write("\n\nDigite a marca do veículo: ");
            veiculo.Marca = Validacao.ValidarDescricao(Console.ReadLine());
            Console.Write("\nDigite o tipo de modelo do veiculo: ");
            veiculo.Modelo = Validacao.ValidarDescricao(Console.ReadLine());
            Console.Write("\nDigite a placa do veículo, EXEMPLO DE PLACA(KUR-2202): ");
            veiculo.Placa = Validacao.ValidarPlaca(Console.ReadLine().ToUpper());
            Console.Write("\nDigite o ano entre 1910 e 2019: ");
            veiculo.Ano = Validacao.ValidarAno(Console.ReadLine());
            Console.Write("\nDigite o estado do pneu atual 1 - Ruim  2 - Moderado, 3 - Perfeito: ");
            veiculo.EstadoPneuAtual = Validacao.Validar3opcoesINT(Console.ReadLine());
            Console.Write("\nDigite a velocidade maxima do veiculo: ");
            veiculo.VelocidadeMaxima = Validacao.ValidarNumerosDouble(Console.ReadLine());
            Console.Write("\nDigite a capacidade maxima do tanque de combustivel: ");
            veiculo.CapacidadeTanque = Validacao.ValidarNumerosDouble(Console.ReadLine());
            Console.WriteLine("\nO veículo é do tipo flex? " +
                         "Digite S para Sim e N para Não");
            veiculo.Flex = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper()) == "S" ? true : false;

            if (veiculo.Flex)
            {
                Console.WriteLine("\nDigite a Kilometragem por Litro de Gasolina (Entre 1 até 15): ");
                veiculo.KmPorGasolinaAtual = Validacao.ValidarAutonomia(Console.ReadLine());
                Console.WriteLine("\nDigite a Kilometragem por Litro de Alcool (Entre 1 até 15): ");
                veiculo.KmPorAlcoolAtual = Validacao.ValidarAutonomia(Console.ReadLine());
            }

            if (veiculo.Flex == false)
            {
                Console.WriteLine("\nQual o tipo de Combustivel (Entre gasolina e alcool) que o veículo utiliza? ");
                veiculo.TipoCombustivel = Validacao.ValidarTipoCombustivel(Console.ReadLine().ToLower());

                if (veiculo.TipoCombustivel == "gasolina")
                {
                    Console.WriteLine("Digite a Kilometragem por Litro de Gasolina (Entre 1 até 15): ");
                    veiculo.KmPorGasolinaAtual = Validacao.ValidarAutonomia(Console.ReadLine());
                }
                else if (veiculo.TipoCombustivel == "alcool")
                {
                    Console.WriteLine("\nDigite a Kilometragem por Litro de Alcool (Entre 1 até 15): ");
                    veiculo.KmPorAlcoolAtual = Validacao.ValidarAutonomia(Console.ReadLine());

                }
            }
            Console.WriteLine("\nCarro Cadastrado Com Sucesso!");
            Veiculos.Add(veiculo);
            Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
            Console.ReadKey();         
        }

        public void CadastrarViagem()
        {
            Console.Clear();
            Viagem viagem = new Viagem();
            Console.WriteLine("===================Cadastro de Viagem Selecionado===================");
            Console.WriteLine("Qual a distância da viagem?");
            viagem.Distancia = Validacao.ValidarNumerosDouble(Console.ReadLine());
            Console.WriteLine("O clima qual o tipo de clima  Sol ou Chuva ou Neve");
            viagem.ClimaAtual = Validacao.Validar3opcoesString(Console.ReadLine().ToUpper());
            Console.WriteLine("\n Viagem Cadastrada com sucesso!");
            Viagens.Add(viagem);
            Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
            Console.ReadKey();
        }

        public void AtribuirCarroAViagem() // COLOCAR UM VEICULO DENTRO DE UMA VIAGEM ESPECIFICA
        {
            
            Console.Clear();
            Console.WriteLine("===================Atribuir Carro a Uma Viagem===================");
            if (Viagens.Count == 0)
            {
                Console.WriteLine("\n\nNão existe viagens cadastradas, cadastre uma!");
                Console.WriteLine("\nDeseja Cadastrar uma Viagem para continuar?");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine());
                if (opcao == "S")
                    CadastrarViagem();
                else
                    return;
            }
            if (Veiculos.Count == 0)
            {
                Console.WriteLine("Não existe carros cadastrados, cadastre uma!");
                Console.WriteLine("\nDeseja Cadastrar um Carro para continuar?");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine());
                if (opcao == "S")
                    CadastrarCarro();
                else
                    return;
            }
            Console.Clear();
            Console.WriteLine("===================Atribuir Carro a Uma Viagem===================\n\n");

            Console.WriteLine("===================Lista de Viagens===================\n\n");

            Viagens.ForEach(v => {
                Console.WriteLine($"{v}\n\n");
            });

            Console.Write("\n\nDigite a ID da viagem: ");
            Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());           
            Viagem viagem = Viagens.Find(v => v.ID == Codigo);
            while(viagem == null) // VALIDAÇÃO PARA CASO ESCREVEU A ID DA VIAGEM ERRADA
            {
                Console.WriteLine("Viagem Existe! Porém foi digitado o codigo errado... Digite novamente");
                Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
                viagem = Viagens.Find(v => v.ID == Codigo);
            }

            Console.Clear();

            Console.WriteLine("===================Atribuir Carro a Uma Viagem===================\n\n");

            Console.WriteLine("===================Lista de Carros===================\n\n");

            Veiculos.ForEach(c => {
                Console.WriteLine($"{c}\n\n");
            });

            Console.Write("\n\nDigite a ID do carro: ");
            Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
            Veiculo veiculo = Veiculos.Find(c => c.ID == Codigo);
            while (veiculo == null)
            {
                Console.WriteLine("Carro Existe! Porém foi digitado o codigo errado... Digite novamente");
                Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
                veiculo = Veiculos.Find(v => v.ID == Codigo);
            }
            CarroViagens.Add(new ViagemCarro(viagem, veiculo));
            Viagens.Remove(viagem);
            Veiculos.Remove(veiculo);
            Console.WriteLine("\n Carro Atribuido a uma Viagem com sucesso");
            Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
            Console.ReadKey();
           
        }

        public void PercorrerViagem() // PERCORRER UMA VIAGEM QUE JÁ FOI ATRIBUIDA UM CARRO
        {
            
            if (CarroViagens.Count == 0)
            {
                Console.WriteLine("Não há Viagens para Percorrer");
                Console.WriteLine("\nDeseja Atribuir um Veiculo a Uma Viagem?");
                opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
                if (opcao == "S")
                    AtribuirCarroAViagem();
                else
                    return;
            }

            Console.Clear();

            Console.WriteLine("===================Percorrer Uma Viagem===================\n\n");

            Console.WriteLine("===================Lista de Viagens Para Percorrer===================\n\n");

            CarroViagens.ForEach(cv =>
            {
                Console.WriteLine($"{cv}\n\n");
            });
            Console.Write("Digite o ID da viagem registrada: ");
            Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
            ViagemCarro viagemCarro = CarroViagens.Find(x => x.ID == Codigo);
            while (viagemCarro == null)
            {
                Console.WriteLine("Viagem Existe! Porém foi digitado o codigo errado... Digite novamente");
                Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
                viagemCarro = CarroViagens.Find(v => v.ID == Codigo);
            }
            Console.Clear();
            Console.Write("Deseja fazer essa viagem de maneira automatica? Digite S para sim e N para não e faze-la de forma procedural: ");
            opcao = Validacao.ValidarSimOuNao(Console.ReadLine().ToUpper());
            if(viagemCarro._Viagem.Distancia == 0)
            {
                Console.WriteLine("Viagem já percorrida!!!");
                Console.ReadKey();
                return;
            }
            if (opcao == "S")
                viagemCarro.DirigirNaViagemAutomatico(this);
            else
                viagemCarro.DirigirNaViagemProcedural(this);
        }
        public void ExibirRelatorios() // EXIBIR RELATORIOS DAS VIAGENS FEITAS
        {
            Console.Clear();
            if (Relatorios.Count == 0)
            {
                Console.WriteLine("Não há relatorios para exibir!!!");
                Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("===================Lista de Viagens===================\n\n");
            CarroViagens.ForEach(cv =>
            {
                Console.WriteLine($"{cv}\n\n");
            });

            Console.Write("Para exibir um relátorio digite o ID da viagem desejada: ");
            Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
            Relatorio relatorio = Relatorios.Find(x => x._viagemCarro.ID == Codigo);
            while (relatorio == null)
            {
                Console.WriteLine("Relatorio Existe! Porém foi digitado o codigo errado... Digite novamente");
                Codigo = Validacao.ValidarNumerosINT(Console.ReadLine());
                relatorio = Relatorios.Find(x => x._viagemCarro.ID == Codigo);
            }
            Console.WriteLine($"{relatorio}");
            Console.WriteLine("Aperte qualquer coisa para voltar ao menu principal");
            Console.ReadKey();

        }
    }
}