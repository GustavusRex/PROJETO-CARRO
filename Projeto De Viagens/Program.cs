using Projeto_De_Viagens.BancoDeDados;
using Projeto_De_Viagens.Entidades;
using Projeto_De_Viagens.Operações;


namespace Projeto_De_Viagens
{
    class Program
    {
        public static AgenciaDeViagens agencia;
        static void Main(string[] args)
        {
           
            BancoJSON banco = new BancoJSON();

            agencia = banco.BuscarDadados();
            if(agencia == null)
            {
                agencia = new AgenciaDeViagens();
            }

            Menu menu = new Menu();
            menu.MenuDaAgencia(agencia, banco);
        }

    }
}
