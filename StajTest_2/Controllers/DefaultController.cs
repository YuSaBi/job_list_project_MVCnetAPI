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
        [Route("test")]
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
        [Route("listJobs")]
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

        [HttpPost]
        [Route("listReceivedMessage")]
        public async Task<ActionResult<string>> ListReceivedMsg(UserId input)
        {
            MailResp mailResp = new MailResp();
            if(input.UserID == 0)
            {
                mailResp.ResponseCode = 202;
            }
            else
            {
                SqlManager sql = new SqlManager (_configuration);
                mailResp = sql.ListReceivedMessage(input.UserID);
            }
            return Ok(mailResp);
        }

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
        [Route("editJob_Manager")]
        public async Task<ActionResult<string>>EditJobMng(EditJob input)
        {
            int resp;
            if(input.JobID == 0 || String.IsNullOrEmpty(input.Baslik) || String.IsNullOrEmpty(input.Detay) || input.CustomerID == 0)
            {
                resp = 202;
            }
            else
            {
                SqlManager sql = new SqlManager(_configuration);

                resp = sql.EditJob(input.JobID, input.Baslik, input.HarcananSure, input.Detay, input.CustomerID, input.Durum, input.PriorityID);
            }
            return Ok(resp);
        }

        // List Sent Mails

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


    }
}
