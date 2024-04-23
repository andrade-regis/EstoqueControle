using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class Produto
    {
        private bool _alterado;
        private int _produtoId;
        private byte[] _imagem;
        private string _nome;
        private string _descricao;

        public bool Alterado
        {
            get { return _alterado; }
            set { _alterado = value; }
        }

        public int ProdutoId
        {
            get { return _produtoId; }
            set { _produtoId = value; }
        }

        public byte[] Imagem
        {
            get { return _imagem; }
            set { _alterado = true; 
                  _imagem = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _alterado = true;
                  _nome = value; }
        }

        public string Descricao
        {
            get { return _descricao;}
            set { _alterado = true;
                  _descricao = value; }
        }

        public Produto()
        {
            _alterado = false;
            _produtoId = 0;
            _imagem = new byte[0];
            _nome = string.Empty;
            _descricao = string.Empty;
        }

        ~Produto()
        {
            Imagem = null;
        }
    }
}
