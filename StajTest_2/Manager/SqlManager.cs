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
        private readonly IConfiguration _configuration;
        public string cs;
        public string logFileName;
        public string logDbMessage;
        public string path;

        public SqlManager(IConfiguration config)
        {
            _configuration = config;
            cs = _configuration["ConnectionStrings:SqlDB_1"];
            logFileName = _configuration["LogManager:fileName"];// "log.txt"
            logDbMessage = _configuration["LogManager:dbMessage"];// "Veritabani kaynakli hata bulundu"
            path = Path.Combine(Environment.CurrentDirectory, "log.txt");
        }
        //public string cs = "Data Source=DESKTOP-SN2L41M;Initial Catalog=StajProje_1; Integrated Security=True";// appsetting.json dan çekilecek

        public string TestLogIn(User item)
        {
            ResponseUID respUID = new ResponseUID();
            try
            {
                using (var connection = new SqlConnection(cs))
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
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                respUID.ResponseMsg = "Veritabani kaynakli hata bulundu";
                respUID.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                respUID.ResponseMsg = ex.Message;// Değiştirilecek
                respUID.ResponseCode = 299;
            }
            return respUID.ResponseMsg;
        }

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
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(path,DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                resp.ResponseMsg = "Veritabani kaynakli hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
                resp.ResponseCode = 299;
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
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                respUID.ResponseMsg = "Veritabani kaynakli hata bulundu";
                respUID.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                respUID.ResponseMsg = ex.Message;// Değiştirilecek
                respUID.ResponseCode = 299;
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
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                resp.ResponseMsg = "Veritabani kaynakli hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
                resp.ResponseCode = 299;
            }
            return resp;
        }

        public int EditJob(int JobID, string Baslik, int HarcananSure, string Detay, int CustomerID, int Durum, int PriorityID)
        {
            int resp;
            try
            {
                using (var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("@JobID", JobID);
                    param.Add("@Baslik", Baslik);
                    param.Add("@HarcananSure", HarcananSure);
                    param.Add("@Detay", Detay);
                    param.Add("@CustomerID", CustomerID);
                    param.Add("@DurumID", Durum);
                    param.Add("@PriorityID", PriorityID);

                    resp = connection.Query<int>("SP_EditJob", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                resp = 239;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                resp = 209;
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
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                resp.ResponseMsg = "Veritabani kaynakli hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
                resp.ResponseCode = 299;
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
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                resp.ResponseMsg = "Veritabani kaynakli hata bulundu";
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                resp.ResponseMsg = ex.Message;// Değiştirilecek
                resp.ResponseCode = 299;
            }
            return resp;
        }

        public jobListMaster ViewJobs(int UserID)
        {
            jobListMaster jobListMaster = new jobListMaster();
            try
            {
                using (SqlConnection Con = new SqlConnection(cs))// SqlConnection bulamazsa SqlClient Expension yüklenmeli
                {
                    SqlCommand cmd = new SqlCommand("SP_View", Con);// procedure ismi ve bağlantı girildi
                    cmd.CommandType = CommandType.StoredProcedure;// bir procedure oldugu için bunu SP olarak tipini belirliyoruz
                    cmd.Parameters.AddWithValue("UserID", UserID);// oluşturulan parametre sql komutuna gönderiliyor

                    Con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())// okuduğumuz verileri oluşturduğumuz değişkene atıyoruz
                    {
                        if (Convert.ToInt32(dr["ResponseCode"]) != 303)// veritabanından 303 dönerse kayıt bulunamadı demek
                        {
                            Jobs job = new Jobs();// tekli veri
                            job.Id = Convert.ToInt32(dr["Id"]);
                            job.Baslik = dr["Baslik"].ToString();
                            job.Detay = dr["Detay"].ToString();
                            job.Gun = Convert.ToDateTime(dr["Gun"]);
                            job.HarcananSure = Convert.ToInt32(dr["HarcananSure"]);
                            job.Musteri = dr["CustomerName"].ToString();
                            job.Durum = dr["DurumAd"].ToString();
                            job.Oncelik = dr["PriorityName"].ToString();
                            jobListMaster.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                            jobListMaster.ResponseMsg = dr["ResponseMsg"].ToString();
                            jobListMaster.Jobslist.Add(job);
                        }
                        else
                        {
                            jobListMaster.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                            jobListMaster.ResponseMsg = dr["ResponseMsg"].ToString();
                        }
                    }
                    dr.Close();
                    Con.Close();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                jobListMaster.ResponseMsg = "Veritabani kaynakli hata bulundu";
                jobListMaster.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                jobListMaster.ResponseMsg = ex.Message;// Değiştirilecek
                jobListMaster.ResponseCode = 399;
            }
            return jobListMaster;
        }

        public MailResp ListReceivedMessage (int UserID)
        {
            MailResp resp = new MailResp();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SP_ListMails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr["ResponseCode"]) != 303)
                        {
                            Mail mail = new Mail();
                            mail.MailFromID = Convert.ToInt32(dr["MailFromID"]);
                            mail.MailFromName = dr["MailFromName"].ToString();
                            mail.MailToID = Convert.ToInt32(dr["MailToID"]);
                            mail.MailToName = dr["MailToName"].ToString();
                            mail.Title = dr["Title"].ToString();
                            mail.Message = dr["Message"].ToString();
                            mail.IsImportant = Convert.ToBoolean(dr["IsImportant"]);
                            mail.MailDateTime = Convert.ToDateTime(dr["MailDateTime"]);
                            resp.ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
                            resp.mails.Add(mail);
                        }
                        else
                        {
                            resp.ResponseCode = 303;
                        }
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (SqlException)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + logDbMessage + "\n");
                resp.ResponseCode = 301;
            }
            catch (Exception ex)
            {
                LogManager log = new LogManager();
                log.logNotepad(path, DateTime.Now + "\n" + "API SqlManager" + "\n" + ex.Message + "\n");
                resp.ResponseCode = 399;
            }
            return resp;
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
