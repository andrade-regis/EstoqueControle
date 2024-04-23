using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using EstoqueControle.Objetos;
using System.Collections;

namespace EstoqueControle.DataOperacoes
{
    internal class Materiais
    {
        private OleDbConnection connection;
        private string MensagemErro;

        #region Procedures

        private readonly string Proc_Adicionar = "INSERT INTO Material (Nome, Metrica, Valor, Observacao, DataUltimaAtualizacao, Excluido) " +
                                                 "VALUES (@Nome, @Metrica, @Valor, @Observacao, @DataUltimaAtualizacao, @Excluido);";

        private readonly string Proc_Atualizar = "UPDATE Material SET Nome = @Nome, Metrica = @Metrica, Valor = @Valor, " +
                                                 "Observacao = @Observacao, DataUltimaAtualizacao = @DataUltimaAtualizacao, " +
                                                 "Excluido = @Excluido WHERE MaterialId = @MaterialId";

        private readonly string Proc_Remover = "UPDATE Material SET Excluido = @Excluido WHERE MaterialId = @MaterialId";

        private readonly string Proc_CarregarPor_Id = "SELECT * FROM Material WHERE MaterialId = @MaterialId";

        private readonly string Proc_CarregarTodos = "SELECT * FROM Material";

        #endregion

        #region Métodos

        public void Adicionar(Material material)
        {
            MensagemErro = string.Empty;

            try
            {
                if (true)
                {
                    using (connection = new OleDbConnection(DadosApp.StringConexao))
                    {
                        connection.Open();

                        using (OleDbCommand command = new OleDbCommand(Proc_Adicionar, connection))
                        {

                            command.Parameters.Add("@Nome", OleDbType.VarChar).Value = material.Nome;
                            command.Parameters.Add("@Metrica", OleDbType.VarChar).Value = material.Metrica;
                            command.Parameters.Add("@Valor", OleDbType.Double).Value = material.Valor;
                            command.Parameters.Add("@Observacao", OleDbType.VarChar).Value = material.Observacao;
                            command.Parameters.Add("@DataUltimaAtualizacao", OleDbType.Date).Value = material.DataUltimaAtualizacao;
                            command.Parameters.Add("@Excluido", OleDbType.Boolean).Value = material.Excluido;

                            command.ExecuteNonQuery();

                            command.CommandText = "SELECT MAX(MaterialId) AS MaterialId FROM Material";
                            material.MaterialId = Convert.ToInt32(command.ExecuteScalar());
                        }
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

        public void Atualizar(Material material)
        {
            MensagemErro = string.Empty;

            try
            {
                if (true)
                {
                    using (connection = new OleDbConnection(DadosApp.StringConexao))
                    {
                        connection.Open();

                        using (OleDbCommand command = new OleDbCommand(Proc_Atualizar, connection))
                        {
                            command.Parameters.Add("@Nome", OleDbType.VarChar).Value = material.Nome;
                            command.Parameters.Add("@Metrica", OleDbType.VarChar).Value = material.Metrica;
                            command.Parameters.Add("@Valor", OleDbType.Double).Value = material.Valor;
                            command.Parameters.Add("@Observacao", OleDbType.VarChar).Value = material.Observacao;
                            command.Parameters.Add("@DataUltimaAtualizacao", OleDbType.Date).Value = material.DataUltimaAtualizacao;
                            command.Parameters.Add("@Excluido", OleDbType.Boolean).Value = material.Excluido;
                            command.Parameters.Add("@MaterialId", OleDbType.Integer).Value = material.MaterialId;

                            command.ExecuteNonQuery();
                        }
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

        public void Remover(Material material)
        {
            MensagemErro = string.Empty;

            try
            {
                using (connection = new OleDbConnection(DadosApp.StringConexao))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Remover, connection))
                    {
                        command.Parameters.Add("@Excluido", OleDbType.Boolean).Value = true;
                        command.Parameters.Add("@MaterialId", OleDbType.Integer).Value = material.MaterialId;

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

        public List<Material> Carregar()
        {
            List<Material> retorno = null;
            MensagemErro = string.Empty;

            try
            {
                if (true)
                {
                    using (connection = new OleDbConnection(DadosApp.StringConexao))
                    {
                        connection.Open();

                        using (OleDbCommand command = new OleDbCommand(Proc_CarregarTodos, connection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                DataTable TabelaMateriais = new DataTable("Materiais");
                                TabelaMateriais.Load(reader);

                                retorno = TabelaParaLista(TabelaMateriais);
                            }
                        }
                    }

                    return retorno;
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

        public Material CarregarPor_MaterialId(int materialId)
        {
            Material retorno = null;
            MensagemErro = string.Empty;

            try
            {
                if (true)
                {
                    using (connection = new OleDbConnection(DadosApp.StringConexao))
                    {
                        connection.Open();

                        using (OleDbCommand command = new OleDbCommand(Proc_CarregarPor_Id, connection))
                        {
                            command.Parameters.AddWithValue("@MaterialId", materialId);

                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                DataTable TabelaMateriais = new DataTable("Materiais");
                                TabelaMateriais.Load(reader);

                                if (TabelaMateriais.Rows.Count > 0)
                                {
                                    retorno = TabelaParaObjeto(TabelaMateriais.Rows[0]);
                                }
                            }
                        }
                    }

                    return retorno;
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

        private Material TabelaParaObjeto(DataRow Linha)
        {
            Material retorno = null;

            if (Linha != null)
            {
                retorno = new Material
                {
                    MaterialId = Convert.ToInt32(Linha["MaterialId"].ToString()),
                    Nome = Linha["Nome"].ToString(),
                    Metrica = Linha["Metrica"].ToString(),
                    Valor = Convert.ToDecimal(Linha["Valor"].ToString()),
                    Observacao = Linha["Observacao"].ToString(),
                    DataUltimaAtualizacao = Convert.ToDateTime(Linha["DataUltimaAtualizacao"].ToString()),
                    Excluido = Convert.ToBoolean(Linha["Excluido"].ToString()),
                    Alterado = false
                };
            }

            return retorno;
        }

        private List<Material> TabelaParaLista(DataTable Tabela)
        {
            List<Material> retorno = null;

            if (Tabela.Rows.Count > 0)
            {
                retorno = new List<Material>();

                for (int contador = 0; contador < Tabela.Rows.Count; contador++)
                {
                    Material Material = new Material
                    {
                        MaterialId = Convert.ToInt32(Tabela.Rows[contador]["MaterialId"].ToString()),
                        Nome = Tabela.Rows[contador]["Nome"].ToString(),
                        Metrica = Tabela.Rows[contador]["Metrica"].ToString(),
                        Valor = Convert.ToDecimal(Tabela.Rows[contador]["Valor"].ToString()),
                        Observacao = Tabela.Rows[contador]["Observacao"].ToString(),
                        DataUltimaAtualizacao = Convert.ToDateTime(Tabela.Rows[contador]["DataUltimaAtualizacao"].ToString()),
                        Excluido = Convert.ToBoolean(Tabela.Rows[contador]["Excluido"].ToString()),
                        Alterado = false
                    };

                    retorno.Add(Material);
                }
            }

            return retorno;
        }

        #endregion
    }
}
