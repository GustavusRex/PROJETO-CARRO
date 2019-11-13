using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projeto_De_Viagens.Operações;

namespace Projeto_De_Viagens.BancoDeDados
{
    public class BancoJSON
    {
        public string Json { get; set; } = $@"{Directory.GetCurrentDirectory()}\base.json";

        public AgenciaDeViagens BuscarDadados()
        {
            if (!File.Exists(Json))
                return null;

            JObject o1 = JObject.Parse(File.ReadAllText(Json));
            AgenciaDeViagens agencia = JsonConvert.DeserializeObject<AgenciaDeViagens>(o1.ToString());
            return agencia;
        }

        public void Salvar(AgenciaDeViagens agencia)
        {
            using (StreamWriter file = File.CreateText(Json))
            {
                string jsonData = JsonConvert.SerializeObject(agencia);
                file.WriteLine(jsonData);
            }
        }
    }
}
