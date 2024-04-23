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
    internal class EstoqueConexao
    {
        private OleDbConnection connection;
        private string MensagemErro;

        #region Procedures

        private readonly string Proc_Adicionar = "INSERT INTO Estoque (MaterialId, Quantidade) VALUES (@MaterialId, @Quantidade);";

        private readonly string Proc_Atualizar = "UPDATE Estoque SET MaterialId = @MaterialId, Quantidade = @Quantidade WHERE EstoqueId = @EstoqueId";

        private readonly string Proc_Remover = "DELETE FROM Estoque WHERE EstoqueId = @EstoqueId";

        private readonly string Proc_CarregarPor_Id = "SELECT * FROM Estoque WHERE EstoqueId = @EstoqueId";

        private readonly string Proc_CarregarTodos = "SELECT * FROM Estoque";

        #endregion

        #region Métodos

        public void Adicionar(Estoque estoque)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Adicionar, connection))
                    {
                        command.Parameters.AddWithValue("@MaterialId", OleDbType.Integer).Value = estoque.MaterialId;
                        command.Parameters.AddWithValue("@Quantidade", OleDbType.Integer).Value = estoque.Quantidade;

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

        public void Atualizar(Estoque estoque)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Atualizar, connection))
                    {
                        command.Parameters.AddWithValue("@MaterialId", OleDbType.Integer).Value = estoque.MaterialId;
                        command.Parameters.AddWithValue("@Quantidade", OleDbType.Integer).Value = estoque.Quantidade;
                        command.Parameters.AddWithValue("@EstoqueId", OleDbType.Integer).Value = estoque.EstoqueId;

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

        public void Remover(Estoque estoque)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Remover, connection))
                    {
                        command.Parameters.AddWithValue("@EstoqueId", estoque.EstoqueId);

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

        public List<Estoque> Carregar()
        {
            List<Estoque> retorno = null;
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
                            DataTable tabelaEstoque = new DataTable("Estoque");
                            tabelaEstoque.Load(reader);

                            retorno = TabelaParaLista(tabelaEstoque);
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

        public Estoque CarregarPor_EstoqueId(int estoqueId)
        {
            Estoque retorno = null;
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_CarregarPor_Id, connection))
                    {
                        command.Parameters.AddWithValue("@EstoqueId", estoqueId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            DataTable tabelaEstoque = new DataTable("Estoque");
                            tabelaEstoque.Load(reader);

                            if (tabelaEstoque.Rows.Count > 0)
                            {
                                retorno = TabelaParaObjeto(tabelaEstoque.Rows[0]);
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

        private Estoque TabelaParaObjeto(DataRow Linha)
        {
            Estoque estoque = null;

            if (Linha != null)
            {
                estoque = new Estoque()
                {
                    EstoqueId = Convert.ToInt32(Linha["EstoqueId"].ToString()),
                    MaterialId = Convert.ToInt32(Linha["MaterialId"].ToString()),
                    Quantidade = Convert.ToDouble(Linha["Quantidade"].ToString()),
                    Alterado = false
                };
            }

            return estoque;
        }

        private List<Estoque> TabelaParaLista(DataTable Tabela)
        {
            List<Estoque> retorno = null;

            if (Tabela.Rows.Count > 0)
            {
                retorno = new List<Estoque>();

                for (int contador = 0; contador < Tabela.Rows.Count; contador++)
                {
                    Estoque estoque = new Estoque
                    {
                        EstoqueId = Convert.ToInt32(Tabela.Rows[contador]["EstoqueId"].ToString()),
                        MaterialId = Convert.ToInt32(Tabela.Rows[contador]["MaterialId"].ToString()),
                        Quantidade = Convert.ToDouble(Tabela.Rows[contador]["Quantidade"].ToString()),
                        Alterado = false
                    };

                    retorno.Add(estoque);
                }
            }

            return retorno;
        }

        #endregion


    }
}
