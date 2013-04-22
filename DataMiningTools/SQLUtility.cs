using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;

namespace DataMiningTools
{
    public class SQLUtility
    {
        public event EventHandler<DataMiningTools.MainForm.ProgressEventArgs> onProgressRecv;

        private string connectionString;
        private int logsCount;

        private void ShowProgress(int progress, int total)
        {
            if (onProgressRecv != null)
                onProgressRecv(this, new DataMiningTools.MainForm.ProgressEventArgs
                {
                    Progress = Convert.ToInt32(Convert.ToDouble(progress) / Convert.ToDouble(total) * 100)
                });
        }

        public SQLUtility()
        {
            connectionString = string.Format("Data source={0};Initial Catalog={1};User ID={2};Password={3}",
                                                Properties.Settings.Default.HOSTNAME,
                                                Properties.Settings.Default.DATABASE,
                                                Properties.Settings.Default.USERNAME,
                                                Properties.Settings.Default.PASSWORD
                                                );
        }

        public void InsertLogToDB(List<DataMiningTools.ImportData.Log> logs)
        {
            try
            {
                DataTable dt = new DataTable();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(DataMiningTools.ImportData.Log));
                foreach (PropertyDescriptor property in properties)
                    dt.Columns.Add(property.Name, property.PropertyType);
                object[] values = new object[properties.Count];
                foreach (DataMiningTools.ImportData.Log log in logs)
                {
                    for (int i = 0; i < properties.Count; i++)
                        values[i] = properties[i].GetValue(log);
                    dt.Rows.Add(values);
                }

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans) 
                                                      {
                                                          DestinationTableName = "Logs",
                                                          NotifyAfter = 5000
                                                      })
                    {
                        logsCount = logs.Count;
                        foreach (PropertyDescriptor property in properties)
                            bulkCopy.ColumnMappings.Add(property.Name, property.Name);
                        bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(bulkCopy_SqlRowsCopied);
                        bulkCopy.WriteToServer(dt);
                    }

                    trans.Commit();

                    // Delete duplicate data.
                    SqlCommand cmd = new SqlCommand("DELETE FROM Logs WHERE id NOT IN(SELECT Min(id) FROM Logs GROUP BY Time, Protocol)", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Data duplicate!");
            }
        }

        protected void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            ShowProgress(Convert.ToInt32(e.RowsCopied), logsCount);
        }
        public DataView GetResult(string command)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command, conn);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                conn.Close();
                return ds.Tables.Count > 0 ? ds.Tables[0].DefaultView : null;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
        }
        public void RunCommand(string command)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand sqlcmd = new SqlCommand(command, conn);
                    conn.Open();
                    sqlcmd.CommandTimeout = 300;
                    sqlcmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }
        }
    }
}
