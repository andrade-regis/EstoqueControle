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
    internal class HistoricoMaterialConexao
    {
        private OleDbConnection connection;
        private string MensagemErro;

        #region Procedures

        private readonly string Proc_Adicionar = "INSERT INTO HistoricoMaterial (MaterialId, Acao, Origem, Valor, DataAcao) " +
                                                  "VALUES (@MaterialId, @Acao, @Origem, @Valor, @DataAcao);";

        private readonly string Proc_Atualizar = "UPDATE HistoricoMaterial SET MaterialId = @MaterialId, Acao = @Acao, " +
                                                  "Origem = @Origem, Valor = @Valor, DataAcao = @DataAcao " +
                                                  "WHERE HistoricoMaterialId = @HistoricoMaterialId";

        private readonly string Proc_Remover = "DELETE FROM HistoricoMaterial WHERE HistoricoMaterialId = @HistoricoMaterialId";

        private readonly string Proc_CarregarPor_Id = "SELECT * FROM HistoricoMaterial WHERE HistoricoMaterialId = @HistoricoMaterialId";

        private readonly string Proc_CarregarTodos = "SELECT * FROM HistoricoMaterial";

        #endregion

        #region Métodos

        public void Adicionar(HistoricoMaterial historicoMaterial)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Adicionar, connection))
                    {
                        command.Parameters.AddWithValue("@MaterialId", OleDbType.Integer).Value = historicoMaterial.MaterialId;
                        command.Parameters.AddWithValue("@Acao", OleDbType.Integer).Value = historicoMaterial.Acao;
                        command.Parameters.AddWithValue("@Origem", OleDbType.VarChar).Value = historicoMaterial.Origem;
                        command.Parameters.AddWithValue("@Valor", OleDbType.Double).Value = historicoMaterial.Valor;
                        command.Parameters.AddWithValue("@DataAcao", OleDbType.Date).Value = historicoMaterial.DataAcao;

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

        public void Atualizar(HistoricoMaterial historicoMaterial)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Atualizar, connection))
                    {
                        command.Parameters.AddWithValue("@MaterialId", OleDbType.Integer).Value = historicoMaterial.MaterialId;
                        command.Parameters.AddWithValue("@Acao", OleDbType.Integer).Value = historicoMaterial.Acao;
                        command.Parameters.AddWithValue("@Origem", OleDbType.VarChar).Value = historicoMaterial.Origem;
                        command.Parameters.AddWithValue("@Valor", OleDbType.Double).Value = historicoMaterial.Valor;
                        command.Parameters.AddWithValue("@DataAcao", OleDbType.Date).Value = historicoMaterial.DataAcao;
                        command.Parameters.AddWithValue("@HistoricoMaterialId", OleDbType.Integer).Value = historicoMaterial.HistoricoMaterialId;

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

        public void Remover(HistoricoMaterial historicoMaterial)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Remover, connection))
                    {
                        command.Parameters.AddWithValue("@HistoricoMaterialId", historicoMaterial.HistoricoMaterialId);

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

        public List<HistoricoMaterial> Carregar()
        {
            List<HistoricoMaterial> retorno = null;
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
                            DataTable tabelaHistoricoMaterial = new DataTable("HistoricoMaterial");
                            tabelaHistoricoMaterial.Load(reader);

                            retorno = TabelaParaLista(tabelaHistoricoMaterial);
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

        public HistoricoMaterial CarregarPor_HistoricoMaterialId(int historicoMaterialId)
        {
            HistoricoMaterial retorno = null;
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_CarregarPor_Id, connection))
                    {
                        command.Parameters.AddWithValue("@HistoricoMaterialId", historicoMaterialId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            DataTable tabelaHistoricoMaterial = new DataTable("HistoricoMaterial");
                            tabelaHistoricoMaterial.Load(reader);

                            if (tabelaHistoricoMaterial.Rows.Count > 0)
                            {
                                retorno = TabelaParaObjeto(tabelaHistoricoMaterial.Rows[0]);
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
        }

        private HistoricoMaterial TabelaParaObjeto(DataRow Linha)
        {
            HistoricoMaterial retorno = null;

            if (Linha != null)
            {
                retorno = new HistoricoMaterial
                {
                    HistoricoMaterialId = Convert.ToInt32(Linha["HistoricoMaterialId"].ToString()),
                    MaterialId = Convert.ToInt32(Linha["MaterialId"].ToString()),
                    Acao = Convert.ToInt32(Linha["Acao"].ToString()),
                    Origem = Convert.ToInt32(Linha["Origem"].ToString()),
                    Valor = Convert.ToDouble(Linha["Valor"].ToString()),
                    DataAcao = Convert.ToDateTime(Linha["DataAcao"].ToString()),
                    Alterado = false
                };
            }

            return retorno;
        }

        private List<HistoricoMaterial> TabelaParaLista(DataTable Tabela)
        {
            List<HistoricoMaterial> retorno = null;

            if (Tabela.Rows.Count > 0)
            {
                retorno = new List<HistoricoMaterial>();

                for (int contador = 0; contador < Tabela.Rows.Count; contador++)
                {
                    HistoricoMaterial historico = new HistoricoMaterial
                    {
                        HistoricoMaterialId = Convert.ToInt32(Tabela.Rows[contador]["HistoricoMaterialId"].ToString()),
                        MaterialId = Convert.ToInt32(Tabela.Rows[contador]["MaterialId"].ToString()),
                        Acao = Convert.ToInt32(Tabela.Rows[contador]["Acao"].ToString()),
                        Origem = Convert.ToInt32(Tabela.Rows[contador]["Origem"].ToString()),
                        Valor = Convert.ToDouble(Tabela.Rows[contador]["Valor"].ToString()),
                        DataAcao = Convert.ToDateTime(Tabela.Rows[contador]["DataAcao"].ToString()),
                        Alterado = false
                    };

                    retorno.Add(historico);
                }
            }

            return retorno;
        }

        #endregion
    }
}
