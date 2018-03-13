using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using Agenda.BE;
using Agenda.Models;
using Agenda.Models.DAL;
using System.Data;

namespace Agenda.Controllers
{
    public class HomeController : Controller
    {
        ConnDAL Conn = new ConnDAL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contato()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Consulta_Telefone()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Consulta_Email()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /**************************************************************************************************
        * Contato_InsertUpdate  JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Contato_InsertUpdate(ContatoBE objList)
        {
            ContatoBE_SQL Sql = new ContatoBE_SQL();
            objList = Sql.Insert_Update(objList);

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }


        /**************************************************************************************************
        * Contato_InsertUpdate  JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Contato_SelectList()
        {
            ContatoBE_SQL Sql = new ContatoBE_SQL();
            List<ContatoBE> objList = new List<ContatoBE>();

            objList = Sql.SelectList();

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
         * Contato_Delete JSON
         * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Contato_Excluir(string IdContato)
        {
            ContatoBE_SQL Sql = new ContatoBE_SQL();
            Sql.Delete(IdContato);

            JsonResult jsonResult = Json(new { IdContato }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
        * Telefone - InsertUpdateDelete  JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Telefone_InsertUpdate(TelefoneBE objList)
        {
            TelefoneBE_SQL Sql = new TelefoneBE_SQL();
            objList = Sql.Insert_Update(objList);

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
        * Telefone Select JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Telefone_SelectList(string IdContato)
        {
            TelefoneBE_SQL Sql = new TelefoneBE_SQL();
            List<TelefoneBE> objList = new List<TelefoneBE>();
            objList = Sql.SelectList(IdContato);

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
         * Telefone_Delete JSON
         * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Telefone_Excluir(string IdTelefone)
        {
            TelefoneBE_SQL Sql = new TelefoneBE_SQL();
            Sql.Delete(IdTelefone);

            JsonResult jsonResult = Json(new { IdTelefone }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
        * Email - InsertUpdateDelete  JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Email_InsertUpdate(EmailBE objList)
        {
            EmailBE_SQL Sql = new EmailBE_SQL();
            objList = Sql.Insert_Update(objList);

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
        * Email Select JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic email_SelectList(string IdContato)
        {
            EmailBE_SQL Sql = new EmailBE_SQL();
            List<EmailBE> objList = new List<EmailBE>();
            objList = Sql.SelectList(IdContato);

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        /**************************************************************************************************
         * Telefone_Delete JSON
         * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic Email_Excluir(string IdEmail)
        {
            EmailBE_SQL Sql = new EmailBE_SQL();
            Sql.Delete(IdEmail);

            JsonResult jsonResult = Json(new { IdEmail }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
        * Telefone Select List JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic ConsultaTelefone_SelectList()
        {
            List<HomeModels> objList = new List<HomeModels>();
            DataTable tB = Conn.ExecSql_DataTable("SELECT * FROM Vw_TelefoneList");
            foreach (DataRow rw in tB.Rows)
            {
                HomeModels obj = new HomeModels();
                obj.IdContato = rw["IdContato"].ToString();
                obj.Contato = rw["Contato"].ToString();
                obj.Empresa = rw["Empresa"].ToString();
                obj.Endereco = rw["Endereco"].ToString();
                obj.IdTelefone = rw["IdTelefone"].ToString();
                obj.TipoTelefone = rw["TipoTelefone"].ToString();
                obj.UsoTelefone = rw["UsoTelefone"].ToString();
                obj.NumTelefone = rw["NumTelefone"].ToString();
                objList.Add(obj);
            }

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /**************************************************************************************************
        * Email Select List JSON
        * ***********************************************************************************************/
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod()]
        public dynamic ConsultaEmail_SelectList()
        {
            List<HomeModels> objList = new List<HomeModels>();
            DataTable tB = Conn.ExecSql_DataTable("SELECT * FROM Vw_EmailList");
            foreach (DataRow rw in tB.Rows)
            {
                HomeModels obj = new HomeModels();
                obj.IdContato = rw["IdContato"].ToString();
                obj.Contato = rw["Contato"].ToString();
                obj.Empresa = rw["Empresa"].ToString();
                obj.Endereco = rw["Endereco"].ToString();
                obj.IdEmail = rw["IdEmail"].ToString();
                obj.Email = rw["Email"].ToString();
                objList.Add(obj);
            }

            JsonResult jsonResult = Json(new { objList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
    }
}