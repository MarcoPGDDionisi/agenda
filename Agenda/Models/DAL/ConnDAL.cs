using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using System.IO;
namespace Agenda.Models.DAL
{
    public class ConnDAL


    {
        #region Variables
        private SqlConnection _connection = null;
        #endregion

        #region Properties
        /// <summary>
        /// Conexão Sql
        /// </summary>
        private SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor da classe DALHelper.
        /// </summary>
        /// <returns>Seta a String de Conexão na propriedade SqlConnection</returns>
        public ConnDAL()
        {

            //String sConn = @"Data Source = MANU; Initial Catalog = agenda; User ID = agenda; Password = agenda";
            String sConn = ConfigurationManager.AppSettings["KeyConn"];
            this._connection = new SqlConnection(sConn);
        }
        #endregion

        #region Events
        #endregion

        #region Methods

        #region Comando Sql
        /// <summary>
        /// Executa um comando Sql e retorna um DataReader
        /// </summary>
        /// <param name="ComandoSql">Comando Sql a ser executado</param>
        /// <returns>Retorna o resultado do comando em um DataReader</returns>
        public SqlDataReader ExecSql_DataReader(string ComandoSql)
        {
            SqlCommand cmdSql = new SqlCommand();

            try
            {
                cmdSql = new SqlCommand();

                cmdSql.Connection = this._connection;
                cmdSql.CommandText = ComandoSql;
                cmdSql.CommandType = CommandType.Text;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();


                return cmdSql.ExecuteReader();

            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
                cmdSql.Dispose();
            }
        }

        /// <summary>
        /// Executa um comando Sql
        /// </summary>
        /// <param name="ComandoSql">Comando Sql a ser executado</param>
        public void ExecSql(string ComandoSql)
        {
            SqlCommand cmdSql;

            try
            {
                cmdSql = new SqlCommand();

                cmdSql.Connection = this._connection;
                cmdSql.CommandText = ComandoSql;
                cmdSql.CommandType = CommandType.Text;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                cmdSql.ExecuteNonQuery();
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// Executa um comando Sql
        /// </summary>
        /// <param name="ComandoSql">Comando Sql a ser executado</param>
        public dynamic ExecSqlScalar(string ComandoSql)
        {
            SqlCommand cmdSql;
            object back = null;
            try
            {
                cmdSql = new SqlCommand();

                cmdSql.Connection = this._connection;
                cmdSql.CommandText = ComandoSql;
                cmdSql.CommandType = CommandType.Text;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                back = cmdSql.ExecuteScalar();
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
            return back;
        }

        /// <summary>
        /// Executa um comando Sql e retorna um DataSet com o ou os Resultsets
        /// </summary>
        /// <param name="ComandoSql">Comando Sql a ser executado</param>
        /// <returns>Retorna o resultado do comando em um DataSet</returns>
        public DataSet ExecSql_DataSet(string ComandoSql)
        {
            SqlCommand cmdSql;
            DataSet objDataSet;

            try
            {
                cmdSql = new SqlCommand();
                objDataSet = new DataSet();

                cmdSql = new SqlCommand(ComandoSql, this._connection);
                cmdSql.CommandType = CommandType.Text;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdSql);
                da.Fill(objDataSet);

                return objDataSet;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        /// <summary>
        /// Executa um comando Sql e retorna um DataTable
        /// </summary>
        /// <param name="ComandoSql">Comando Sql a ser executado</param>
        /// <returns>Retorna o resultado do comando em um DataTable</returns>
        public DataTable ExecSql_DataTable(string ComandoSql)
        {
            SqlCommand cmdSql;
            DataTable objDataTable;

            try
            {
                cmdSql = new SqlCommand();
                objDataTable = new DataTable();

                cmdSql = new SqlCommand(ComandoSql, this._connection);
                cmdSql.CommandType = CommandType.Text;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdSql);
                da.Fill(objDataTable);

                return objDataTable;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        #endregion

        /// <summary>
        /// Fecha conexão com BD caso esteja aberta.
        /// </summary>
        public void CloseConnection()
        {
            if (this._connection.State == ConnectionState.Open)
            {
                this._connection.Close();
            }
        }


        #region stored procedures
        public DataTable ExecSpDataTable(string spNome, List<SqlParameter> Params)
        {
            SqlCommand cmdSql;
            DataTable objDataTable;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;
                cmdSql.CommandTimeout = 600;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                foreach (SqlParameter item in Params)
                {
                    cmdSql.Parameters.Add(item);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmdSql);

                objDataTable = new DataTable();
                da.Fill(objDataTable);

                return objDataTable;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public DataTable ExecSpDataTable(string spNome)
        {
            SqlCommand cmdSql;
            DataTable objDataTable;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandTimeout = 3600;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdSql);

                objDataTable = new DataTable();
                da.Fill(objDataTable);

                return objDataTable;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public DataSet ExecSpDataSet(string spNome, List<SqlParameter> Params)
        {
            SqlCommand cmdSql;
            DataSet ds;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                foreach (SqlParameter item in Params)
                {
                    cmdSql.Parameters.Add(item);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmdSql);

                ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public XmlDocument ExecSpXML(string spNome, List<SqlParameter> Params)
        {
            SqlCommand cmdSql;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                foreach (SqlParameter item in Params)
                {
                    cmdSql.Parameters.Add(item);
                }

                XmlDocument xmldoc = new XmlDocument();
                XmlReader xmlRead = cmdSql.ExecuteXmlReader();
                xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", "ISO-8859-1", null));
                xmldoc.AppendChild(xmldoc.CreateElement("Root"));
                while (!xmlRead.EOF)
                {
                    XmlNode xmlNodeitem = xmldoc.ReadNode(xmlRead);
                    if (xmlNodeitem != null)
                    {
                        xmldoc.DocumentElement.AppendChild(xmlNodeitem);
                    }
                }
                return xmldoc;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public DataSet ExecSpDataSet(string spNome)
        {
            SqlCommand cmdSql;
            DataSet ds;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandTimeout = 3600;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdSql);

                ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void ExecSp(string spNome, List<SqlParameter> Params)
        {
            SqlCommand cmdSql;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                foreach (SqlParameter item in Params)
                {
                    cmdSql.Parameters.Add(item);
                }

                cmdSql.ExecuteNonQuery();
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void ExecSp(string spNome)
        {
            SqlCommand cmdSql;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                cmdSql.ExecuteNonQuery();
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public string ExecSpEscalar(string spNome, List<SqlParameter> Params)
        {
            SqlCommand cmdSql;

            try
            {
                cmdSql = new SqlCommand(spNome);
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.Connection = Connection;

                if (this._connection.State == ConnectionState.Closed)
                    this._connection.Open();

                foreach (SqlParameter item in Params)
                {
                    cmdSql.Parameters.Add(item);
                }

                return cmdSql.ExecuteScalar().ToString();
            }
            catch (SqlException objErr)
            {
                throw objErr;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        #endregion stored procedures

        #endregion

        #region function validate DataSql

        public string return_date(string d)
        {
            string MyDate;
            if (d.ToString().Equals(""))
            {
                MyDate = "";

            }
            else
            {
                MyDate = Convert.ToDateTime(d).ToShortDateString();
            }

            return MyDate;


        }

        #endregion
    }
}