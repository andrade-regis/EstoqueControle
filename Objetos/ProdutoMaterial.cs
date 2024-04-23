using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class ProdutoMaterial
    {
        private bool _alterado;
        private int _produtoMaterialId;
        private int _produtoId;
        private int _materialId;
        private int _quantidadeNecessaria;

        public bool Alterado
        {
            get { return _alterado; }
            set { _alterado = value; }
        }

        public int ProdutoMaterialId
        {
            get { return _produtoMaterialId; }
            set { _alterado = true;
                  _produtoMaterialId = value;}
        }

        public int ProdutoId
        {
            get { return _produtoId; }
            set { _alterado = true;
                  _produtoId = value;}
        }

        public int MaterialId
        {
            get { return _materialId; }
            set { _alterado = true;
                  _materialId = value;}
        }

        public int QuantidadeNecessaria
        {
            get { return _quantidadeNecessaria; }
            set { _alterado = true;
                  _quantidadeNecessaria = value;}
        }

        public ProdutoMaterial() 
        {
            _alterado = false;
            _produtoMaterialId = 0;
            _produtoId = 0;
            _materialId = 0;
            _quantidadeNecessaria = 0;
        }

        ~ProdutoMaterial() 
        {
        }
    }
}
