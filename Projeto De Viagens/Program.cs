using Projeto_De_Viagens.Operações;


namespace Projeto_De_Viagens
{
    class Program
    {
        static void Main(string[] args)
        {
            AgenciaDeViagens agencia = new AgenciaDeViagens();
            Menu menu = new Menu();
            menu.MenuDaAgencia(agencia);
        }

    }
}
