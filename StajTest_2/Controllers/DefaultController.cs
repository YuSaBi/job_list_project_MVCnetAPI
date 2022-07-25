using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StajTest_2.Models;
using StajTest_2.Manager;
using Microsoft.AspNetCore.Authorization;

namespace StajTest_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public string cs = "Data Source=DESKTOP-SN2L41M;Initial Catalog=StajProje_1; Integrated Security=True";// appsetting.json dan çekilecek

        [Authorize]
        [HttpPost]
        [Route("viewJobs")]
        public jobListMaster JobGetir(int UserID)
        {



            jobListMaster X = new jobListMaster();
            using (SqlConnection Con = new SqlConnection(cs))// SqlConnection bulamazsa SqlClient Expension yüklenmeli
            {
                SqlCommand cmd = new SqlCommand("SP_View", Con);// procedure ismi ve bağlantı girildi
                cmd.CommandType = System.Data.CommandType.StoredProcedure;// bir procedure oldugu için bunu SP olarak tipini belirliyoruz

                SqlParameter parameter = new SqlParameter();// sql parametresi oluşturuluyor
                parameter.ParameterName = "@UserID";// parametre adı giriliyor
                parameter.Value = UserID;// parametre verileri giriliyor

                cmd.Parameters.Add(parameter);// oluşturulan parametre sql komutuna gönderiliyor
                Con.Open();

                //var connection = new SqlConnection()

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())// okuduğumuz verileri oluşturduğumuz değişkene atıyoruz
                {
                    Jobs job = new Jobs();// tekli veri
                    job.Baslik = dr["Baslik"].ToString();
                    job.Detay = dr["Detay"].ToString();
                    job.Gun = Convert.ToDateTime(dr["Gun"]);
                    job.HarcananSure = Convert.ToInt32(dr["HarcananSure"]);
                    job.Musteri = dr["CustomerName"].ToString();
                    job.Durum = dr["DurumAd"].ToString();
                    job.Oncelik = dr["PriorityName"].ToString();

                    X.Jobslist.Add(job);
                }
                dr.Close();
                Con.Close();
            }
            return X;
        }

        [Authorize]
        [HttpPost]
        [Route("userRegister_Manager")]
        public Response UserRegisterMng(string UserName, string UserPassword)
        {
            Response resp = new Response();

            User user = new User();
            user.UserName = UserName;
            user.UserPassword = UserPassword;

            SqlManager sql = new SqlManager();
            resp = sql.AddUser(user);

            return resp;
        }

        [Authorize]
        [HttpPost]
        [Route("userLogin")]
        public int UserLogin(string UserName, string UserPassword)
        {
            int result = 3;
            int UserID = 0;

            using (SqlConnection Con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_Login", Con);// procedure ismi ve bağlantı girildi
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("@UserName", UserName));
                list.Add(new SqlParameter("@UserPassword", UserPassword));
                cmd.Parameters.AddRange(list.ToArray<SqlParameter>());// oluşturulan parametre sql komutuna gönderiliyor

                Con.Open();

                
                SqlDataReader dr = cmd.ExecuteReader();
                
                
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["RESULT"]);
                    if (result == 1)
                    {
                        UserID = Convert.ToInt32(dr["USERID"]);
                    }

                }
                dr.Close();
                Con.Close();
                
            }
            return UserID;
        }

        [Authorize]
        [HttpPost]
        [Route("deleteJob_Manager")]
        public Response DeleteJobMng(int JobID)
        {
            Response response = new Response();
            SqlManager sql = new SqlManager();

            response = sql.DelJob(JobID);

            return response;
        }

        [Authorize]
        [HttpPost]
        [Route("SaveJob_Manager")]
        public Response SaveJobMng(int UserID, string Baslik, int HarcananSure, string Detay, int CustomerID, int Durum, int PriorityID)
        {
            Response resp = new Response();
            SqlManager sql = new SqlManager();
            
            resp = sql.AddJob(UserID, Baslik, HarcananSure, Detay, CustomerID, Durum, PriorityID);

            return resp;
        }










        //[Authorize]
        //[HttpPost]
        //[Route("saveJob")]
        //public int SaveJob(int UserID, string Baslik, int HarcananSure, string Detay, int CustomerID, int Durum, int PriorityID)
        //{
        //    int result = 2;
        //    using (SqlConnection Con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_AddJob", Con);// procedure ismi ve bağlantı girildi
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();

        //        List<SqlParameter> list = new List<SqlParameter>();
        //        list.Add(new SqlParameter("@UserID", UserID));
        //        list.Add(new SqlParameter("@Baslik", Baslik));
        //        list.Add(new SqlParameter("@HarcananSure", HarcananSure));
        //        list.Add(new SqlParameter("@Detay", Detay));
        //        list.Add(new SqlParameter("@CustomerID", CustomerID));
        //        list.Add(new SqlParameter("@DurumID", Durum));
        //        list.Add(new SqlParameter("@PriorityID", PriorityID));
        //        cmd.Parameters.AddRange(list.ToArray<SqlParameter>());// oluşturulan parametre sql komutuna gönderiliyor

        //        Con.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            result = Convert.ToInt32(dr["RESULT"]);
        //        }
        //        dr.Close();
        //        Con.Close();
        //    }
        //    return result;
        //}


        /*
        [Authorize]
        [HttpPost]
        [Route("deleteJob")]
        public int DeleteJob(int UserID)
        {
            int result = 2;
            using (SqlConnection Con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_DelJob", Con);// procedure ismi ve bağlantı girildi
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                SqlParameter parameter = new SqlParameter();// sql parametresi oluşturuluyor
                parameter.ParameterName = "@JobID";// parametre adı giriliyor
                parameter.Value = UserID;// parametre verileri giriliyor

                cmd.Parameters.Add(parameter);// oluşturulan parametre sql komutuna gönderiliyor
                Con.Open();

                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["RESULT"]);
                }
                dr.Close();
                Con.Close();
            }
            return result;
        }
        */

        /*
        [Authorize]
        [HttpPost]
        [Route("viewJobs_SqlManager")]
        public jobListMaster jobGetirSqlMng(int UserID)
        {
            jobListMaster jobListMaster = new jobListMaster();
            SqlManager sql = new SqlManager();

            jobListMaster = sql.GetJob(UserID);

            return jobListMaster;
        }
        */

        /*
        [Authorize]
        [HttpPost]
        [Route("userRegister")]
        public int UserRegister(string UserName, string UserPassword)
        {
            int result = 2;

            using (SqlConnection Con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_Register", Con);// procedure ismi ve bağlantı girildi
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("@UserName", UserName));
                list.Add(new SqlParameter("@UserPassword", UserPassword));
                cmd.Parameters.AddRange(list.ToArray<SqlParameter>());// oluşturulan parametre sql komutuna gönderiliyor

                Con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())// okuduğumuz verileri oluşturduğumuz değişkene atıyoruz
                {
                    result = Convert.ToInt32(dr["Result"]);
                }
                dr.Close();
                Con.Close();
            }
            return result;
        }
        */

    }
}
