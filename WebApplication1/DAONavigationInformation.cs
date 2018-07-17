using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp1
{
    public class DAONavigationInformation
    {
        public bool Inserir()
        {
            bool result = false;
            SqlConnection conn = null;
            SqlCommand dbCmd = null;
            SqlTransaction tr = null;

            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                tr = conn.BeginTransaction();

                dbCmd = conn.CreateCommand();
                dbCmd.Transaction = tr;
                dbCmd.CommandText = "pr_Denuncia_ins";
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.Parameters.AddWithValue("@idResponsavel", obj.Responsavel != null && !string.IsNullOrEmpty(obj.Responsavel.IdFacebook) ? obj.Responsavel.IdFacebook : (object)DBNull.Value);
                dbCmd.Parameters.AddWithValue("@IdObjeto", obj.IdObjeto); //IdObjeto Classe
                dbCmd.Parameters.AddWithValue("@Classe", obj.Classe);
                dbCmd.Parameters.AddWithValue("@Tipo", obj.Tipo);

                dbCmd.ExecuteNonQuery();

                tr.Commit();
                result = true;
            }
            catch (Exception)
            {
                tr.Rollback();
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
