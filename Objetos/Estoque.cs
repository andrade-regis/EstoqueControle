using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class Estoque
    {
        private bool _alterado;
        private int _estoqueId;
        private int _materialId;
        private double _quantidade;

        public bool Alterado
        {
            get { return _alterado; }
            set { _alterado = value; }
        }

        public int EstoqueId
        {
            get { return _estoqueId; }
            set { _alterado = true;
                  _estoqueId = value; }
        }

        public int MaterialId
        {
            get { return _materialId; }
            set { _alterado = true;
                  _materialId = value; }
        }

        public double Quantidade
        {
            get { return _quantidade; }
            set { _alterado = true;
                  _quantidade = value; }
        }

        public Estoque() 
        {
            _alterado = false;
            _estoqueId = 0;
            _materialId = 0;
            _quantidade = 0.0;
        }

        ~Estoque() 
        {
        }
    }
}
