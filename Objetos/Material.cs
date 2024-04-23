using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class Material
    {
        private bool _alterado;
        private int _materialId;
        private string _nome;
        private string _metrica;
        private decimal _valor;
        private string _observacao;
        private DateTime _dataUltimaAtualizacao;
        private bool _excluido;

        public bool Alterado
        {
            get { return _alterado; }
            set { _alterado = value; }
        }

        public int MaterialId
        {
            get { return _materialId; }
            set { _alterado = true;
                  _materialId = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _alterado = true;
                  _nome = value; }
        }

        public string Metrica
        {
            get { return _metrica; }
            set { _alterado = true;
                  _metrica = value; }
        }

        public decimal Valor
        {
            get { return _valor; }
            set { _alterado = true;
                  _valor = value; }
        }

        public string Observacao
        {
            get { return _observacao; }
            set { _alterado = true;
                  _observacao = value; }
        }

        public DateTime DataUltimaAtualizacao
        {
            get { return _dataUltimaAtualizacao; } 
            set { _alterado = true; 
                  _dataUltimaAtualizacao = value; }
        }

        public bool Excluido
        {
            get { return _excluido; }
            set { _alterado = true;
                  _excluido = value; }
        }

        public Material()
        {
            _alterado = false;
            _materialId = 0;
            _nome = string.Empty;
            _metrica = string.Empty;
            _valor = 0;
            _observacao = string.Empty;
            _dataUltimaAtualizacao = DateTime.MinValue;
            _excluido = false;
        }

        ~Material()
        {            
            _nome = string.Empty;
            _metrica = string.Empty;
            _observacao = string.Empty;
        }
    }
}
