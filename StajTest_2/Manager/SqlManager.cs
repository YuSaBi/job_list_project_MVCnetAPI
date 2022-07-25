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
        public string cs = "Data Source=DESKTOP-SN2L41M;Initial Catalog=StajProje_1; Integrated Security=True";// appsetting.json dan çekilecek
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
            catch (Exception ex)
            {
                resp.ResponseMsg = ex.Message;
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
            catch (Exception ex)
            {

                respUID.ResponseMsg = ex.Message;
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
            catch (Exception ex)
            {
                resp.ResponseMsg = ex.Message;
            }
            return resp;
        }

        public Response DelJob(int JobID)
        {
            Response response = new Response();

            try
            {
                using(var connection = new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    param.Add("JobID", JobID);

                    response=connection.Query<Response>("SP_DelJob", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                response.ResponseMsg = ex.Message;
            }

            return response;
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
            catch (Exception ex)
            {

                resp.ResponseMsg = ex.Message;
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
