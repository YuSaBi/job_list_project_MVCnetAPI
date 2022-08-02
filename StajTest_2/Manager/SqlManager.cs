using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using StajTest_2.Models;
using Dapper;

namespace StajTest_2.Manager
{
    public class SqlManager
    {
        public string cs = "Data Source=DESKTOP-SN2L41M;Database=StajProje_1;User Id=testt;Password=QWE123-asd456*;";
        //public string cs = "Data Source=DESKTOP-SN2L41M;Initial Catalog=StajProje_1; Integrated Security=True";// appsetting.json dan çekilecek
        public Response AddUser(User item)
        {
            Response resp = new Response();
            try
            {
                using (var connection= new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("UserName", item.UserName);
                    param.Add("UserPassword", item.UserPassword);

                    resp = connection.Query<Response>("SP_Register", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            //catch(SqlException)
            //{
            //    LogManager log = new LogManager();
            //    log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"),
            //        DateTime.Now + "\n" + "Veritabanı kaynaklı hata bulundu" + "\n");
            //    resp.ResponseMsg = "Veritabanı kaynaklı hata bulundu";
            //    resp.ResponseCode = 300;
            //}
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"), DateTime.Now + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
            }
            return resp;
        }

        public ResponseUID LogIn(User item)
        {
            ResponseUID respUID = new ResponseUID();
            try
            {
                using(var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("UserName", item.UserName);
                    param.Add("UserPassword", item.UserPassword);

                    respUID = connection.Query<ResponseUID>("SP_Login", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"),
                    DateTime.Now + "\n" + "Veritabanı kaynaklı hata bulundu" + "\n");
                respUID.ResponseMsg = "Veritabanı kaynaklı hata bulundu";
                respUID.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"), DateTime.Now + "\n" + ex.Message + "\n");
                respUID.ResponseMsg = ex.Message;// Değiştirilecek
            }
            return respUID;
        }

        public Response AddJob(int UserID, string Baslik, int HarcananSure, string Detay, int CustomerID, int Durum, int PriorityID)
        {
            Response resp = new Response();
            try
            {
                using(var connection=new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserID", UserID);
                    param.Add("@Baslik", Baslik);
                    param.Add("@HarcananSure", HarcananSure);
                    param.Add("@Detay", Detay);
                    param.Add("@CustomerID", CustomerID);
                    param.Add("@DurumID", Durum);
                    param.Add("@PriorityID", PriorityID);

                    resp = connection.Query<Response>("SP_AddJob", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"),
                    DateTime.Now + "\n" + "Veritabanı kaynaklı hata bulundu" + "\n");
                resp.ResponseMsg = "Veritabanı kaynaklı hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"), DateTime.Now + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
            }
            return resp;
        }

        public Response DelJob(int JobID)
        {
            Response resp = new Response();
            try
            {
                using(var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("JobID", JobID);

                    resp=connection.Query<Response>("SP_DelJob", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"),
                    DateTime.Now + "\n" + "Veritabanı kaynaklı hata bulundu" + "\n");
                resp.ResponseMsg = "Veritabanı kaynaklı hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"), DateTime.Now + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
            }
            return resp;
        }

        public Response UserLastLoginUpdate(int UserID)
        {
            Response resp = new Response();
            try
            {
                using (var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserID", UserID);

                    resp = connection.Query<Response>("SP_Users_LastLoginUpdate", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"),
                    DateTime.Now + "\n" + "Veritabanı kaynaklı hata bulundu" + "\n");
                resp.ResponseMsg = "Veritabanı kaynaklı hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"), DateTime.Now + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
            }
            return resp;
        }

        public jobListMaster ViewJobs(int UserID)
        {
            jobListMaster jobListMaster = new jobListMaster();
            try
            {
                using (var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("UserID", UserID);

                    jobListMaster = connection.Query<jobListMaster>("SP_View", param, commandType: CommandType.StoredProcedure).FirstOrDefault();

                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"),
                    DateTime.Now + "\n" + "Veritabanı kaynaklı hata bulundu" + "\n");
                jobListMaster.ResponseMsg = "Veritabanı kaynaklı hata bulundu";
                jobListMaster.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(Path.Combine(Environment.CurrentDirectory, "log.txt"), DateTime.Now + "\n" + ex.Message + "\n");
                jobListMaster.ResponseMsg = ex.Message;// Değiştirilecek
            }
            return jobListMaster;
        }






        /*
        public jobListMaster GetJob(int UserID)
        {
            jobListMaster jobListMaster = new jobListMaster();

            try
            {
                using (var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("UserID", UserID);

                    jobListMaster.Jobslist = connection.Query<Jobs>("SP_View", param, commandType: CommandType.StoredProcedure).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                jobListMaster.ResponseCode = "0";
                jobListMaster.ResponseMsg = ex.Message;
            }

            return jobListMaster;
        }
        */

    }
}
