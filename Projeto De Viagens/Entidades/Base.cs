using System;
namespace Projeto_De_Viagens.Entidades
{
    public abstract class Base
    {
        public int ID = new Random().Next(100000, 999999);
        public DateTime DataCadastro = DateTime.Now;  
    }
}