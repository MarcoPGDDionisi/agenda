using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agenda.Models.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Agenda.BE
{
    public class EmailBE
    {
        public string IdEmail { set; get; }
        public string IdContato { set; get; }
        public string Email { set; get; }

    }
    public class EmailBE_SQL
    {
        ConnDAL Conn = new ConnDAL();
        public EmailBE Insert_Update(EmailBE obj_list)
        {
            {
                List<SqlParameter> lstPar = new List<SqlParameter>();

                obj_list.IdEmail = (obj_list.IdEmail == "0" ? Guid.NewGuid().ToString() : obj_list.IdEmail);
                lstPar.Add(new SqlParameter("@IdEmail", obj_list.IdEmail));
                lstPar.Add(new SqlParameter("@IdContato", obj_list.IdContato ?? ""));
                lstPar.Add(new SqlParameter("@Email", obj_list.Email ?? ""));

                Conn.ExecSp("Email_Insert_Update", lstPar);
                return obj_list;
            }
        }
            public List<EmailBE> SelectList(String IdContato)
            {
                {
                    List<SqlParameter> lstPar = new List<SqlParameter>();


                    StringBuilder Query = new StringBuilder();
                    Query.AppendFormat("SELECT [IdEmail], [IdContato], [Email] FROM Email WHERE IdContato = '{0}'", IdContato);
                    DataTable tB = Conn.ExecSql_DataTable(Query.ToString());

                    List<EmailBE> objList = new List<EmailBE>();

                    foreach (DataRow rw in tB.Rows)
                    {
                        EmailBE obj = new EmailBE();
                        obj.IdEmail = rw["IdEmail"].ToString();
                        obj.IdContato = rw["IdContato"].ToString();
                        obj.Email = rw["Email"].ToString();

                        objList.Add(obj);
                    }

                    return objList;
                }

            }
        public void Delete(String IdEmail)
        {
            {
                StringBuilder Query = new StringBuilder();
                Query.AppendFormat("DELETE FROM Email WHERE IdEmail = '{0}'", IdEmail);
                Conn.ExecSql(Query.ToString());
            }
        }
    }


    }