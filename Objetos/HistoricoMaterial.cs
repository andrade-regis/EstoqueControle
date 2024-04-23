using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.Objetos
{
    public class HistoricoMaterial
    {
        private bool _alterado;
        private int _historicoMaterialId;
        private int _materialId;
        private int _acao;
        private int _origem;
        private double _valor;
        private DateTime _dataAcao;

        public bool Alterado
        {
            get { return _alterado; }
            set { _alterado = value; }
        }

        public int HistoricoMaterialId
        {
            get { return _historicoMaterialId; }
            set { _alterado = true;
                  _historicoMaterialId = value; }
        }

        public int MaterialId
        {
            get { return _materialId; }
            set { _alterado = true; 
                  _materialId = value; }
        }

        public int Acao
        {
            get { return _acao; }
            set { _alterado = true; 
                  _acao = value; }
        }

        public int Origem
        {
            get { return _origem; }
            set { _alterado = true;
                _origem = value; }
        }


        public double Valor
        {
            get { return _valor; }
            set { _alterado = true;
                  _valor = value; }
        }

        public DateTime DataAcao
        {
            get { return _dataAcao; }
            set { _alterado = true;
                  _dataAcao = value; }
        }

        public HistoricoMaterial()
        {
            _alterado = false;
            _historicoMaterialId = 0;
            _materialId = 0;
            _acao = 0;
            _origem = 0;
            _valor = 0.0;
            _dataAcao = DateTime.MinValue;
        }

        ~HistoricoMaterial()
        {
        }
    }
}
