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
        private readonly IConfiguration _configuration;
        public string cs;

        public DefaultController(IConfiguration config)
        {
            _configuration = config;
            cs = _configuration["ConnectionStrings:SqlDB_1"];
        }
        //public string cs = "Data Source=DESKTOP-SN2L41M;Database=StajProje_1;User Id=testt;Password=QWE123-asd456*;";


        [Authorize]
        [HttpGet]
        [Route("getir")]
        public User listling()
        {
            try
            {
                User user = new User();
                user.UserName = "kullanıcı adı";
                user.UserPassword = "Kulanıcı şifre";
                return user;
            }
            catch (Exception ex)
            {
                User user = new User();
                user.UserName = ex.Message;
                user.UserPassword = ex.Message;
                return user;
            }
            
        }

        [HttpPost]
        [Route("Test_UserLogin")]
        public async Task<ActionResult<string>> Test_UserLoginMng(User inputs)
        {
            string resp = "";
            if (String.IsNullOrEmpty(inputs.UserName) || String.IsNullOrEmpty(inputs.UserPassword))
            {
                resp = "Zorunlu alanlari doldurunuz";
            }
            else
            {
                User item = new User();
                SqlManager sql = new SqlManager(_configuration);

                item.UserName = inputs.UserName;
                item.UserPassword = inputs.UserPassword;

                resp = sql.TestLogIn(item);
            }
            return Ok(resp);
        }

        //[Authorize]
        [HttpPost]
        [Route("viewJobs")]
        public async Task<ActionResult<string>> JobGetir(UserId input)
        {
            //int ResponseCode;
            jobListMaster jobListMaster = new jobListMaster();
            if (input.UserID == 0)
            {
                jobListMaster.ResponseMsg = "Zorunlu alanlari doldurunuz";
                jobListMaster.ResponseCode = 202;
            }
            else
            {
                SqlManager sql = new SqlManager(_configuration);
                jobListMaster = sql.ViewJobs(input.UserID);
            }
            return Ok(jobListMaster);
        }

        /*
        [Authorize]
        [HttpPost]
        [Route("viewJobs_Manager")]
        public jobListMaster JobGetirMng(int UserID)
        {
            jobListMaster jobListMaster = new jobListMaster();
            SqlManager sql = new SqlManager();

            jobListMaster = sql.ViewJobs(UserID);
            return jobListMaster;
        }
        */

        //[Authorize]
        [HttpPost]
        [Route("userRegister_Manager")]
        public async Task<ActionResult<string>> UserRegisterMng(User inputs)
        {
            
            Response resp = new Response();
            if (String.IsNullOrEmpty(inputs.UserName) || String.IsNullOrEmpty(inputs.UserPassword))
            {
                resp.ResponseMsg = "Zorunlu alanlari doldurunuz";
                resp.ResponseCode = 202;
            }
            else
            {
                User user = new User();
                SqlManager sql = new SqlManager(_configuration);

                user.UserName = inputs.UserName;
                user.UserPassword = inputs.UserPassword;

                resp = sql.AddUser(user);
            }
            return Ok(resp);
        }


        //[Authorize]
        [HttpPost]
        [Route("UserLogin_Manager")]
        public async Task<ActionResult<string>> UserLoginMng(User inputs)
        {
            ResponseUID resp = new ResponseUID();
            if (String.IsNullOrEmpty(inputs.UserName) || String.IsNullOrEmpty(inputs.UserPassword))
            {
                resp.ResponseMsg = "Zorunlu alanlari doldurunuz";
                resp.ResponseCode = 202;
            }
            else
            {
                User item = new User();
                SqlManager sql = new SqlManager(_configuration);

                item.UserName = inputs.UserName;
                item.UserPassword = inputs.UserPassword;

                resp = sql.LogIn(item);
            }
            return Ok(resp);
        }

        //[Authorize]
        [HttpPost]
        [Route("deleteJob_Manager")]
        public async Task<ActionResult<string>> DeleteJobMng(JobId input)
        {
            Response response = new Response();
            if (input.JobID==0)
            {
                response.ResponseMsg = "Zorunlu alanlari doldurunuz";
                response.ResponseCode = 202;
            }
            else
            {
                SqlManager sql = new SqlManager(_configuration);

                response = sql.DelJob(input.JobID);
            }
            return Ok(response);
        }

        //[Authorize]
        [HttpPost]
        [Route("saveJob_Manager")]
        public async Task<ActionResult<string>> SaveJobMng(saveJob input)
        {//int UserID, string? Baslik, int HarcananSure, string? Detay, int CustomerID, int Durum, int PriorityID
            Response resp = new Response();
            if(input.UserID==0 || String.IsNullOrEmpty(input.Baslik) || String.IsNullOrEmpty(input.Detay) || input.CustomerID == 0)
            {
                resp.ResponseMsg = "Zorunlu alanlari doldurunuz";
                resp.ResponseCode = 202;
            }
            else
            {
                SqlManager sql = new SqlManager(_configuration);

                resp = sql.AddJob(input.UserID, input.Baslik, input.HarcananSure, input.Detay, input.CustomerID, input.Durum, input.PriorityID);
            }
            return Ok(resp);
        }

        //[Authorize]
        [HttpPost]
        [Route("userLastLoginUpdate_Manager")]
        public async Task<ActionResult<string>> UserLastLoginUpdate(UserId input)
        {
            Response resp = new Response();
            if (input.UserID == 0)
            {
                resp.ResponseMsg = "Zorunlu alanlari doldurunuz";
                resp.ResponseCode = 202;
            }
            else
            {
                SqlManager sql = new SqlManager(_configuration);

                resp = sql.UserLastLoginUpdate(input.UserID);
            }
            return Ok(resp);
        }




        /*
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
        */


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
