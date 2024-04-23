using EstoqueControle.Objetos;

namespace EstoqueControle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataOperacoes.Materiais NegóciosMateriais = new DataOperacoes.Materiais();
            
            Material material = new Material();
            material.Nome = "Gasolina";
            material.Metrica = "ml";
            material.Valor = 2.50M;
            material.Observacao = "Simplesmente não tem";

            NegóciosMateriais.Adicionar(material);

            material = NegóciosMateriais.CarregarPor_MaterialId(material.MaterialId);

            material.Nome = "Etanol";

            NegóciosMateriais.Atualizar(material);

            NegóciosMateriais.Remover(material);

            List<Material> materiais = NegóciosMateriais.Carregar();

            foreach(Material materia in materiais)
            {
                NegóciosMateriais.Remover(materia);
            }

        }
    }
}
