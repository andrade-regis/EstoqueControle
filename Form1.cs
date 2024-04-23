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
            DataOperacoes.Materiais Neg�ciosMateriais = new DataOperacoes.Materiais();
            
            Material material = new Material();
            material.Nome = "Gasolina";
            material.Metrica = "ml";
            material.Valor = 2.50M;
            material.Observacao = "Simplesmente n�o tem";

            Neg�ciosMateriais.Adicionar(material);

            material = Neg�ciosMateriais.CarregarPor_MaterialId(material.MaterialId);

            material.Nome = "Etanol";

            Neg�ciosMateriais.Atualizar(material);

            Neg�ciosMateriais.Remover(material);

            List<Material> materiais = Neg�ciosMateriais.Carregar();

            foreach(Material materia in materiais)
            {
                Neg�ciosMateriais.Remover(materia);
            }

        }
    }
}
