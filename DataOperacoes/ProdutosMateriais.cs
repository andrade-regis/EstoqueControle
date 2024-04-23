using EstoqueControle.Objetos;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueControle.DataOperacoes
{
    internal class ProdutoMaterialConexao
    {
        private OleDbConnection connection;
        private string MensagemErro;

        #region Procedures

        private readonly string Proc_Adicionar = "INSERT INTO ProdutoMaterial (ProdutoId, MaterialId, QuantidadeNecessaria) " +
                                                  "VALUES (@ProdutoId, @MaterialId, @QuantidadeNecessaria);";

        private readonly string Proc_Atualizar = "UPDATE ProdutoMaterial SET ProdutoId = @ProdutoId, MaterialId = @MaterialId, " +
                                                  "QuantidadeNecessaria = @QuantidadeNecessaria WHERE ProdutoMaterialId = @ProdutoMaterialId";

        private readonly string Proc_Remover = "DELETE FROM ProdutoMaterial WHERE ProdutoMaterialId = @ProdutoMaterialId";

        private readonly string Proc_CarregarPor_Id = "SELECT * FROM ProdutoMaterial WHERE ProdutoMaterialId = @ProdutoMaterialId";

        private readonly string Proc_CarregarTodos = "SELECT * FROM ProdutoMaterial";

        #endregion

        #region Métodos

        public void Adicionar(ProdutoMaterial produtoMaterial)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Adicionar, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoId", OleDbType.Integer).Value = produtoMaterial.ProdutoId;
                        command.Parameters.AddWithValue("@MaterialId", OleDbType.Integer).Value = produtoMaterial.MaterialId;
                        command.Parameters.AddWithValue("@QuantidadeNecessaria", OleDbType.Integer).Value = produtoMaterial.QuantidadeNecessaria;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                MensagemErro = string.Empty;
            }
        }

        public void Atualizar(ProdutoMaterial produtoMaterial)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Atualizar, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoId", OleDbType.Integer).Value = produtoMaterial.ProdutoId;
                        command.Parameters.AddWithValue("@MaterialId", OleDbType.Integer).Value = produtoMaterial.MaterialId;
                        command.Parameters.AddWithValue("@QuantidadeNecessaria", OleDbType.Integer).Value = produtoMaterial.QuantidadeNecessaria;
                        command.Parameters.AddWithValue("@ProdutoMaterialId", OleDbType.Integer).Value = produtoMaterial.ProdutoMaterialId;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                MensagemErro = string.Empty;
            }
        }

        public void Remover(ProdutoMaterial produtoMaterial)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Remover, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoMaterialId", produtoMaterial.ProdutoMaterialId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                MensagemErro = string.Empty;
            }
        }

        public List<ProdutoMaterial> Carregar()
        {
            List<ProdutoMaterial> retorno = null;
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_CarregarTodos, connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            DataTable tabelaProdutoMaterial = new DataTable("ProdutoMaterial");
                            tabelaProdutoMaterial.Load(reader);

                            retorno = TabelaParaLista(tabelaProdutoMaterial);
                        }
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                MensagemErro = string.Empty;
            }
        }

        public ProdutoMaterial CarregarPor_ProdutoMaterialId(int produtoMaterialId)
        {
            ProdutoMaterial retorno = null;
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_CarregarPor_Id, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoMaterialId", produtoMaterialId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            DataTable tabelaProdutoMaterial = new DataTable("ProdutoMaterial");
                            tabelaProdutoMaterial.Load(reader);

                            if (tabelaProdutoMaterial.Rows.Count > 0)
                            {
                                retorno = TabelaParaObjeto(tabelaProdutoMaterial.Rows[0]);
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                MensagemErro = string.Empty;
            }
        }

        private ProdutoMaterial TabelaParaObjeto(DataRow Linha)
        {
            ProdutoMaterial retorno = null;

            if (Linha != null)
            {
                retorno = new ProdutoMaterial
                {
                    ProdutoMaterialId = Convert.ToInt32(Linha["ProdutoMaterialId"].ToString()),
                    ProdutoId = Convert.ToInt32(Linha["ProdutoId"].ToString()),
                    MaterialId = Convert.ToInt32(Linha["MaterialId"].ToString()),
                    QuantidadeNecessaria = Convert.ToInt32(Linha["QuantidadeNecessaria"].ToString()),
                    Alterado = false
                };
            }

            return retorno;
        }

        private List<ProdutoMaterial> TabelaParaLista(DataTable Tabela)
        {
            List<ProdutoMaterial> retorno = null;

            if (Tabela.Rows.Count > 0)
            {
                retorno = new List<ProdutoMaterial>();

                for (int contador = 0; contador < Tabela.Rows.Count; contador++)
                {
                    ProdutoMaterial ProdutoMaterial = new ProdutoMaterial
                    {
                        ProdutoMaterialId = Convert.ToInt32(Tabela.Rows[contador]["ProdutoMaterialId"].ToString()),
                        ProdutoId = Convert.ToInt32(Tabela.Rows[contador]["ProdutoId"].ToString()),
                        MaterialId = Convert.ToInt32(Tabela.Rows[contador]["MaterialId"].ToString()),
                        QuantidadeNecessaria = Convert.ToInt32(Tabela.Rows[contador]["QuantidadeNecessaria"].ToString()),
                        Alterado = false
                    };

                    retorno.Add(ProdutoMaterial);
                }
            }

            return retorno;
        }

        #endregion
    }
}
