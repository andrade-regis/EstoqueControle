using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class ProdutoMaterial
    {
        public int ProdutoMaterialId { get; set; }
        public int ProdutoId { get; set; }
        public int MaterialId { get; set; }
        public int QuantidadeNecessaria { get; set; }

        public ProdutoMaterial() 
        {
            ProdutoMaterialId = 0;
            ProdutoId = 0;
            MaterialId = 0;
            QuantidadeNecessaria = 0;
        }

        ~ProdutoMaterial() 
        {
        }
    }
}
