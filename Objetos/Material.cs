using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class Material
    {
        public bool Alterado { get; set; }
        public int MaterialId { get; set; }
        public string Nome { get; set; }
        public string Metrica { get; set; }
        public double Valor { get; set; }
        public string Observacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public bool Excluido { get; set; }

        public Material()
        {
            Alterado = false;
            MaterialId = 0;
            Nome = string.Empty;
            Metrica = string.Empty;
            Valor = 0.0;
            Observacao = string.Empty;
            DataUltimaAtualizacao = DateTime.MinValue;
            Excluido = false;
        }

        ~Material()
        {
            Nome = string.Empty;
            Metrica = string.Empty;
            Observacao = string.Empty;
        }
    }
}
