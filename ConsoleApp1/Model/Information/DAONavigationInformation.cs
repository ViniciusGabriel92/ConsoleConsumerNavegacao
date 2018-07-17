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
        public bool Inserir(InformationNavigation obj)
        {
            bool result = false;
            SqlConnection conn = null;
            SqlCommand dbCmd = null;
            SqlTransaction tr = null;

            try
            {
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionSQLServer"].ConnectionString);
                conn = new SqlConnection("Server=DELAVEGA\\JARVIS;Database=Livros;Trusted_Connection=True;MultipleActiveResultSets=true");
                conn.Open();

                tr = conn.BeginTransaction();

                dbCmd = conn.CreateCommand();
                dbCmd.Transaction = tr;
                dbCmd.CommandText = "pr_InformationNavigation_ins";
                dbCmd.CommandType = CommandType.StoredProcedure;

                dbCmd.Parameters.AddWithValue("@descricao", obj.Descricao);
                dbCmd.Parameters.AddWithValue("@data", obj.Data);
                dbCmd.Parameters.AddWithValue("@idOperacao", (int)obj.OPERACAO);

                dbCmd.ExecuteNonQuery();

                tr.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                conn.Close();
                string exc = ex.Message;
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
