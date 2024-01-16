using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class Estoque
    {
        public int EstoqueId { get; set; }
        public int MaterialId { get; set; }
        public double Quantidade { get; set; }

        public Estoque() 
        {
            EstoqueId = 0;
            MaterialId = 0;
            Quantidade = 0.0;
        }

        ~Estoque() 
        {
        }
    }
}
