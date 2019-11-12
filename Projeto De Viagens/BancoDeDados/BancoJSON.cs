using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Projeto_De_Viagens.BancoDeDados
{
    public class BancoJSON
    {
        public string NomeArquivo { get; set; } = $@"{Directory.GetCurrentDirectory()}\base.json";
    }
}
