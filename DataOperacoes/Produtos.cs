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
    internal class ProdutoConexao
    {
        private OleDbConnection connection;
        private string MensagemErro;

        #region Procedures

        private readonly string Proc_Adicionar = "INSERT INTO Produto (Imagem, Nome, Descricao) " +
                                                  "VALUES (@Imagem, @Nome, @Descricao);";

        private readonly string Proc_Atualizar = "UPDATE Produto SET Imagem = @Imagem, Nome = @Nome, Descricao = @Descricao " +
                                                  "WHERE ProdutoId = @ProdutoId";

        private readonly string Proc_Remover = "DELETE FROM Produto WHERE ProdutoId = @ProdutoId";

        private readonly string Proc_CarregarPor_Id = "SELECT * FROM Produto WHERE ProdutoId = @ProdutoId";

        private readonly string Proc_CarregarTodos = "SELECT * FROM Produto";

        #endregion

        #region Métodos

        public void Adicionar(Produto produto)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Adicionar, connection))
                    {
                        command.Parameters.AddWithValue("@Imagem", OleDbType.Binary).Value = produto.Imagem;
                        command.Parameters.AddWithValue("@Nome", OleDbType.VarChar).Value = produto.Nome;
                        command.Parameters.AddWithValue("@Descricao", OleDbType.VarChar).Value = produto.Descricao;

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

        public void Atualizar(Produto produto)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Atualizar, connection))
                    {
                        command.Parameters.AddWithValue("@Imagem", OleDbType.Binary).Value = produto.Imagem;
                        command.Parameters.AddWithValue("@Nome", OleDbType.VarChar).Value = produto.Nome;
                        command.Parameters.AddWithValue("@Descricao", OleDbType.VarChar).Value = produto.Descricao;
                        command.Parameters.AddWithValue("@ProdutoId", OleDbType.Integer).Value = produto.ProdutoId;

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

        public void Remover(Produto produto)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Remover, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoId", produto.ProdutoId);

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

        public List<Produto> Carregar()
        {
            List<Produto> retorno = null;
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
                            DataTable tabelaProduto = new DataTable("Produto");
                            tabelaProduto.Load(reader);

                            retorno = TabelaParaLista(tabelaProduto);
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

        public Produto CarregarPor_ProdutoId(int produtoId)
        {
            Produto retorno = null;
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_CarregarPor_Id, connection))
                    {
                        command.Parameters.AddWithValue("@ProdutoId", produtoId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            DataTable tabelaProduto = new DataTable("Produto");
                            tabelaProduto.Load(reader);

                            if (tabelaProduto.Rows.Count > 0)
                            {
                                retorno = TabelaParaObjeto(tabelaProduto.Rows[0]);
                            }
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

        private Produto TabelaParaObjeto(DataRow Linha)
        {
            Produto retorno = null;

            if (Linha != null)
            {
                retorno = new Produto
                {
                    ProdutoId = Convert.ToInt32(Linha["ProdutoId"].ToString()),
                    Imagem = (byte[])Linha["Imagem"],
                    Nome = Linha["Nome"].ToString(),
                    Descricao = Linha["Descricao"].ToString(),
                    Alterado = false
                };
            }

            return retorno;
        }

        private List<Produto> TabelaParaLista(DataTable Tabela)
        {
            List<Produto> retorno = null;

            if (Tabela.Rows.Count > 0)
            {
                retorno = new List<Produto>();

                for (int contador = 0; contador < Tabela.Rows.Count; contador++)
                {
                    Produto Produto = new Produto
                    {
                        ProdutoId = Convert.ToInt32(Tabela.Rows[contador]["ProdutoId"].ToString()),
                        Imagem = (byte[])Tabela.Rows[contador]["Imagem"],
                        Nome = Tabela.Rows[contador]["Nome"].ToString(),
                        Descricao = Tabela.Rows[contador]["Descricao"].ToString(),
                        Alterado = false
                    };

                    retorno.Add(Produto);
                }
            }

            return retorno;
        }

        #endregion
    }
}
