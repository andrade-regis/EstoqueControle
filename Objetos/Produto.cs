using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public byte[] Imagem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Produto()
        {
            ProdutoId = 0;
            Imagem = new byte[0];
            Nome = string.Empty;
            Descricao = string.Empty;
        }

        ~Produto()
        {
            Imagem = null;
        }
    }
}
