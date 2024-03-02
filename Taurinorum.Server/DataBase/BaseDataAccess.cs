using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Taurinorum.Server.DataBase
{
    public abstract class BaseDataAccess
    {

        /// <summary>
        /// Centralizzare richiamo stored
        /// </summary>

        protected string connectionString = string.Empty;
        protected SqlParameter ReturnValueParameter()
        {
            return SqlParameter_Int("ReturnValue", 0, ParameterDirection.InputOutput);
        }
        protected SqlParameter SqlParameter_ReturnMessage()
        {
            return SqlParameter_Varchar("ReturnMessage", "", 255, ParameterDirection.InputOutput);
        }
        protected SqlParameter SqlParameter_Int(string name, int valore, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter pReturnValue = new SqlParameter(name, SqlDbType.Int)
            {
                Value = valore,
                Direction = parameterDirection
            };
            return pReturnValue;
        }
        protected SqlParameter SqlParameter_Varchar(string name, string valore, int size, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter pReturnValue = new SqlParameter(name, SqlDbType.VarChar, size)
            {
                Value = valore,
                Direction = parameterDirection
            };
            return pReturnValue;
        }
        protected string GetParamDieci3k(string sezione, string param, string defaultValue, string connectionString)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand Cmd = new SqlCommand("spUtil_GetValueIni", con);
            try
            {
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter pReturnValue = ReturnValueParameter();
                Cmd.Parameters.Add(pReturnValue);
                Cmd.Parameters.Add(SqlParameter_Varchar("@Section", sezione, 255, ParameterDirection.InputOutput));
                Cmd.Parameters.Add(SqlParameter_Varchar("@Search", param, 255));

                SqlParameter pValue = SqlParameter_Varchar("@Value", "", 255, ParameterDirection.InputOutput);

                con.Open();
                Cmd.ExecuteNonQuery();

                if (!string.IsNullOrEmpty((string)pValue.Value))
                {
                    return (string)pValue.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return defaultValue;
            }
        }
        protected async Task<DataTable?> ReadFromSpAsync(string spName, string? xmlFiltri = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    using SqlConnection con = new SqlConnection(connectionString);
                    StringBuilder sb = new StringBuilder();

                    using SqlCommand Cmd = new SqlCommand(spName, con); 
                    Cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pReturnValue = ReturnValueParameter();
                    Cmd.Parameters.Add(pReturnValue);
                    if (!string.IsNullOrEmpty(xmlFiltri))
                    {
                        Cmd.Parameters.Add(new SqlParameter("@XML",
                            sb.Append(xmlFiltri)
                            .ToString()));
                    }
                    con.Open();

                    var dataReader = await Cmd.ExecuteReaderAsync();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dataReader.Close();
                    return dataTable;
                }
            }
            catch(Exception e)
            {

            }
            
            return null;
        }
    }
}
