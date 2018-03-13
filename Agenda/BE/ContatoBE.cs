using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Agenda.Models.DAL;
using System.Text;

namespace Agenda.BE
{
    public class ContatoBE
    {
        public string IdContato { set; get; }
        public string Contato { set; get; }
        public string Empresa { set; get; }
        public string Endereco { set; get; }

    }
    public class ContatoBE_SQL
    {
        ConnDAL Conn = new ConnDAL();
        public ContatoBE Insert_Update(ContatoBE obj_list)
        {
            {
                List<SqlParameter> lstPar = new List<SqlParameter>();

                obj_list.IdContato = (obj_list.IdContato == "0" ? Guid.NewGuid().ToString() : obj_list.IdContato);
                lstPar.Add(new SqlParameter("@IdContato", obj_list.IdContato));
                lstPar.Add(new SqlParameter("@Contato", obj_list.Contato ?? ""));
                lstPar.Add(new SqlParameter("@Empresa", obj_list.Empresa ?? ""));
                lstPar.Add(new SqlParameter("@Endereco", obj_list.Endereco ?? ""));

                Conn.ExecSp("Contato_Insert_Update", lstPar);
                return obj_list;
            }

        }
        public List<ContatoBE> SelectList()
        {
            {
                StringBuilder Query = new StringBuilder();
                Query.AppendFormat("SELECT [IdContato], [Contato] ,[Empresa] ,[Endereco] FROM [acesso].[dbo].[Contato] ORDER BY [Contato]");
                DataTable tB = Conn.ExecSql_DataTable(Query.ToString());

                List<ContatoBE> objList = new List<ContatoBE>();

                foreach (DataRow rw in tB.Rows)
                {
                    ContatoBE obj = new ContatoBE();
                    obj.IdContato = rw["IdContato"].ToString();
                    obj.Contato = rw["Contato"].ToString();
                    obj.Empresa = rw["Empresa"].ToString();
                    obj.Endereco = rw["Endereco"].ToString();

                    objList.Add(obj);
                }

                return objList;
            }

        }
        public void Delete(string IdContato)
        {
            {
                List<SqlParameter> lstPar = new List<SqlParameter>();
                lstPar.Add(new SqlParameter("@IdContato", IdContato));

                Conn.ExecSp("Contato_Delete", lstPar);
                return;
            }

        }

    }
}


