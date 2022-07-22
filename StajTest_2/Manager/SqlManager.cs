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
        
        public jobListMaster GetJob(int UserID)
        {
            Response resp = new Response();
            jobListMaster jobListMaster = new jobListMaster();



            try
            {

            }
            catch (Exception ex)
            {

                resp.ResponseMsg = ex.Message;
            }

            return jobListMaster;
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
    }
}
