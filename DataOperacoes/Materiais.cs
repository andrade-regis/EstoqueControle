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
        private readonly string StringConexão = "@\"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\root\\RG_Estoque.accdb;Persist Security Info=False;";

        private string MensagemErro;

        #region Procedures

        private readonly string Proc_Adicionar = "INSERT INTO Material (Nome, Metrica, Valor, Observacao, DataUltimaAtualizacao, Excluido) " +
                                                 "VALUES (@Nome, @Metrica, @Valor, @Observacao, @DataUltimaAtualizacao, @Excluido)";

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
                    using (OleDbConnection connection = new OleDbConnection(StringConexão))
                    {
                        connection.Open();

                        using (OleDbCommand command = new OleDbCommand(Proc_Adicionar, connection))
                        {
                            command.Parameters.AddWithValue("@Nome", material.Nome);
                            command.Parameters.AddWithValue("@Metrica", material.Metrica);
                            command.Parameters.AddWithValue("@Valor", material.Valor);
                            command.Parameters.AddWithValue("@Observacao", material.Observacao);
                            command.Parameters.AddWithValue("@DataUltimaAtualizacao", material.DataUltimaAtualizacao);
                            command.Parameters.AddWithValue("@Excluido", material.Excluido);
                            command.CommandText += "; SELECT @@IDENTITY";

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
                    using (OleDbConnection connection = new OleDbConnection(StringConexão))
                    {
                        connection.Open();

                        using (OleDbCommand command = new OleDbCommand(Proc_Atualizar, connection))
                        {
                            command.Parameters.AddWithValue("@MaterialId", material.MaterialId);
                            command.Parameters.AddWithValue("@Nome", material.Nome);
                            command.Parameters.AddWithValue("@Metrica", material.Metrica);
                            command.Parameters.AddWithValue("@Valor", material.Valor);
                            command.Parameters.AddWithValue("@Observacao", material.Observacao);
                            command.Parameters.AddWithValue("@DataUltimaAtualizacao", material.DataUltimaAtualizacao);
                            command.Parameters.AddWithValue("@Excluido", material.Excluido);

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
                MensagemErro = string.Empty;
            }
        }

        public void Remover(Material material)
        {
            MensagemErro = string.Empty;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(StringConexão))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand(Proc_Remover, connection))
                    {
                        command.Parameters.AddWithValue("@MaterialId", material.MaterialId);

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
                MensagemErro = string.Empty;
            }
        }

        public Material Carregar()
        {
            MensagemErro = string.Empty;

            try
            {
                if (true)
                {
                    using (OleDbConnection connection = new OleDbConnection(StringConexão))
                    {

                        using (OleDbCommand command = new OleDbCommand(Proc_CarregarTodos, connection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return new Material
                                    {
                                        MaterialId = Convert.ToInt32(reader["MaterialId"]),
                                        Nome = Convert.ToString(reader["Nome"]),
                                        Metrica = Convert.ToString(reader["Metrica"]),
                                        Valor = Convert.ToDouble(reader["Valor"]),
                                        Observacao = Convert.ToString(reader["Observacao"]),
                                        DataUltimaAtualizacao = Convert.ToDateTime(reader["DataUltimaAtualizacao"]),
                                        Excluido = Convert.ToBoolean(reader["Excluido"])
                                    };
                                }
                            }
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                MensagemErro = string.Empty;
            }
        }

        public Material CarregarPor_MaterialId(int materialId)
        {
            MensagemErro = string.Empty;

            try
            {
                if (true)
                {
                    using (OleDbConnection connection = new OleDbConnection(StringConexão))
                    {

                        using (OleDbCommand command = new OleDbCommand(Proc_CarregarPor_Id, connection))
                        {
                            command.Parameters.AddWithValue("@MaterialId", materialId);

                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return new Material
                                    {
                                        MaterialId = Convert.ToInt32(reader["MaterialId"]),
                                        Nome = Convert.ToString(reader["Nome"]),
                                        Metrica = Convert.ToString(reader["Metrica"]),
                                        Valor = Convert.ToDouble(reader["Valor"]),
                                        Observacao = Convert.ToString(reader["Observacao"]),
                                        DataUltimaAtualizacao = Convert.ToDateTime(reader["DataUltimaAtualizacao"]),
                                        Excluido = Convert.ToBoolean(reader["Excluido"])
                                    };
                                }
                            }
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                MensagemErro = string.Empty;
            }
        }

        #endregion
    }
}
