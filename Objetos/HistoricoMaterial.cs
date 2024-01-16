using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class HistoricoMaterial
    {
        public int HistoricoMaterialId { get; set; }
        public int MaterialId { get; set; }
        public int Acao { get; set; }
        public int Origem { get; set; }
        public double Valor { get; set; }
        public DateTime DataAcao { get; set; }

        public HistoricoMaterial() 
        { 
            HistoricoMaterialId = 0;
            MaterialId = 0;
            Acao = 0;
            Origem = 0;
            Valor = 0.0;
            DataAcao = DateTime.MinValue;
        }

        ~HistoricoMaterial() 
        {
        }
    }
}
