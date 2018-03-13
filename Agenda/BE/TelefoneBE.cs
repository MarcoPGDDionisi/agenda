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
    public class TelefoneBE
    {
        public string IdTelefone { set; get; }
        public string IdContato { set; get; }
        public string TipoTelefone { set; get; }
        public string UsoTelefone { set; get; }
        public string NumTelefone { set; get; }
    }

    public class TelefoneBE_SQL
    {
        ConnDAL Conn = new ConnDAL();
        public TelefoneBE Insert_Update(TelefoneBE obj_list)
        {
            {
                List<SqlParameter> lstPar = new List<SqlParameter>();

                obj_list.IdTelefone = (obj_list.IdTelefone == "0" ? Guid.NewGuid().ToString() : obj_list.IdTelefone);
                lstPar.Add(new SqlParameter("@IdTelefone", obj_list.IdTelefone));
                lstPar.Add(new SqlParameter("@IdContato", obj_list.IdContato ?? ""));
                lstPar.Add(new SqlParameter("@TipoTelefone", obj_list.TipoTelefone ?? ""));
                lstPar.Add(new SqlParameter("@UsoTelefone", obj_list.UsoTelefone ?? ""));
                lstPar.Add(new SqlParameter("@NumTelefone", obj_list.NumTelefone ?? ""));

                Conn.ExecSp("Telefone_Insert_Update", lstPar);
                return obj_list;
            }
        }
        public List<TelefoneBE> SelectList(String IdContato)
        {
            {
                List<SqlParameter> lstPar = new List<SqlParameter>();


                StringBuilder Query = new StringBuilder();
                Query.AppendFormat("SELECT IdTelefone, IdContato, TipoTelefone, UsoTelefone, NumTelefone FROM Telefone WHERE IdContato = '{0}'", IdContato);
                DataTable tB = Conn.ExecSql_DataTable(Query.ToString());

                List<TelefoneBE> objList = new List<TelefoneBE>();

                foreach (DataRow rw in tB.Rows)
                {
                    TelefoneBE obj = new TelefoneBE();
                    obj.IdTelefone = rw["IdTelefone"].ToString();
                    obj.IdContato = rw["IdContato"].ToString();
                    obj.TipoTelefone = rw["TipoTelefone"].ToString();
                    obj.UsoTelefone = rw["UsoTelefone"].ToString();
                    obj.NumTelefone = rw["NumTelefone"].ToString();
                    objList.Add(obj);
                }

                return objList;
            }

        }
        public void Delete(String IdTelefone)
        {
            {
                StringBuilder Query = new StringBuilder();
                Query.AppendFormat("DELETE FROM Telefone WHERE IdTelefone = '{0}'", IdTelefone);
                Conn.ExecSql(Query.ToString());
            }
        }
    }
}